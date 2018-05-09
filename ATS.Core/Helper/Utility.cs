using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Helper
{
   public class Utility
    {
        public static void CopyEntity<T>(out T t1, object t2)
        {
            var serializedoubleClass = JsonConvert.SerializeObject(t2);

            t1 = JsonConvert.DeserializeObject<T>(serializedoubleClass);
        }
    }
}
