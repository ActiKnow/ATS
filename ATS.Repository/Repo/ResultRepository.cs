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

        public IQueryable<TestAssignmentModel> Retrieve(List<Guid> userId)
        {

            var query = (from x in _context.TestAssignment
                         select new TestAssignmentModel
                         {

                         }).AsQueryable<TestAssignmentModel>();

            return query;

            //return query;

            //var query = (from x in _context.TestBank
            //             select new TestBankModel
            //             {
            //                 CreatedBy = x.CreatedBy,
            //                 CreatedDate = x.CreatedDate,
            //                 Description = x.Description,
            //                 LastUpdatedBy = x.LastUpdatedBy,
            //                 LastUpdatedDate = x.LastUpdatedDate,
            //                 StatusId = x.StatusId,
            //                 CategoryTypeValue = x.CategoryTypeValue,
            //                 Duration = x.Duration,
            //                 Instructions = x.Instructions,
            //                 LevelTypeValue = x.LevelTypeValue,
            //                 TestBankId = x.TestBankId,
            //                 TestTypeValue = x.TestTypeValue,
            //                 TotalMarks = x.TotalMarks,
            //                 TestAssignments =(from y in _context.TestAssignment
            //                                   select new TestAssignmentModel{
            //                                       UserId = y.UserId,
            //                                       TestBankId = y.TestBankId
            //                                   }).Where(y=>y.TestBankId==x.TestBankId && userId.Contains(y.UserId)).ToList(),
            //             }).AsQueryable<TestBankModel>();
            //return query;

        }
    }
}
