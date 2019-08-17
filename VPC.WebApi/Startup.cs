using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Swashbuckle.AspNetCore.Swagger;
using VPC.Entities.BatchType;
using VPC.Framework.Business.BatchItems.APIs;
using VPC.Framework.Business.BatchType.Contracts;
using VPC.Framework.Business.Counter.Contracts;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Framework.Business.EntityResourceManager.Contracts;
using VPC.Framework.Business.EntitySecurity.APIs;
using VPC.Framework.Business.Initilize.Contracts;
using VPC.Framework.Business.Menu.Contracts;
using VPC.Framework.Business.MetadataManager.Contracts;
using VPC.Framework.Business.RelationManager.Contracts;
using VPC.Framework.Business.ResourceManager.Contracts;
using VPC.Framework.Business.Role.APIs;
using VPC.Framework.Business.Rule.Contracts;
using VPC.Framework.Business.SampleBusiness.Contracts;
using VPC.Framework.Business.SchedulerConfiguration;
using VPC.Framework.Business.SchedulerConfiguration.Scheduler.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerDaily.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerMonthly.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerWeekly.Contracts;
using VPC.Framework.Business.SchedulerConfiguration.SchedulerYearly.Contracts;
using VPC.Framework.Business.SettingsManager.Contracts;
using VPC.Framework.Business.Task.Contracts;
using VPC.Framework.Business.Task.UserTask;
using VPC.Framework.Business.Tenant.Contracts;
using VPC.Framework.Business.TenantSubscription.Contracts;
using VPC.Framework.Business.WorkFlow.Contracts;
using VPC.WebApi.Attribute;
using VPC.WebApi.Ninject;
using VPC.WebApi.Utility;


