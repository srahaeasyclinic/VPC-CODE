using System;

namespace VPC.Entities.EntityCore.Model.Resource
{
    public class Resource
    { 
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
        public string LanguageName { get; set; }
        public string EntityCode { get; set; }
        public bool IsStatic { get; set; }

        public Resource() {
            
        }

        public Resource(string key, string value)
        {
            Key=key;
            Value=value;
            Language = "en-US";  //Properties.Resources.Language;
            LanguageName =  "Eng"; //Properties.Resources.LanguageName; //
            EntityCode ="";
            IsStatic=true;
        }
       
        public Resource(string key, string value, string language=null, string languageName=null, string entityCode="", bool isStatic=false )
        {
            Key=key;
            Value=value;
            if(language==null)
                Language = "en-US";
            else
                Language =language;
            if(languageName==null)
                LanguageName = "Eng";
            else
                LanguageName =LanguageName;
            EntityCode=entityCode;
            IsStatic=isStatic;
        }
    }

   public class DefaultResourcelanguage
   {
        public string Key { get; set; }

        public string Text { get; set; }
   }
}