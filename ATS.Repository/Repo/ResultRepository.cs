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

            var records = (from Ta in _context.TestAssignment.Where(x => userId.Contains(x.UserId)).DefaultIfEmpty()
                           select new TestAssignmentModel
                           {                           
                               UserId = Ta.UserId,
                               TestBankId = Ta.TestBankId
                           }
                          ).AsQueryable<TestAssignmentModel>(); //ToList();

            var query = (from x in _context.TestBank
                         join b in records on x.TestBankId equals b.TestBankId
                         group new { x, b } by new { x.TestBankId } into pg
                         let firsttestgroup = pg.FirstOrDefault()
                         let test = firsttestgroup.x
                         let test_user = firsttestgroup.b
                         select new TestBankModel
                         {

                             //CreatedBy = test.CreatedBy,
                             //CreatedDate = test.CreatedDate,
                             //Description = test.Description,
                             //LastUpdatedBy = test.LastUpdatedBy,
                             //LastUpdatedDate = test.LastUpdatedDate,
                             StatusId = test.StatusId,
                             CategoryTypeValue = test.CategoryTypeValue,
                             Duration = test.Duration,
                             Instructions = test.Instructions,
                             LevelTypeValue = test.LevelTypeValue,
                             TestBankId = test.TestBankId,
                             TestTypeValue = test.TestTypeValue,
                             TotalMarks = test.TotalMarks,
                             TestAssignments = records.ToList(),

                         }).AsQueryable<TestBankModel>();

            return query;
         
        }


    }
}
