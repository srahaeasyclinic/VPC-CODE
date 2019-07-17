using System;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.Entity.Configuration;

namespace VPC.Metadata.Business.DataTypes
{
    public class DOBDate : ComplexBase
    {
       

        // [Receiver(typeof(DateTime), typeof(AgeCalculation))]
        [Receiver("Date", "AgeCalculation")]
        public ComputedType Age { get; set; }
        
        
        [Broadcaster(typeof(AgeCalculation))]
        [ColumnName("[DOB]")]
        public DateTime Date { get; set; }


        [ColumnName("[DOBIsApproximate]")]
        public BooleanType DOBIsApproximate { get; set; }

        public DOBDate()
        {
            base.ControlType = ControlType.DateOfBirth;
        }
    }
}
