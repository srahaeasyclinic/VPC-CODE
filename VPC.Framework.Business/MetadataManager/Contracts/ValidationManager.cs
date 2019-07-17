using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Framework.Business.MetadataManager.Contracts
{
    public interface IValidationManager
    {
        bool Entityfieldsvalidation(FieldModel Field, string FieldValue);
    }

    public class ValidationManager : IValidationManager
    {

        #region ValidationByDataType
        bool IValidationManager.Entityfieldsvalidation(FieldModel Field, string FieldValue)
        {
            //Type typeinfo = Type.GetType(FieldDataType);
            // var dataTypes = GetDatatypeInstance();
            // if (dataTypes == null || !dataTypes.Any()) throw new ArgumentException("FieldDataTypeBase not found");

            // var type = dataTypes.FirstOrDefault(t => t.Name.ToLower().Equals(FieldDataType.ToLower()));
            // if (type == null) throw new ArgumentException("Datatype not found");

            // //Create an instance of the type
            // object obj = Activator.CreateInstance(type);
            bool Isvalid = true;
            DataTypeBase type = GetDataTypeByClientName(Field.TypeOf);
            // MethodInfo GetValidators = type.GetValidators();
            // if (GetValidators == null) throw new ArgumentException(string.Format("Validation method not found in {0}", FieldDataType));
            if (type == null)
            {
                return true;
            } // throw new ArgumentException(string.Format("DataTypeBase is not found of {0}", FieldDataType));
              //invoke AddMethod.NumericType

            if (type.DataType == DataType.Number && Field.TypeOf.ToLower() == "numerictype")
            {
                return decimal.TryParse(FieldValue, out decimal i);
            }

            if (type.ControlType == ControlType.DateOfBirth && !string.IsNullOrEmpty(FieldValue))
            {
                var parsedDate = new System.DateTime();
                if (System.DateTime.TryParse(FieldValue, out parsedDate))
                {
                    return System.DateTime.Now > parsedDate;
                }
                else
                {
                    return false;
                }
            }

            List<ValidatorBase> validators = type.GetValidators();
            // validators = (List<ValidatorBase>)GetValidators.Invoke(obj, null);
            if (validators == null || validators.Count == 0)
            {
                return true;
            }

            var ValidationName = GetValidatorInstance();
            List<bool> IsallValid = new List<bool>();
            foreach (var v in validators)
            {

                //Type ValidationName = Type.GetType(v.ValidationName);
                var valitype = ValidationName.FirstOrDefault(t => t.Name.ToLower().Equals(v.ValidationName.ToLower()));
                if (valitype == null) throw new ArgumentException("ValidationType not found");

                var validatortype = Activator.CreateInstance(valitype);
                MethodInfo IsValidators = valitype.GetMethod("IsValid");

                if (IsValidators == null) throw new ArgumentException(string.Format("Validation method not found of {0}", v.ValidationName));



                IsallValid.Add((bool)IsValidators.Invoke(validatortype, new object[] { v, FieldValue }));

                //if (v.ValidationName == "RangeValidator" || v.ValidationName == "LengthValidator")
                //{

                //    //Client setting validation

                //    // end

                //}
            }
            if (IsallValid.Count > 0)
            {
                Isvalid = IsallValid.Contains(false) ? false : true;
            }
            return Isvalid;
        }

        private DataTypeBase GetDataTypeByClientName(string fieldDataType)
        {
            if (string.IsNullOrEmpty(fieldDataType)) return null;

            var dataTypes = Assembly
                //.GetEntryAssembly()
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.GetTypes())
                .Where(type =>
                   typeof(DataTypeBase).GetTypeInfo().IsAssignableFrom(type)).ToList();
            if (!dataTypes.Any()) return null;

            var SkipDatatypes = new string[] { "globalpicklist`1", "localpicklist`1", "lookup`1", "picklist`1" }; // for the time being filter added for some datatypes is giving error due to "'1" with objectname.
            var dataType = dataTypes.Where(w =>
               !SkipDatatypes.Contains(w.Name.ToLower()) && w.IsAbstract == false && w.Name.ToLower() == fieldDataType.ToLower()).FirstOrDefault();

            // DataTypeBase result = null;

            if (dataType == null)
            {
                return null;
            }
            var cl = (DataTypeBase)Activator.CreateInstance(dataType);
            return cl;

            // foreach (var item in dataTypes)
            // {

            //     var cl = (DataTypeBase)Activator.CreateInstance(item);
            //     if (cl != null && cl.DataType.ToString().ToLower().Equals(fieldDataType.ToLower()))
            //     {
            //         result = cl;
            //         break;
            //     }
            // }

            // return result;
        }

        //private List<Type> GetDatatypeInstance () {
        //    List<Type> datatype;

        //    datatype = Assembly
        //        //.GetEntryAssembly()
        //        .GetExecutingAssembly ()
        //        .GetReferencedAssemblies ()
        //        .Select (Assembly.Load)
        //        .SelectMany (x => x.GetTypes ())
        //        .Where (type =>
        //            typeof (DataTypeBase).GetTypeInfo ().IsAssignableFrom (type)).ToList ();

        //    return datatype;
        //}

        private List<Type> GetValidatorInstance()
        {
            List<Type> datatype;

            datatype = Assembly
                //.GetEntryAssembly()
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.GetTypes())
                .Where(type =>
                   typeof(ValidatorBase).GetTypeInfo().IsAssignableFrom(type)
                ).ToList();

            return datatype;
        }
        #endregion
    }
}
