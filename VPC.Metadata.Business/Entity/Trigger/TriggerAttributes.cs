using System;
using System.Collections.Generic;
using System.Text;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Operator.DataAnnotations;
using VPC.Metadata.Business.Validator.Schema;

namespace VPC.Metadata.Business.Entity.Trigger {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TriggerAttribute : Attribute
    {
        private string _executionClassName;
        private ExecutionType[] _executionTypes;
        private string _entityName;
        private string _level { get { return "row"; } } //future scope..
        private string[] _body;


        public TriggerAttribute(string executionClassName, ExecutionType[] types, string entityName, string[] body)
        {
            _executionClassName = executionClassName;
            _executionTypes = types;
            _entityName = entityName;
            _body = body;
        }

        public string GetExecutionClassName(){
            return _executionClassName;
        }

        public ExecutionType[] ExecutionTypes()
        {
            return _executionTypes;
        }

        public string GetEntityName()
        {
            return _entityName;
        }
        public string GetLevel()
        {
            return _level;
        }

        public string[] GetBody()
        {
            return _body;
        }
    }

}