using System;
using System.Collections.Generic;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Core.Helper;

namespace ATS.Repository.Repo
{
  public class ResultRepository : Repository<TestBankModel>, IResultRepository
    {

        private readonly ATSDBContext _context;
        public ResultRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public IQueryable<TestBankModel> Retrieve(List<Guid> userId)
        {

            var records = (from Ta in _context.TestAssignment.Where(x => userId.Contains(x.UserId))
                           select new TestAssignmentModel
                           {
                               UserId = Ta.UserId,
                               TestBankId = Ta.TestBankId
                           }
                          ).ToList();//AsQueryable<TestAssignmentModel>(); 

            var query = (from x in _context.TestBank
                         join b in records on x.TestBankId equals b.TestBankId
                         //join p in _context.TestAssignment on x.TestBankId equals p.TestBankId
                         // join q in _context.UserInfo on p.UserId equals q.UserId
                         select new TestBankModel
                         {
                             CreatedBy = x.CreatedBy,
                             CreatedDate = x.CreatedDate,
                             Description = x.Description,
                             LastUpdatedBy = x.LastUpdatedBy,
                             LastUpdatedDate = x.LastUpdatedDate,
                             StatusId = x.StatusId,
                             CategoryTypeValue = x.CategoryTypeValue,
                             Duration = x.Duration,
                             Instructions = x.Instructions,
                             LevelTypeValue = x.LevelTypeValue,
                             TestBankId = x.TestBankId,
                             TestTypeValue = x.TestTypeValue,
                             TotalMarks = x.TotalMarks,
                             TestAssignments = records,

                         }).AsQueryable<TestBankModel>();

            return query;
         
        }


    }
}
