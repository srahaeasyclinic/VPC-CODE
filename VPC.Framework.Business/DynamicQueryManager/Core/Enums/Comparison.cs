using System.ComponentModel;

namespace VPC.Framework.Business.DynamicQueryManager.Core.Enums
{
    public enum Comparison
    {
        [Description("Equal")] 
        Equals,


        [Description("Not Equal")] 

        NotEquals,

        [Description("Like")] 
        Like,

        [Description("Not like")] 
        NotLike,

        [Description("Greater than")] 
        GreaterThan,

        [Description("Greater Or Equal")] 
        GreaterOrEquals,

        [Description("Less than")] 
        LessThan,

        [Description("Less than or equal")] 
        LessOrEquals,

        [Description("In")] 
        In
    }
}
