using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VPC.Entities.BatchType;
using VPC.Entities.EntityCore.Metadata.Runtime;
using VPC.Entities.EntityCore.Model.Query;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Framework.Business.DynamicQueryManager.Contracts;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity;
using VPC.Metadata.Business.Entity.Configuration;
using VPC.Metadata.Business.Entity.CustomField.Attributes;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Metadata.Business.Entity.Trigger;
using VPC.Metadata.Business.Entity.Version;
using VPC.Metadata.Business.Operations;
using VPC.Metadata.Business.SearchFilter;
using VPC.Metadata.Business.Tasks;
using VPC.Metadata.Business.Validator;
using VPC.Metadata.Business.Validator.Schema;
using static System.String;

namespace VPC.Framework.Business.MetadataManager.Contracts {
    public interface IMetadataManager {

        List<Entity> GetEntities (bool fieldsRequired = true, bool picklistRequired = false);
        Entity GetEntitityByName (string entityName);
        List<ColumnAndField> GetColumnNameByEntityName (string entityName, string[] fields);

        List<ColumnAndField> GetIntersectColumnNameByEntityName (string entityName, string[] fields);
        List<ColumnAndField> GetBasicColumnNameByEntityName (string entityName, string[] fields);

        ColumnAndField GetRelatedColumnNameOfDetailEntity (string parentEntityName, string entityName);
       
        string GetEntityContextByEntityName (string entityName, bool isPickList = false);
        string GetEntityNameByEntityContext (string entityContext, bool isPickList = false);
        string GetTableNameByEntityname (string entityName);
        string GetPrimaryKeyByEntityname (string entityName);
        bool EntityIsAnItem (string entityName, bool picklistRequired = true);
        string GetSubTypeId (string entityName, string subtypename);
        int GetTypeId (string type);
        int GetContextId (string context);
        List<Value> GetSubTypes (string entityName);

        Dictionary<string, string> GetSubTypesDetails (string entityName);

        bool Duplicatevalucheck (Guid tenantId, string entityName, Guid id, string Fieldname, string value);
        Entity GetEntitityByNameExceptsomeFields (string entityName, bool IsItemFieldRequired);
        string GetTemplateBodyWithTagablesValue (string body, JObject resource);

        TriggerAttribute[] GetTriggerProperties (string entityName);
        string GetVersionControlName (string entityName);

        
    }
    public class MetadataManager : IMetadataManager {

        private readonly string _assembliesName = EntityConstant.assembliesName;
        private readonly string _prefixDelimiter = "_";
        #region GetEntities

