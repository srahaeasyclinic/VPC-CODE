namespace VPC.Entities.EntityCore.Model.Storage
{
    public class Operation
    {
         public string Name { get; set; }
        //public bool Status { get; set; }
        public string Type { get; set; }
        public int Sequence { get; set; }
        public string Group { get; set; }
        public string Properties { get; set; }

        public string TaskType{get;set;}
        public string TaskVerb{get;set;}
        public string TaskDisplay{get;set;}

        //public Operation()
        //{
        //    //Status = true;
        //    Type = "operation";
        //}
    }
}