using System;

namespace VPC.Entities.Common
{
  public class UserDetailInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSuperadmin{get;set;}
        public bool IsSystemAdmin{get;set;}
    }
}