        private List<TypeInfo> GetEntities (bool picklistRequired = true) {
            List<TypeInfo> classes;
            if (picklistRequired) {
                classes = Assembly
                    .GetEntryAssembly () ?
                    .GetReferencedAssemblies ()
                    .Select (Assembly.Load)
                    .SelectMany (x => x.DefinedTypes)
                    .Where (type =>

                        typeof (EntityBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) ||
                        typeof (PicklistBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) ||
                        typeof (CollectionBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                        !type.IsAbstract
                    ).ToList ();
            } else {
                classes = Assembly
                    .GetEntryAssembly () ?
                    .GetReferencedAssemblies ()
                    .Select (Assembly.Load)
                    .SelectMany (x => x.DefinedTypes)
                    .Where (type =>
                        typeof (EntityBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) ||
                        typeof (CollectionBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                        !type.IsAbstract
                    ).ToList ();
            }

            if (classes == null)
                throw new ArgumentException ("Entity not found");
            return classes;
        }

        List<TypeInfo> GetEntitiesByType (bool picklistRequired = true) {
            List<TypeInfo> classes;
            if (picklistRequired) {
                classes = Assembly
                    .GetEntryAssembly () ?
                    .GetReferencedAssemblies ()
                    .Select (Assembly.Load)
                    .SelectMany (x => x.DefinedTypes)
                    .Where (type =>
                        typeof (PicklistBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                        !type.IsAbstract
                    ).ToList ();
            } else {
                classes = Assembly
                    .GetEntryAssembly () ?
                    .GetReferencedAssemblies ()
                    .Select (Assembly.Load)
                    .SelectMany (x => x.DefinedTypes)
                    .Where (type =>
                        typeof (EntityBase).GetTypeInfo ().IsAssignableFrom (type.AsType ()) &&
                        !type.IsAbstract
                    ).ToList ();
            }

            if (classes == null)
                throw new ArgumentException ("Entity not found");
            return classes;
        }

        List<Entity> IMetadataManager.GetEntities (bool fieldsRequired, bool picklistRequired) {
            var result = GetEntities (picklistRequired);
            var list = new List<Entity> ();
            if (result == null || !result.Any ()) return list;
            list.AddRange (result.Select (item => GetEntityModel (item, fieldsRequired)));
            return list;
        }

        Entity IMetadataManager.GetEntitityByNameExceptsomeFields (string entityName, bool IsItemFieldRequired) {
            var result = GetEntities ();
            if (result == null || !result.Any ()) return null;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            return type != null ? GetEntityModel (type, IsItemFieldRequired : IsItemFieldRequired) : null;
        }

        private Entity GetEntityModel (Type item, bool fieldsRequired = true, bool IsItemFieldRequired = true) {
            var entity = new Entity {
            Name = item.Name,
            PluralName = GetPluralName (item),
            DisplayName = GetDisplayName (item),
            Configurations = GetConfiguration (item),
            SupportWorkflow = GetWorflowConfiguration (item),
            VersionControl=  GetVersionControlName(item)
            };
            if (item.BaseType != null) entity.Type = item.BaseType.Name;
            entity.Subtypes = GetEntitySubTypes (item);

            if (!fieldsRequired) return entity;
            entity.Fields = new List<FieldModel> ();
            var properties = item.GetProperties ();

            entity.RelatedEntities = new List<Entity> ();
            entity.DetailEntities = new List<Entity> ();

            entity.RowLevelOperations = new List<RowLevelOperations> ();
            foreach (var property in properties) {
                if (property.PropertyType.FullName == null ||
                    (!property.PropertyType.IsClass || property.PropertyType.FullName.StartsWith ("System."))) continue;

            
                var field = GetField (property, IsItemFieldRequired);

                if (field != null) {
                    if (field.Fields != null && field.Fields.Any ()) {
                        AddFieldToList (entity, field, property.Name);
                    } else {
                        entity.Fields.Add (field);
                    }
                }

                //---------
                var detailedEntity = GetDetailEntity (property);
                if (detailedEntity != null) {
                    entity.DetailEntities.Add (detailedEntity);
                }
                //--------

                var relatedEntity = GetRelatedEntity (property);
                if (relatedEntity != null) {
                    entity.RelatedEntities.Add (relatedEntity);
                }
            }
            var operations = GetOperations (item);
            if (operations != null) {
                entity.Operations = operations;
            }

            var tasks = GetTasks (item);
            if (tasks != null) {
                entity.Tasks = tasks;
            }

            var isItem = EntityIsAnItem (item.Name, true);
            if (isItem && IsItemFieldRequired) {
                PropertyInfo[] propertyInfos1 = typeof (Item).GetProperties ();
                if (propertyInfos1.Any ()) {
                    foreach (var prop in propertyInfos1) {
                        var itemField = GetField (prop, IsItemFieldRequired);
                        if (itemField == null) continue;
                        entity.Fields.Add (itemField);
                    }
                }
            }
            if (entity.Fields != null && entity.Fields.Any ()) {
                entity.Fields.OrderBy (t => t.Name).ToList ();
            }

            //----------------------
            // Activity Entities...
            var activityEntity = GetActivityEntityName (item);
            if (activityEntity != null) {
                entity.ActivityEntity = GetEntityModel (activityEntity, true, IsItemFieldRequired);
            }
            //----------------------

            //----------------------
            // VersionControl Entities...
            var versionControlName = GetVersionControl (item);
            if (!string.IsNullOrEmpty (versionControlName)) {
                entity.VersionControl = GetEntityByName (versionControlName);
            }

            //need to check this portion..
            var versionOf = GetVersionOf (item);
            if (!string.IsNullOrEmpty (versionOf)) {
                entity.VersionOf = new Entity {
                    Name = versionOf,
                };
                //GetEntityByName (versionOf);
            }
            //----------------------

            var errorCodes = GetErrorCodes (item);
            if (errorCodes != null) {
                entity.ErrorCodes = errorCodes;
            }

            return entity;
        }

        private string GetVersionOf (Type item) {
            var version = (VersionOf[]) item.GetCustomAttributes (typeof (VersionOf), false);
            return (version.Any ()) ? version[0].GetVersionOfName () : Empty;
        }

        private string GetVersionControl (Type item) {
            var version = (VersionControl[]) item.GetCustomAttributes (typeof (VersionControl), false);
            return (version.Any ()) ? version[0].GetVersionClassName () : Empty;
        }

        private Entity GetRelatedEntity (PropertyInfo property) {
            if (!typeof (DataTypeBase).IsAssignableFrom (property.PropertyType)) return null;
            var cl = (DataTypeBase) Activator.CreateInstance (property.PropertyType);
            if (!cl.DataType.Equals (DataType.Lookup)) return null;
            var arguments = property.PropertyType.GetGenericArguments ();
            if (!arguments.Any ()) return null;
            var details = GetEntityModel (arguments[0], false);
            return details;
        }

        private Entity GetDetailEntity (PropertyInfo property) {
            // var parent = property.DeclaringType.Name;
            var propType = property.PropertyType;
            if (propType.BaseType == null || propType.BaseType != typeof (CollectionBase)) return null;
            var genericArguments = propType.GetGenericArguments (); // two and one
            if (!genericArguments.Any ()) return null;
            var details = GetEntityModel (genericArguments[0], false);

            var columnName = GetColumnNameByEntityName (details.Name, null);
            if (columnName == null) return details;
            if (property.DeclaringType != null) {
                var parentTableName = GetTableNameByEntityName (property.DeclaringType.Name);
                var relatedForeignKey = columnName.FirstOrDefault (t => !IsNullOrEmpty (t.ReferenceTableName) && !IsNullOrEmpty (t.ReferenceColumnName) && (t.ReferenceTableName.Equals (parentTableName)));
                if (relatedForeignKey != null) {
                    details.RelatedField = relatedForeignKey.FieldName;
                }
            }
            var targetClass = (details.Type.Equals (typeof (IntersectEntity).Name)) ? genericArguments[1] : genericArguments[0];
            var entityTwo = GetEntityModel (targetClass, false);
            details.RelatedEntity = entityTwo.Name;
            return details;
        }

        private static void AddFieldToList (Entity entity, FieldModel field, string parentName) {
            if (field.Fields == null || !field.Fields.Any ()) return;
            foreach (var nested in field.Fields) {
                nested.Name = parentName + "." + nested.Name;
                if (nested.Fields != null && nested.Fields.Any ()) {
                    AddFieldToList (entity, nested, nested.Name);
                } else {
                    entity.Fields.Add (nested);
                }
            }
        }
        #endregion

        //@Todo need to get from db.
        // private List<string> GetEntitySubTypes(Type item)
        // {
        //     if (item.BaseType != null && item.BaseType != typeof(PrimaryEntity)) return null;
        //     var cl = (PrimaryEntity)Activator.CreateInstance(item);
        //     return cl.SubTypes.Select(t => t.Value).ToList();
        // }

        private List<string> GetEntitySubTypes (Type item) {
            if (item.BaseType == null ||
                (item.BaseType != typeof (PrimaryEntity) && item.BaseType != typeof (DetailEntity))) return null;
            if (item.BaseType == typeof (PrimaryEntity)) {
                var cl = (PrimaryEntity) Activator.CreateInstance (item);
                return cl.SubTypes.Select (t => t.Value).ToList ();
            }
            if (item.BaseType == typeof (DetailEntity)) {
                var cl = (DetailEntity) Activator.CreateInstance (item);
                return cl.SubTypes.Select (t => t.Value).ToList ();
            }
            return null;
        }
        private bool GetWorflowConfiguration (Type type) {
            var operations = (SupportWorkflowAttribute[]) type.GetCustomAttributes (typeof (SupportWorkflowAttribute), false);
            return operations.Any () && operations[0].Value;
        }

        private string GetPluralName (Type type) {
            var operations = (PluralNameAttribute[]) type.GetCustomAttributes (typeof (PluralNameAttribute), false);
            return (operations.Any ()) ? operations[0].Name : Empty;
        }

        private string GetDisplayName (Type type) {
            var operations = (DisplayNameAttribute[]) type.GetCustomAttributes (typeof (DisplayNameAttribute), false);
            return (operations.Any ()) ? operations[0].Name : Empty;
        }

         private Entity GetVersionControlName (Type type) {
            var versionCrt = (VersionControl[]) type.GetCustomAttributes (typeof (VersionControl), false);
            var entity=new Entity();
            if(versionCrt.Any ())
            {
                entity.Name= versionCrt[0].GetVersionClassName();
            }
            return entity;
        }


        private Entities.EntityCore.Model.Storage.Configuration GetConfiguration (Type type) {
            var exportObj = (ExportAttribute[]) type.GetCustomAttributes (typeof (ExportAttribute), false);
            var export = exportObj.Any () && exportObj[0].Value;
            var importObj = (ImportAttribute[]) type.GetCustomAttributes (typeof (ImportAttribute), false);
            var import = importObj.Any () && importObj[0].Value;
            var configuration = new Entities.EntityCore.Model.Storage.Configuration {
                AllowExport = export,
                AllowImport = import
            };
            return configuration;
        }
        private List<Operation> GetOperations (Type type) {
            var operations = (OperationAttribute[]) type.GetCustomAttributes (typeof (OperationAttribute), false);
            return !operations.Any () ? null : operations[0].Operations.Select (item => new Operation { Name = item }).ToList ();
        }

        private List<Tasks> GetTasks (Type type) {
            var tasks = (TaskAttribute[]) type.GetCustomAttributes (typeof (TaskAttribute), false);
            if (!tasks.Any ()) return null;
            var ope = new List<Tasks> ();
            foreach (var item in tasks) {
                // var res = new Tasks { Name = item.Name, TaskType = item.TaskType, TaskVerb = item.TaskVerb, TaskDisplay = item.TaskDisplay };
                Tasks res = JsonConvert.DeserializeObject<Tasks> (JsonConvert.SerializeObject (item));
                ope.Add (res);
            }
            return ope;
        }

        private FieldModel GetField (PropertyInfo property, bool IsItemFieldRequiredx = true) {
            if (property.Name.ToLower ().Equals ("tenantsubscription")) {
                var test1 = property;
            }
            var field = new FieldModel { Name = property.Name };
            if(property.Name.ToString()=="UserInRole")
            {

            }
            var ts = ((AccessibleLayoutAttribute[]) property.GetCustomAttributes (typeof (AccessibleLayoutAttribute), false));
            if (ts.Any ()) {
                field.AccessibleLayoutTypes = ts.Select (t => t.GetValues ()).FirstOrDefault (); // ts[0].GetValues();
            }
            if (typeof (DataTypeBase).IsAssignableFrom (property.PropertyType)) {

                var cl = (DataTypeBase) Activator.CreateInstance (property.PropertyType);
                field.ControlType = cl.ControlType.ToString ();
                field.DataType = cl.DataType.ToString ();
                if (typeof (MetadataPickListBase).IsAssignableFrom (property.PropertyType)) {
                    var metalist = (MetadataPickListBase) Activator.CreateInstance (property.PropertyType);
                    field.DefaultValue = metalist.APIUrl.ToString ();
                }
                // field.DataType = property.PropertyType.Name.ToString ();                

                field.ApplicableForAdvanceSearch = ((AdvanceSearchAttribute[]) property.GetCustomAttributes (typeof (AdvanceSearchAttribute), false)).Any ();
                field.ApplicableForFreeTextSearch = ((FreeTextSearchAttribute[]) property.GetCustomAttributes (typeof (FreeTextSearchAttribute), false)).Any ();
                field.ApplicableForSimpleSearch = ((SimpleSearchAttribute[]) property.GetCustomAttributes (typeof (SimpleSearchAttribute), false)).Any ();
                field.IsQueryable = !((NonQueryableAttribute[]) property.GetCustomAttributes (typeof (NonQueryableAttribute), false)).Any ();
                field.Required = ((NotNullAttribute[]) property.GetCustomAttributes (typeof (NotNullAttribute), false)).Any ();
                field.TypeOf = property.PropertyType.Name;

                field.IsTagable = ((TagableAttribute[]) property.GetCustomAttributes (typeof (TagableAttribute), false)).Any ();
                field.ReadOnly = ((IsReadonlyAttribute[]) property.GetCustomAttributes (typeof (IsReadonlyAttribute), false)).Any ();
                var displayname = (DisplayNameAttribute[]) property.GetCustomAttributes (typeof (DisplayNameAttribute), false);
                field.DisplayName = (displayname.Any ()) ? displayname[0].Name : Empty;

                var hierarchyattributes = ((HierarchyDisplayAttribute[]) property.GetCustomAttributes (typeof (HierarchyDisplayAttribute), false));

                if (hierarchyattributes.Any ()) {
                    field.DefaultValue = Convert.ToString (hierarchyattributes.Select (s => s.Value).FirstOrDefault ());
                }

                var clientAttributes = ((CustomClientAttribute[]) property.GetCustomAttributes (typeof (CustomClientAttribute), false));
                var serverAttributes = ((CustomServerAttribute[]) property.GetCustomAttributes (typeof (CustomServerAttribute), false));
                if (clientAttributes.Any ()) {
                    field.ContextType = property.PropertyType.BaseType.Name.ToString ();
                    field.Context = clientAttributes[0].GetValue ().ToString ();
                } else if (serverAttributes.Any ()) {
                    field.ContextType = property.PropertyType.BaseType.Name.ToString ();
                    field.Context = serverAttributes[0].GetValue ().ToString ();
                }
                var receiver = ((ReceiverAttribute[]) property.GetCustomAttributes (typeof (ReceiverAttribute), false));
                if (receiver.Any ()) {
                    field.ReceiverDataTypes = receiver.Select (t => t.GetReceiverTypeName ()).ToList (); //receiver[0].GetReceiverTypeName();
                    field.ReceivingTypes = receiver.Select (t => t.GetMethodName ()).ToList (); //receiver[0].GetMethodName();
                }

                var broadcaster = ((BroadcasterAttribute[]) property.GetCustomAttributes (typeof (BroadcasterAttribute), false));
                if (broadcaster.Any ()) {
                    field.BroadcastingTypes = broadcaster.Select (t => t.GetBroadcastingMethodName ()).ToList (); //broadcaster[0].GetBroadcastingMethodName();
                }

                var supported = ((QuickAddAttribute[]) property.GetCustomAttributes (typeof (QuickAddAttribute), false));
                if (supported.Any ()) {
                    field.SupportedQuickAddModes = supported.Select (t => t.GetValues ()).FirstOrDefault (); // ts[0].GetValues();
                }

                if (typeof (ComplexBase).IsAssignableFrom (property.PropertyType)) {
                    field.Fields = new List<FieldModel> ();
                    var complex = (ComplexBase) Activator.CreateInstance (property.PropertyType);
                    var complexEntity = GetEntityModel (complex.GetType (), IsItemFieldRequired : IsItemFieldRequiredx);
                    if (complexEntity?.Fields != null && complexEntity.Fields.Any ()) {
                        field.Fields = complexEntity.Fields;
                    }
                }

                if (typeof (NumberBase).IsAssignableFrom (property.PropertyType)) {
                    var number = (NumberBase) Activator.CreateInstance (property.PropertyType);
                    field.DecimalPrecision = number.DecimalPrecision;
                }
                if (cl.DataType.Equals (DataType.PickList) || cl.DataType.Equals (DataType.Lookup)) {
                    var arguments = property.PropertyType.GetGenericArguments ();
                    if (arguments.Any ()) {
                        field.TypeOf = arguments[0].Name;
                        // cl = (DataTypeBase) Activator.CreateInstance (arguments[0]);
                    }
                }
                var validators = cl.GetValidators ();
                if (validators == null) return field;
                field.Validators = MapValidator (validators);
                return field;
            }
            if (typeof (LookupBase).IsAssignableFrom (property.PropertyType)) {
                field.ControlType = "Lookup";
                field.DataType = "Lookup";
                return field;
            }
            if (typeof (PicklistBase).IsAssignableFrom (property.PropertyType)) {
                field.Fields = new List<FieldModel> ();
                var pickListValue = (PicklistBase) Activator.CreateInstance (property.PropertyType);
                //   field.Name = property.Name;
                field.ControlType = "PickList";
                field.DataType = "PickList";
                Entity complexEntity = GetEntityModel (pickListValue.GetType (), IsItemFieldRequired : IsItemFieldRequiredx);
                if (complexEntity?.Fields != null && complexEntity.Fields.Any ()) {
                    field.Fields = complexEntity.Fields;
                }
                return field;
            }
            if (typeof (EntityBase).IsAssignableFrom (property.PropertyType)) {
                field.Fields = new List<FieldModel> ();
                var complex = (EntityBase) Activator.CreateInstance (property.PropertyType);
                var complexEntity = GetEntityModel (complex.GetType (), IsItemFieldRequired : IsItemFieldRequiredx);
                if (complexEntity?.Fields != null && complexEntity.Fields.Any ()) {
                    field.Fields = complexEntity.Fields;
                }
                return field;
            }
            return null;
        }

        private static List<Validator> MapValidator (List<ValidatorBase> validators) {
            return MapValidator_v2 (validators);
        }

        private static List<Validator> MapValidator_v2 (List<ValidatorBase> validators) {
            var mapValidators = new List<Validator> ();
            //var objLenghtValidator = new LenghtValidator();
            foreach (var item in validators) {
                var validator = new Validator {
                    Customizable = item.Customizable,
                    Name = item.ValidationName
                };
                // Added new validation rule for field lenght as per Database's field lenght.
                if (item.GetType () == typeof (LengthValidator)) {
                    var baseValidatorBase = (LengthValidator) item;
                    validator.Dblength = baseValidatorBase.Dblength;
                    validator.MinDblength=baseValidatorBase.MinDblength;
                }

                if (item.GetType () == typeof (EmailFormatValidator)) {
                    var baseValidatorBase = (EmailFormatValidator) item;
                    validator.Pattern = baseValidatorBase.RegexFormat;
                }

                var validatorDetails = item.GetExtraValidationParameters ();
                if (validatorDetails != null) {
                    validator.Options = new List<ValidatorOptions> ();
                    foreach (var option in validatorDetails) {
                        var validationOptions = new ValidatorOptions {
                            Name = option.Name,
                            ControlType = option.ControlType.ToString (),
                            Value = option.Value
                        };
                        validator.Options.Add (validationOptions);
                    }
                }
                mapValidators.Add (validator);
            }
            return mapValidators;
        }

        Entity IMetadataManager.GetEntitityByName (string entityName) {
            return GetEntityByName (entityName);
        }

        private Entity GetEntityByName (string entityName) {
            var result = GetEntities ();
            if (result == null || !result.Any ()) return null;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            return type != null ? GetEntityModel (type) : null;
        }

        private string GetTableName (TypeInfo type) {
            var tableProperties = (TablePropertiesAttribute[]) type.GetCustomAttributes (typeof (TablePropertiesAttribute), false);
            return (tableProperties.Any ()) ? tableProperties[0].TableName : Empty;
        }

        private string GetPrimaryKey (TypeInfo type) {
            var tableProperties = (TablePropertiesAttribute[]) type.GetCustomAttributes (typeof (TablePropertiesAttribute), false);
            return (tableProperties.Any ()) ? tableProperties[0].PrimaryKey : Empty;
        }

        List<ColumnAndField> IMetadataManager.GetColumnNameByEntityName (string entityName, string[] fields) {
            return GetColumnNameByEntityName (entityName, fields);
        }

        private List<ColumnAndField> GetColumnNameByEntityName (string entityName, string[] fields) {
            var columns = new List<ColumnAndField> ();
            var result = GetEntities ();
            if (result != null && result.Any ()) {
                var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
                if (type != null) {
                    GetColumnsByEntityWithLinker (entityName, type, columns, Empty, false, Empty, string.Empty);
                }
            }

            //need to update inverse prefix and inverse reference....
            foreach (var col in columns) {
                if (string.IsNullOrEmpty (col.InverseColumnName) || string.IsNullOrEmpty (col.InverseTableName)) continue;
                var targetPrefix = col.EntityPrefix + _prefixDelimiter + col.FieldName + _prefixDelimiter + col.TypeOf;
                var fieldsWithMatchingTargetPrefix = columns.Where (t => t.EntityPrefix.ToLower ().Equals (targetPrefix.ToLower ())).ToList ();
                if (!fieldsWithMatchingTargetPrefix.Any ()) continue;
                foreach (var fl in fieldsWithMatchingTargetPrefix) {
                    if (!fl.ColumnName.ToLower ().Equals (col.InverseColumnName.ToLower ()) ||
                        !fl.TableName.ToLower ().Equals (col.InverseTableName.ToLower ())) continue;
                    col.InversePrefixName = fl.EntityPrefix;
                    break;
                }
            }
            return columns;
        }
        private void GetColumnsByEntityWithLinker (
            string entityName,
            TypeInfo type,
            List<ColumnAndField> columns,
            string parentClassName,
            bool isReadyToEnterPicklistOrLookup,
            string clientName,

            string customPrefix
        )

        {
            var primaryKey = GetPrimaryKey (type);
            var tableName = GetTableName (type);
            var properties = type.GetProperties ();
            var allowCasecadingDelete = GetCascadeAttribute (type);
            foreach (var item in properties) {
                if (clientName.ToLower () == item.Name.ToLower ()) continue;
                var basicColumn = (BasicColumnAttribute[]) item.GetCustomAttributes (typeof (BasicColumnAttribute), false);
                if (isReadyToEnterPicklistOrLookup && !basicColumn.Any ()) continue;

                var prefix = (string.IsNullOrEmpty (customPrefix)) ? entityName : customPrefix + _prefixDelimiter + entityName;
                MapColumnAndFieldWithLinker (entityName, item, columns, parentClassName, tableName, primaryKey, allowCasecadingDelete, clientName, prefix);
            }
        }

        private void MapColumnAndFieldWithLinker (string entityName,
            PropertyInfo property,
            List<ColumnAndField> columns,
            string parentClassName,
            string tableName,
            string primaryKey,
            bool allowCasecadingDelete,
            string clientName,
            string customPrefix
        ) {

            if (property.PropertyType.FullName == null || (!property.PropertyType.IsClass || property.PropertyType.FullName.StartsWith ("System."))) return;
            TypeInfo type = null;
            var typeOf = string.Empty;

            var isReadyToEnterPicklistOrLookup = false;
            if (typeof (ComplexBase).IsAssignableFrom (property.PropertyType)) {
                var complex = (ComplexBase) Activator.CreateInstance (property.PropertyType);
                type = complex.GetType ().GetTypeInfo ();
                typeOf = type.Name;
            } else if (typeof (PicklistBase).IsAssignableFrom (property.PropertyType)) {
                var oneTwoOne = (PicklistBase) Activator.CreateInstance (property.PropertyType);
                type = oneTwoOne.GetType ().GetTypeInfo ();
                typeOf = type.Name;
                parentClassName = entityName;
            } else if (typeof (EntityBase).IsAssignableFrom (property.PropertyType)) {
                var entityBase = (EntityBase) Activator.CreateInstance (property.PropertyType);
                type = entityBase.GetType ().GetTypeInfo ();
                typeOf = type.Name;
            } else {
                var propType = property.PropertyType;
                if (propType.BaseType != null && (propType.BaseType == typeof (PickListBase) || propType.BaseType == typeof (LookupBase))) {
                    var genericArgu = propType.GetGenericArguments ();
                    if (genericArgu.Any ()) {
                        type = genericArgu[0].GetTypeInfo ();
                    }
                    isReadyToEnterPicklistOrLookup = true;
                }
                if (propType.BaseType != null && propType.BaseType == typeof (CollectionBase)) {
                    var genericArguments = propType.GetGenericArguments (); // two and one
                    if (genericArguments.Any () && genericArguments.Count () > 1) {
                        type = genericArguments[0].GetTypeInfo ();
                    }
                }
            }

            var columnsAndField = PrepareColumnAndFiledWithLinker (entityName, parentClassName, tableName, primaryKey, allowCasecadingDelete, property, clientName, customPrefix);
            if (columnsAndField != null) {
                if (isReadyToEnterPicklistOrLookup && type != null) {
                    columnsAndField.TypeOf = type.Name;
                } else {
                    columnsAndField.TypeOf = typeOf;
                }
                columns.Add (columnsAndField);
            }
            if (type != null) {
                var clientNameStr = property.Name;
                var pre = customPrefix + _prefixDelimiter + clientNameStr;
                GetColumnsByEntityWithLinker (type.Name, type, columns, type.Name, isReadyToEnterPicklistOrLookup, clientNameStr, pre); //parent class name checking...
            }
        }

        private ColumnAndField PrepareColumnAndFiledWithLinker (
            string entityName,
            string className, string tableName, string primaryKey, bool allowCaseCadeDelete, PropertyInfo property,
            string clientName, string customPrefix) {

            var columnsNames = (ColumnNameAttribute[]) property.GetCustomAttributes (typeof (ColumnNameAttribute), false);
            if (!columnsNames.Any ()) return null;

            //approach four
            var fieldName = property.Name;
            var prefix = (customPrefix.Substring (0, 1).Equals (_prefixDelimiter)) ? customPrefix : _prefixDelimiter + customPrefix;

            if (string.IsNullOrEmpty (customPrefix)) throw new ArgumentException ("Entity prefix not found");

            //inverserLogic.... but inverse prefix is complecated....
            var inverseAttributes = (InversePropertyAttribute[]) property.GetCustomAttributes (typeof (InversePropertyAttribute), false);
            var inverseColumnName = (inverseAttributes.Any ()) ? inverseAttributes[0].Key : Empty;
            var inverseTableName = Empty;
            var inversePrefixName = Empty;
            if (!IsNullOrEmpty (inverseColumnName)) {
                inverseTableName = GetInverseTableName (property);
                // var inversePrefix = (DynamicPrefixAttribute[]) property.GetCustomAttributes (typeof (DynamicPrefixAttribute), false);
                // if(inversePrefix!=null && inversePrefix.Any()){
                //     inversePrefixName = inversePrefix[0].GetPrefix();
                // }
            }

            // intersect...........
            var intersectAttributes = (IntersectColumnAttribute[]) property.GetCustomAttributes (typeof (IntersectColumnAttribute), false);
            //var intersectClassName = ""; //need to think (it should be user when User In role);
            //required and null
            var isDbRequired = (DefaultValueAttribute[]) property.GetCustomAttributes (typeof (DefaultValueAttribute), false);
            var isNullPorp = (NotNullAttribute[]) property.GetCustomAttributes (typeof (NotNullAttribute), false);
            var dafultOrder = (DefaultOrderAttribute[]) property.GetCustomAttributes (typeof (DefaultOrderAttribute), false);
            //------------------- if default order is not set....

            //-------------------
            var foreignKeys = (ForeignKeyAttribute[]) property.GetCustomAttributes (typeof (ForeignKeyAttribute), false);
            var referenceTableName = (foreignKeys.Any ()) ? foreignKeys[0].GetReferenceTableName () : Empty;
            var referenceColumnName = (foreignKeys.Any ()) ? foreignKeys[0].GetReferenceColumnName () : Empty;
            var refferencePrefixName = string.Empty;

            var dataType = DataType.Text;
            if (typeof (DataTypeBase).IsAssignableFrom (property.PropertyType)) {
                var cl = (DataTypeBase) Activator.CreateInstance (property.PropertyType);
                dataType = cl.DataType;
            }
            //using two level

            //
            var activeVersion = (ActiveVersionAttribute[]) property.GetCustomAttributes (typeof (ActiveVersionAttribute), false);
            var draftVersion = (DraftVersionAttribute[]) property.GetCustomAttributes (typeof (DraftVersionAttribute), false);
            var autoIncrement = (AutoIncrementAttribute[]) property.GetCustomAttributes (typeof (AutoIncrementAttribute), false);
            var col = new ColumnAndField () {
                EntityPrefix = prefix.ToLower (),
                EntityFullName = entityName,
                ColumnName = columnsNames[0].ColumnName,
                FieldName = fieldName,
                TableName = tableName,
                PrimaryKey = primaryKey,
                //Linker = linker,
                IsNotNull = (isNullPorp.Any ()),
                ReferenceTableName = referenceTableName,
                ReferenceColumnName = referenceColumnName,
                ReferencePrefixName = refferencePrefixName, //// I think it is not required ....
                AllowCaseCadingDelete = allowCaseCadeDelete,
                Value = (isDbRequired.Any ()) ? isDbRequired[0].Value : null,
                DataType = dataType,
                //   DisplayName = displayName,
                InverseColumnName = (!IsNullOrEmpty (inverseColumnName) && !IsNullOrEmpty (inverseTableName)) ? inverseColumnName : Empty,
                InverseTableName = (!IsNullOrEmpty (inverseColumnName) && !IsNullOrEmpty (inverseTableName)) ? inverseTableName : Empty,
                InversePrefixName = inversePrefixName,
                IsIntersectProperties = intersectAttributes.Any (),
                ClientName = clientName,
                DefaultOrder = dafultOrder.Any (),
                ActiveVersionField = activeVersion.Any(),
                DraftVersionField = draftVersion.Any()
            };
            if(autoIncrement.Any()){
                col.AutoIncrement = autoIncrement[0].GetIncrementType();
            }
            return col;
        }

        private string GetInverseTableName (PropertyInfo property) {
            TypeInfo type = null;
            if (typeof (ComplexBase).IsAssignableFrom (property.PropertyType)) {
                var complex = (ComplexBase) Activator.CreateInstance (property.PropertyType);
                type = complex.GetType ().GetTypeInfo ();
            } else if (typeof (PicklistBase).IsAssignableFrom (property.PropertyType)) {
                var oneTwoOne = (PicklistBase) Activator.CreateInstance (property.PropertyType);
                type = oneTwoOne.GetType ().GetTypeInfo ();
            } else if (typeof (EntityBase).IsAssignableFrom (property.PropertyType)) {
                var entityBase = (EntityBase) Activator.CreateInstance (property.PropertyType);
                type = entityBase.GetType ().GetTypeInfo ();
            } else {
                var propType = property.PropertyType;

                if (propType.BaseType == typeof (PickListBase) || propType.BaseType == typeof (LookupBase)) {
                    // type = GetEntities ().FirstOrDefault (t => t.Name.Equals (property.Name));
                    var argu = propType.GetGenericArguments ();
                    if (argu.Any ()) {
                        type = argu[0].GetTypeInfo ();
                    }
                }
                if (propType.BaseType == typeof (CollectionBase)) {
                    var genericArguments = propType.GetGenericArguments (); // two and one
                    if (genericArguments.Any () && genericArguments.Count () > 1) {
                        type = genericArguments[0].GetTypeInfo ();
                    }
                }
            }
            return type == null ? Empty : GetTableName (type);
        }

        List<ColumnAndField> IMetadataManager.GetBasicColumnNameByEntityName (string entityName, string[] fields) {
            var columns = new List<ColumnAndField> ();
            var result = GetEntities ();
            if (result == null || !result.Any ()) return columns;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type == null) return columns;
            var primaryKey = GetPrimaryKey (type);
            var tableName = GetTableName (type);
            var properties = type.GetProperties ();
            var allowCasecadingDelete = GetCascadeAttribute (type);
            foreach (var property in properties) {
                var basicColumn = (BasicColumnAttribute[]) property.GetCustomAttributes (typeof (BasicColumnAttribute), false);
                if (basicColumn.Any ()) {
                    MapColumnAndFieldWithLinker (entityName, property, columns, Empty, tableName, primaryKey, allowCasecadingDelete, Empty, entityName);
                }
            }
            return columns;
        }
        private bool GetCascadeAttribute (TypeInfo type) {
            var casecadeProperties = (CascadeDeleteAttribute[]) type.GetCustomAttributes (typeof (CascadeDeleteAttribute), false);
            return (casecadeProperties.Any ());
        }
        string IMetadataManager.GetEntityContextByEntityName (string entityName, bool isPickList) {
            var context = Empty;

            var result = GetEntities ();
            if (result != null && result.Any ()) {
                var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
                if (type == null) return context;
                if (isPickList) {
                    var instance = (PicklistBase) Activator.CreateInstance (type);
                    if (instance.PicklistContext != null) {
                        return instance.PicklistContext.GetContext ().ToString ();
                    }

                } else {
                    var instance = (EntityBase) Activator.CreateInstance (type);
                    if (instance.EntityContext != null) {
                        return instance.EntityContext.GetContext ().ToString ();
                    }

                }
            }

            return context;
        }

        string IMetadataManager.GetEntityNameByEntityContext (string entityContext, bool isPickList) {
            var entityName = Empty;
            var entityContextRef = Empty;
            var result = GetEntitiesByType (isPickList);
            foreach (var type in result) {
                if (isPickList) {
                    PicklistBase instance = (PicklistBase) Activator.CreateInstance (type);
                    if (instance.PicklistContext != null) {
                        entityContextRef = instance.PicklistContext.GetContext ().ToString ();
                    }
                } else {
                    EntityBase instance = (EntityBase) Activator.CreateInstance (type);
                    if (instance.EntityContext != null) {
                        entityContextRef = instance.EntityContext.GetContext ().ToString ();
                    }
                }
                if (entityContextRef != entityContext) continue;
                entityName = type.Name;
                return entityName;
            }
            return entityName;
        }

        int IMetadataManager.GetTypeId (string type) {
            LayoutType layoutType = (LayoutType) Enum.Parse (typeof (LayoutType), type);
            return (int) layoutType;
        }

        string IMetadataManager.GetSubTypeId (string entityName, string subtypename) {
            if (IsNullOrEmpty (entityName) || IsNullOrEmpty (subtypename)) return Empty;
            var result = GetEntities ();
            if (result == null || !result.Any ()) throw new ArgumentException ("Entity not found");
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type == null) throw new ArgumentException ("Entity not found");
            //need to check this code..
            var retvalue = Empty;
            if (type.BaseType == null) return retvalue;
            if (type.BaseType == typeof (PrimaryEntity)) {
                var cl = (PrimaryEntity) Activator.CreateInstance (type);
                retvalue = GetMatchingSubtypes (subtypename, retvalue, cl.SubTypes);
            }
            if (type.BaseType == typeof (DetailEntity)) {
                var cl = (DetailEntity) Activator.CreateInstance (type);
                retvalue = GetMatchingSubtypes (subtypename, retvalue, cl.SubTypes);
            }
            return retvalue;
        }

        private static string GetMatchingSubtypes (string subtypename, string retvalue, Dictionary<string, string> subTypes) {
            if (subTypes == null || !subTypes.Any ()) throw new ArgumentException ("Subtype not found");
            foreach (var item in subTypes) {
                if (!item.Value.ToLower ().Equals (subtypename.ToLower ())) continue;
                retvalue = item.Key;
                break;
            }
            return retvalue;
        }

        List<Value> IMetadataManager.GetSubTypes (string entityName) {
            var subTypes = new List<Value> ();
            if (IsNullOrEmpty (entityName)) {
                return subTypes;
            }
            var result = GetEntities ();
            if (result == null || !result.Any ()) return subTypes;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type == null) return subTypes;
            if (type.BaseType == null) return subTypes;
            if (type.BaseType == typeof (PrimaryEntity)) {
                var cl = (PrimaryEntity) Activator.CreateInstance (type);
                subTypes.AddRange (cl.SubTypes.Select (item => new Value { Name = item.Value }));
            }
            if (type.BaseType != typeof (DetailEntity)) return subTypes; {
                var cl = (DetailEntity) Activator.CreateInstance (type);
                subTypes.AddRange (cl.SubTypes.Select (item => new Value { Name = item.Value }));
            }
            return subTypes;
        }

