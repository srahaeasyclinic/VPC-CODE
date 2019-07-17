using System;
using System.Collections.Generic;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core.Clauses;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;

namespace VPC.Framework.Business.DynamicQueryManager.Core
{
    internal static class TransactionHelper
    {


        public static string BuildQuery(string queryStr)
        {
            string Query = "BEGIN BEGIN TRY ";
            Query += "BEGIN TRANSACTION ";
            Query += queryStr;
            Query += "COMMIT TRANSACTION ";
            Query += "END TRY ";
            Query += "BEGIN CATCH ";
            Query += "ROLLBACK TRANSACTION ";
            Query += "declare @strErrorMessage nvarchar(200),";
            Query += "@intErrorNumber int,";
            Query += "@intErrorSeverity int,";
            Query += "@intErrorState int,";
            Query += "@intErrorLine int,";
            Query += "@strErrorProcedure nvarchar(200)";
            Query += "SELECT @strErrorMessage = ERROR_MESSAGE(),";
            Query += "@intErrorNumber = ERROR_NUMBER(),";
            Query += "@intErrorSeverity = ERROR_SEVERITY(),";
            Query += "@intErrorState = ERROR_STATE(),";
            Query += "@intErrorLine = ERROR_LINE(),";
            Query += "@strErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');";
            Query += "RAISERROR(@strErrorMessage, @intErrorSeverity, 1, @intErrorNumber, @intErrorSeverity, @intErrorState, @strErrorProcedure, @intErrorLine);";
            Query += "END CATCH ";
            Query += "END ";
            return Query;
        }

       
    }
}
