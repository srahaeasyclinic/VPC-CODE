namespace VPC.Cache
{
    public static class VPCCache
    {
        private static readonly Cache Cache;

        static VPCCache()
        {
            Cache=new Cache();
        }
        public static Cache GetInstance()
        {
            return Cache;
        }
    }
}