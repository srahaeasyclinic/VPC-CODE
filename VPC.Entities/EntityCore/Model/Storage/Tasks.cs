namespace VPC.Entities.EntityCore.Model.Storage
{
    public class Tasks
    {
         public string Name { get; set; }
        public string TaskType{get;set;}
        public string TaskVerb{get;set;}
        public string TaskDisplay{get;set;}
        public string Type { get; set; }

        public Tasks()
        {
            //Status = true;
            Type = "task";
        }
    }
}