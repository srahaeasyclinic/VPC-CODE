using System;

namespace VPC.Metadata.Business.DataTypes {
    public class AgeCalculation {
        public AgeCalculation()
        {
            
        }

        public dynamic GetAge(System.DateTime dateOfBirth)
        {
           var today = System.DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }
    }
}