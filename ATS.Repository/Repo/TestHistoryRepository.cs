using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class TestHistoryRepository : Repository<UserTestHistory>, ITestHistoryRepository
    {
        public TestHistoryRepository(ATSDBContext context) : base(context)
        {
        }

        public UserTestHistory Retrieve(UserTestHistory input)
        {
            throw new NotImplementedException();
        }

        public new bool Update(UserTestHistory input)
        {
            throw new NotImplementedException();
        }
    }
}
