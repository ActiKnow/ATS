using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class TestAssignmentRepository : Repository<TestAssignment>, ITestAssignmentRepository
    {
        public TestAssignmentRepository(ATSDBContext context) : base(context)
        {
        }

        public TestAssignment Retrieve(TestAssignment input)
        {
            throw new NotImplementedException();
        }

        public new bool Update(TestAssignment input)
        {
            throw new NotImplementedException();
        }        
    }
}
