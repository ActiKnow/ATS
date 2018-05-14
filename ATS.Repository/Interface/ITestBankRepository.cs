using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
   public interface ITestBankRepository : IRepository<TestBank>
    {
        IQueryable<TestBankModel> Retrieve(Guid testBankId);
        IQueryable<TestBankModel> Select(Func<TestBankModel,bool> condition);
    }
}