        int IMetadataManager.GetContextId (string context) {
            LayoutContext layout = (LayoutContext) Enum.Parse (typeof (LayoutContext), context);
            return (int) layout;
        }

        string IMetadataManager.GetTableNameByEntityname (string entityName) {
            return GetTableNameByEntityName (entityName);
        }

        //need to merge in one function table name and entityName.........
        private string GetTableNameByEntityName (string entityName) {
            var tablename = Empty;
            var result = GetEntities ();
            if (result == null || !result.Any ()) return tablename;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type != null) {
                tablename = GetTableName (type);
            }
            return tablename;
        }
        string IMetadataManager.GetPrimaryKeyByEntityname (string entityName) {
            var primaryKey = Empty;
            var result = GetEntities ();
            if (result == null || !result.Any ()) return primaryKey;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type != null) {
                primaryKey = GetPrimaryKey (type);
            }
            return primaryKey;
        }

        bool IMetadataManager.EntityIsAnItem (string entityName, bool picklistRequired) {
            return EntityIsAnItem (entityName, picklistRequired);
        }

        private bool EntityIsAnItem (string entityName, bool picklistRequired = true) {
            //  if(picklistRequired) return false;
            var classes = Assembly
                .GetEntryAssembly () ?
                .GetReferencedAssemblies ()
                .Select (Assembly.Load)
                .SelectMany (x => x.DefinedTypes)
                .Where (type =>
                    typeof (IItem<Item>).GetTypeInfo ().IsAssignableFrom (type.AsType ())).ToList ();
            return classes != null && classes.Any (item => item.Name.ToLower ().Equals (entityName.ToLower ()));
        }

        private Type GetActivityEntityName (Type entity) {
            Type[] ifaces = entity.GetInterfaces ();
            return !ifaces.Any () ? null : (from inf in ifaces where inf.IsGenericType && inf.GetGenericTypeDefinition () == typeof (IActivityEntity<>) select inf.GetGenericArguments () into genericArguments where genericArguments.Any () select genericArguments[0]).FirstOrDefault ();
        }
        ColumnAndField IMetadataManager.GetRelatedColumnNameOfDetailEntity (string parentEntityName, string entityName) {
            var column = new ColumnAndField ();
            var result = GetEntities ();
            if (result == null || !result.Any ()) return column;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type == null) return column;
            var columns = GetColumnNameByEntityName (entityName, null);
            var tableName = GetTableNameByEntityName (parentEntityName);
            //foreign key only
            foreach (var col in columns) {
                if (!col.ReferenceTableName.Equals (tableName)) continue;
                column = col;
            }
            return column;
        }

        Dictionary<string, string> IMetadataManager.GetSubTypesDetails (string entityName) {
            var result = GetEntities ();
            if (result == null || !result.Any ()) throw new ArgumentException ("Entity not found");
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type == null) throw new ArgumentException ("Entity not found");
            if (type.BaseType == null) return new Dictionary<string, string> ();
            if (type.BaseType == typeof (PrimaryEntity)) {
                var cl = (PrimaryEntity) Activator.CreateInstance (type);
                return cl.SubTypes;
            }
            if (type.BaseType == typeof (DetailEntity)) {
                var cl = (DetailEntity) Activator.CreateInstance (type);
                return cl.SubTypes;
            }
            return new Dictionary<string, string> ();
        }

        List<ColumnAndField> IMetadataManager.GetIntersectColumnNameByEntityName (string entityName, string[] fields) {
            var columns = new List<ColumnAndField> ();
            var result = GetEntities ();
            if (result == null || !result.Any ()) return columns;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type != null) {
                columns = GetIntersectColumns (entityName, type);
            }
            return columns;
        }

        private List<ColumnAndField> GetIntersectColumns (string entityName, TypeInfo type) {
            var columns = new List<ColumnAndField> ();
            var properties = type.GetProperties ();
            var merGeColumns = new List<ColumnAndField> ();
            foreach (var property in properties) {
                var intersectColumns = (IntersectColumnAttribute[]) property.GetCustomAttributes (typeof (IntersectColumnAttribute), false);
                if (!intersectColumns.Any ()) continue;
                if (property.PropertyType.FullName == null || (!property.PropertyType.IsClass || property.PropertyType.FullName.StartsWith ("System."))) return columns;
                var propType = property.PropertyType;
                if (propType.BaseType != null && propType.BaseType == typeof (CollectionBase)) {
                    var genericArguments = propType.GetGenericArguments (); // two and one
                    if (genericArguments.Count () > 1) {
                        type = genericArguments[0].GetTypeInfo ();
                        var clientNameStr = property.Name;

                        var customPrefix = entityName + "." + type.Name;
                        GetColumnsByEntityWithLinker (type.Name, type, columns, type.Name, false, clientNameStr, customPrefix); //parent class name checking...
                        if (!columns.Any ()) continue;
                        var match = columns.FirstOrDefault (t => t.ColumnName.Equals (intersectColumns[0].GetColumn ().ToString ()));
                        var intersectType = genericArguments[1].GetTypeInfo ();
                        if (match == null) continue;
                        match.IntersectClassName = intersectType.Name;
                        merGeColumns.Add (match);
                    }
                }
            }

            return merGeColumns;

        }

        #region DuplicateFieldValueCheck
        bool IMetadataManager.Duplicatevalucheck (Guid tenantId, string entityName, Guid id, string Fieldname, string value) {
            var columns = GetColumnNameByEntityName (entityName, null);
            if (columns == null || !columns.Any ()) throw new ArgumentException ("Columns not found in the " + entityName);
            var columnname = columns.Where (w => w.FieldName == Fieldname).Select (s => s.ColumnName).FirstOrDefault ();
            if (columnname == null || !columnname.Any ()) throw new ArgumentException ("Columns not found in the " + entityName);
            QueryContext queryModel = MetadataHelper.GetQueryContext (entityName, 0, 0, id != Guid.Empty ? Format ("InternalId<>{0},{1}='{2}'", id, columnname, value) : Format ("{0}='{1}'", columnname, value));

            if (queryModel == null) throw new ArgumentException ("QueryContext is invalid!");

            if (columns.Any ()) {
                queryModel.Fields = Join (",", columns);
            }
            IQueryManager querym = new QueryManager ();
            var result = querym.GetResult (tenantId, entityName, queryModel, false);
            return result == null || result.Rows.Count <= 0;
        }
        #endregion

        string IMetadataManager.GetTemplateBodyWithTagablesValue (string body, JObject resource) {
            foreach (var item in resource) {
                body = body.Replace ("[" + item.Key + "]", Convert.ToString (item.Value));
            }
            body = Regex.Replace (body, "(\\[.*?\\])", "");
            return body;
        }

        TriggerAttribute[] IMetadataManager.GetTriggerProperties (string entityName) {
            var result = GetEntities ();
            if (result == null || !result.Any ()) throw new ArgumentException ("Entity not found");
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if (type == null) throw new ArgumentException ("Entity not found");
            var triggers = (TriggerAttribute[]) type.GetCustomAttributes (typeof (TriggerAttribute), false);
            return triggers;
        }
        private List<ErrorCode> GetErrorCodes (Type type) {
            var errorCodes = (ErrorCodeAttribute[]) type.GetCustomAttributes (typeof (ErrorCodeAttribute), false);
            return !errorCodes.Any () ? null : errorCodes[0].ErrorCodes.Select (item => new ErrorCode { Code = item }).ToList ();
        }

        string IMetadataManager.GetVersionControlName (string entityName) {
            var result = GetEntities ();
            if (result == null || !result.Any ()) return string.Empty;
            var type = result.FirstOrDefault (t => t.Name.ToLower ().Equals (entityName.ToLower ()));
            if(type==null)return string.Empty;
            return  GetVersionControl (type);
        }

       
    }
}