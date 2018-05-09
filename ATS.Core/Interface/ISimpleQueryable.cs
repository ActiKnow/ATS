using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Core.Interface
{
   public   interface ISimpleQueryable<Model, Q>
    {
        Expression<Func<Model, bool>> GetQuery( Q queries);
    }
}
