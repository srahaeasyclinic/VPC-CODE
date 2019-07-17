using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Globalization;

namespace VPC.Framework.Business.Common
{
    public static class HelperUtility
    {
        public static string ConvertDateToUTC(string date)
        {
            if (CheckDate(date))
            {
                var dateformat = DateTime.Parse(date);
                return dateformat.ToString("MM/dd/yyyy HH:mm:ss");
            }

            return date;
        }

        public static string GetCurrentUTCDate()
        {
            return DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}