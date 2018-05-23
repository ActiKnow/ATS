using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface ITestAssignmentRepository : IRepository<TestAssignment>
    {
        IQueryable<TestAssignmentModel> Select(Func<TestAssignmentModel, bool> condition);
        bool Assign(List<TestAssignment> testAssignmentModel);
    }
}
