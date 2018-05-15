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
        public Dictionary<string, ExpressionType> Properties { get; set; }

        public struct ExpressionType
        {
            public QueryType QueryType { get; set; }
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
                if (Properties.ContainsKey(key))
                {
                    resultVal = Properties[key];
                }
                return resultVal;
            }
            set
            {
                if (Properties == null)
                {
                    Properties = new Dictionary<string, ExpressionType>();
                }
                SetValue(key, value, queryType, condition);
            }
        }
        private void SetValue(string key, object dataValue, QueryType queryType = QueryType.And, QueryType condition = QueryType.Equal)
        {
         
            if (dataValue != null && dataValue.GetType() == typeof(ExpressionType))
            {
                Properties[key] = (ExpressionType)dataValue;
            }
            else
            {
                Properties[key] = new ExpressionType { QueryType = queryType, QueryCondition = condition, DataValue = dataValue };
            }
        }
    }
}
