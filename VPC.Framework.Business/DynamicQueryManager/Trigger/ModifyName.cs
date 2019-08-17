using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VPC.Framework.Business.DynamicQueryManager.Core;
using VPC.Framework.Business.DynamicQueryManager.Core.Enums;
using VPC.Metadata.Business.Entity.Trigger.Execution;

namespace VPC.Framework.Business.DynamicQueryManager.Trigger
{
    public class ModifyName : ITriggerExecution
    {
        public TriggerExecutionMessage ExecuteTrigger(TriggerExecutionPayload payload)
        {
            var updateQuery = new UpdateQueryBuilder();
            var columnWithValue = new Dictionary<string, string>();
            var value = payload.PayloadObj.Where(item => !string.IsNullOrEmpty(item.Value)).Aggregate(string.Empty, (current, item) => current + (item.Value + " "));
            columnWithValue.Add("[Name]", value.TrimEnd());
            updateQuery.AddTable("[dbo].[Item]", columnWithValue);
            updateQuery.AddWhere("[Id]", Comparison.Equals, payload.ConditionalValue);
            var queryRes = updateQuery.BuildQuery();
            var message = new TriggerExecutionMessage
            {
                Message = queryRes
            };
            return message;
        }
    }
}