namespace VPC.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LoginConfiguration.Configuration = configuration;
            //   BatchThread();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Other configurations
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthRole", policy => policy.AddRequirements(new CustomRoleAuth()));
                options.AddPolicy("AuthFunction", policy => policy.AddRequirements(new CustomFunctionAuth()));
            });
            AuthorizeAttribute rolePolicy = new AuthorizeAttribute("AuthRole");
            AuthorizeAttribute functionPolicy = new AuthorizeAttribute("AuthFunction");
            services.AddHttpContextAccessor();
            // Ninject related configurations
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRequestScopingMiddleware(() => _scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string> () },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllEnumsAsStrings();

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../VPC.Client/ClientApp/src";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                        }

                        await context.Response.WriteAsync("There was an error");
                    });
                });
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api"))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next();
                }
                //  if (context.Response.StatusCode == 403)
                // {
                //     context.Request.Path = "/index.html";
                //     context.Response.StatusCode = 403;
                //     await next();
                // }

            });

            app.UseCors("AllowAllOrigins");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "../VPC.Client/ClientApp/src";
                }
                else
                {
                    spa.Options.SourcePath = "wwwroot";
                }

            });

            this.Kernel = this.RegisterApplicationComponents(app);
        }
      
        #region Wiring up Ninject with ASP.NET Core 2.0

        private readonly AsyncLocal<Scope> _scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }
        private object Resolve(Type type) => Kernel.Get(type);
        private object RequestScope(IContext context) => _scopeProvider.Value;
        private sealed class Scope : DisposableObject { }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            // IKernelConfiguration config = new KernelConfiguration();
            var kernel = new StandardKernel();

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }

            // This is where our bindings are configurated
            kernel.Bind<IHttpResponseMessage>().To<BasicHttpResponseMessage>().InTransientScope();
            kernel.Bind<ISampleBusinessManager>().To<SampleBusinessManager>().InTransientScope();
            kernel.Bind<IJsonMessage>().To<JsonMessage>().InTransientScope();
            kernel.Bind<ILayoutManager>().To<LayoutManager>().InTransientScope();
            kernel.Bind<IMetadataManager>().To<MetadataManager>().InScope(RequestScope);
            kernel.Bind<IQueryManager>().To<QueryManager>().InTransientScope();
            kernel.Bind<IEntityResourceManager>().To<EntityResourceManager>().InTransientScope();
            kernel.Bind<IPicklistManager>().To<PicklistManager>().InTransientScope();
            kernel.Bind<IResourceManager>().To<ResourceManager>().InTransientScope();
            kernel.Bind<IManagerWorkFlow>().To<ManagerWorkFlow>().InTransientScope();
            kernel.Bind<IManagerWorkFlowStep>().To<ManagerWorkFlowStep>().InTransientScope();
            kernel.Bind<IManagerWorkFlowInnerStep>().To<ManagerWorkFlowInnerStep>().InTransientScope();
            kernel.Bind<IManagerWorkFlowProcess>().To<ManagerWorkFlowProcess>().InTransientScope();
            kernel.Bind<IManagerWorkFlowProcessTask>().To<ManagerWorkFlowProcessTask>().InTransientScope();
            kernel.Bind<IManagerWorkFlowOperation>().To<ManagerWorkFlowOperation>().InTransientScope();
            kernel.Bind<IManagerWorkFlowRole>().To<ManagerWorkFlowRole>().InTransientScope();
            kernel.Bind<IManagerWorkFlowTransition>().To<ManagerWorkFlowTransition>().InTransientScope();
            kernel.Bind<IManagerRole>().To<ManagerRole>().InTransientScope();
            kernel.Bind<IManagerWorkFlowSecurity>().To<ManagerWorkFlowSecurity>().InTransientScope();
            kernel.Bind<IManagerEntitySecurity>().To<ManagerEntitySecurity>().InTransientScope();
            kernel.Bind<ISecurityCacheManager>().To<SecurityCacheManager>().InTransientScope();
            kernel.Bind<IInitilizeManager>().To<InitilizeManager>().InTransientScope();
            kernel.Bind<IRelationManager>().To<RelationManager>().InTransientScope();
            kernel.Bind<IMenuManager>().To<MenuManager>().InTransientScope();
            kernel.Bind<IManagerTenantSubscription>().To<ManagerTenantSubscription>().InTransientScope();
            kernel.Bind<IManagerTenantSubscriptionEntity>().To<ManagerTenantSubscriptionEntity>().InTransientScope();
            kernel.Bind<IManagerTenantSubscriptionEntityDetail>().To<ManagerTenantSubscriptionEntityDetail>().InTransientScope();
            kernel.Bind<ISettingManager>().To<SettingManager>().InTransientScope();

           // kernel.Bind<IManagerConfigureScheduler>().To<ManagerConfigureScheduler>().InTransientScope();
            kernel.Bind<IManagerScheduler>().To<ManagerScheduler>().InTransientScope();
            kernel.Bind<IManagerSchedulerDaily>().To<ManagerSchedulerDaily>().InTransientScope();
            kernel.Bind<IManagerSchedulerMonthly>().To<ManagerSchedulerMonthly>().InTransientScope();
            kernel.Bind<IManagerSchedulerWeekly>().To<ManagerSchedulerWeekly>().InTransientScope();
            kernel.Bind<IManagerSchedulerYearly>().To<ManagerSchedulerYearly>().InTransientScope();
            kernel.Bind<IManagerBatchType>().To<ManagerBatchType>().InTransientScope();
            kernel.Bind<IManageRule>().To<RuleManager>().InTransientScope();
            kernel.Bind<IManageTenant>().To<TenantManager>().InTransientScope();
            kernel.Bind<IManagerCounter>().To<ManagerCounter>().InTransientScope();
            kernel.Bind<IManagerBatchItem>().To<ManagerBatchItem>().InTransientScope();
            kernel.Bind<IMatcher>().To<Matcher>().InTransientScope();
            kernel.Bind<IInsertHelper>().To<InsertHelper>().InTransientScope();
            kernel.Bind<IUserTaskManager>().To<UserTaskManager>().InTransientScope();           
kernel.Bind<ITaskManager>().To<TaskManager>().InTransientScope();    
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }
        #endregion
    }
}