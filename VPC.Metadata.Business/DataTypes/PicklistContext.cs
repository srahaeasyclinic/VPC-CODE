namespace VPC.Metadata.Business.DataTypes
{
    public class PicklistContext
    {

        private short _id;
        public PicklistContext(short context)
        {
            _id = context;
        }
        public PicklistContext()
        {
        }
        public short GetContext()
        {
            return _id;
        }
    }
}