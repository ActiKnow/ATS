using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class AttemptHistoryRepository : Repository<UserAttemptedHistory> , IAttemptHistoryRepository
    {   
        public AttemptHistoryRepository(ATSDBContext context) : base(context)
        {          
        }
    }
}
