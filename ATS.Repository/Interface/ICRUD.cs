using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository.Interface
{
    public interface ICRUD<T>
    {
        bool Create(T input);
       ICollection<T> Retrieve(T input);
        bool Update(T input);
        bool Delete(T input);
    }
}
