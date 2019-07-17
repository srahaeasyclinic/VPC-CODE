using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;

namespace IHostedServiceAsAService
{
    // Code from https://github.com/aspnet/Hosting/blob/2a98db6a73512b8e36f55a1e6678461c34f4cc4d/samples/GenericHostSample/ServiceBaseLifetime.cs

    public class ServiceBaseLifetime : ServiceBase, IHostLifetime
    {
        private readonly TaskCompletionSource<object> _delayStart = new TaskCompletionSource<object>();
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public ServiceBaseLifetime(IApplicationLifetime applicationLifetime)
        {
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        }

        private IApplicationLifetime ApplicationLifetime { get; }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _delayStart.TrySetCanceled());
            ApplicationLifetime.ApplicationStopping.Register(Stop);

            new Thread(Run).Start(); // Otherwise this would block and prevent IHost.StartAsync from finishing.
            return _delayStart.Task;
        }

        private void Run()
        {
            try
            {
                Run(this); // This blocks until the service is stopped.
                _delayStart.TrySetException(new InvalidOperationException("Stopped without starting"));
            }
            catch (Exception ex)
            {
                _delayStart.TrySetException(ex);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Stop();
            return Task.CompletedTask;
        }

        // Called by base.Run when the service is ready to start.
        protected override void OnStart(string[] args)
        {
            try
            {
               // System.Diagnostics.Debugger.Launch();
                _delayStart.TrySetResult(null);
                base.OnStart(args);
            }
            catch (Exception ex)
            {
                // Log exception
                _log.Error("Email send settings nit configured", ex.Message);
            }
        }

        // Called by base.Stop. This may be called multiple times by service Stop, ApplicationStopping, and StopAsync.
        // That's OK because StopApplication uses a CancellationTokenSource and prevents any recursion.
        protected override void OnStop()
        {
            ApplicationLifetime.StopApplication();
            base.OnStop();
        }
    }
}