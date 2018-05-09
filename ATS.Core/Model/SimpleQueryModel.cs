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
    }
}
