using System;
using System.Collections.Generic;

namespace VPC.Entities.EntitySecurity
{
    public class RoleAccessOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Guid> AvailableGroups { get; set; }
    }

   public class AccessLevel
    {
          public int Id { get; set; }
          public string Name { get; set; }
        //public List<CustomLevel> Children { get; set; }
    }

    public class CustomLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
