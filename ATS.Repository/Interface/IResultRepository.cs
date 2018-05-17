using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Repo;

namespace ATS.Repository.Interface
{
   public interface IResultRepository : IRepository<TestBankModel>
    {
        IQueryable<TestBankModel> Retrieve(List<Guid> userId);

    }
}
