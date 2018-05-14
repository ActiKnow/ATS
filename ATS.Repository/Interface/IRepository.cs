using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository.Interface
{
   public interface IRepository<T> where T : class
    {
        bool Create(ref T input);
        //T Get(Guid id);
        //IEnumerable<T> GetAll();
        //IEnumerable<T> Select(Func<T, bool> condition);
        bool Update(ref T input);
        bool Delete(T input);
    }
}
