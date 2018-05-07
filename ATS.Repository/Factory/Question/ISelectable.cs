using ATS.Core.Model;
using ATS.Repository.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository.Factory.Question
{
    public interface ISelectable<T>
    {
        List<T> Select( ATSDBContext context,params object[] inputs);
    }
}
