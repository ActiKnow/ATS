using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Model
{
    public class SimpleQueryModel
    {
        public string ModelName { get; set; }
        public Dictionary<string, object> Properties { get; set; }

        public object this[string key]
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
                    Properties = new Dictionary<string, object>();
                }
                Properties[key] = value;
            }
        }
    }
}
