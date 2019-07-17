using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPC.Metadata.Business.Operator.DataAnnotations
{


    public static class Operators
    {
        [OperatorSqlMappert(Comparison.Equals)]
        public static string Equal { get { return "Equal"; } } // equal :: =

        [OperatorSqlMappert(Comparison.NotEquals)]
        public static string NotEqual { get { return "NotEqual"; } }   //not equal :: !=

        [OperatorSqlMappert(Comparison.LessThan)]
        public static string LessThan { get { return "LessThan"; } }    //less than :: <

        [OperatorSqlMappert(Comparison.LessOrEquals)]
        public static string LessThanEqual { get { return "LessThanEqual"; } }    //less than equal :: <=

        [OperatorSqlMappert(Comparison.GreaterThan)]
        public static string GreaterThan { get { return "GreaterThan"; } }    //greater than :: >

        [OperatorSqlMappert(Comparison.GreaterOrEquals)]
        public static string GreaterThanEqual { get { return "GreaterThanEqual"; } }    //greater than equal :: >=

        [OperatorSqlMappert(Comparison.Like)]
        public static string StartsWith { get { return "StartsWith"; } }    //starts with :: LIKE 'value%'

        [OperatorSqlMappert(Comparison.Like)]
        public static string EndssWith { get { return "EndssWith"; } }    //ends with :: LIKE '%value'

        [OperatorSqlMappert(Comparison.Like)]
        public static string Contains { get { return "Contains"; } }    //contains :: LIKE '%value%'


        
        [OperatorSqlMappert(Comparison.Like)]
        public static string LIKE { get { return "LIKE"; } }    //excludes 

        [OperatorSqlMappert(Comparison.In)]
        public static string In { get { return "IN"; } }    //in

  
        public static string NotIn { get { return "NOT IN"; } }    //not in 

        public static string Includes { get { return "INCLUDES"; } }    //includes

        public static string Excludes { get { return "EXCLUDES"; } }    //excludes 

    }
}
