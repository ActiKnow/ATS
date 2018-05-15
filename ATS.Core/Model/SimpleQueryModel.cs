using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Model
{
    public enum QueryType { Or, And, Equal, NotEqual, GreaterThan, GreaterThanEqual, LessThan, LessThanEqual }
    public class SimpleQueryModel
    {
        public string ModelName { get; set; }
        public Dictionary<ExpressionType, ExpressionType> Properties { get; set; }

        public struct ExpressionType
        {
            public QueryType QueryCondition { get; set; }
            public object DataValue { get; set; }
        }
        public object this[string key, QueryType queryType = QueryType.And,QueryType condition = QueryType.Equal]
        {
            get
            {
                object resultVal = null;
                if (Properties == null)
                {
                    return resultVal;
                }
                ExpressionType expressionKey = new ExpressionType { QueryCondition = queryType, DataValue = key };
                if (Properties.ContainsKey(expressionKey))
                {
                    resultVal = Properties[expressionKey];
                }
                return resultVal;
            }
            set
            {
                if (Properties == null)
                {
                    Properties = new Dictionary<ExpressionType, ExpressionType>();
                }
                SetValue(key, value, queryType, condition);
            }
        }
        private void SetValue(string key, object dataValue, QueryType qryType = QueryType.And, QueryType condition = QueryType.Equal)
        {
            ExpressionType expressionKey = new ExpressionType { QueryCondition = qryType, DataValue = key };
            if (dataValue != null && dataValue.GetType() == typeof(ExpressionType))
            {
                Properties[expressionKey] = (ExpressionType)dataValue;
            }
            else
            {
                Properties[expressionKey] = new ExpressionType { QueryCondition = condition, DataValue = dataValue };
            }
        }
    }
}
