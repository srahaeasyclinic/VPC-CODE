using System;
using System.ComponentModel;

namespace VPC.Entities.WorkFlow.Engine
{
    public static class WorkFlowEngineOperation
    {

        public const string _create = "A66F4E8C-7675-483A-840D-299BA33F99F2";
        public const string _update = "90F35F0D-5D56-4C82-8B66-EC0080177909";
        public const string _delete = "5FF8DED6-73EB-4B03-9787-F940106B817D";
        public const string _lock = "D896532A-55AB-4C89-98F6-3C814F072E35";
        public const string _unLock = "4283C8DB-6C82-482E-9839-89F5E337D896";

        [Description("key_Create")]
        public static Guid Create
        {
            get
            {
                return new Guid(_create);
            }
        }

        [Description("key_Update")]
        public static Guid Update
        {
            get
            {
                return new Guid(_update);
            }
        }

        [Description("key_Delete")]
        public static Guid Delete
        {
            get
            {
                return new Guid(_delete);
            }
        }

        [Description("key_Lock")]
        public static Guid Lock
        {
            get
            {
                return new Guid(_lock);
            }
        }

        [Description("key_Unlock")]
        public static Guid Unlock
        {
            get
            {
                return new Guid(_unLock);
            }
        }
    }
}
