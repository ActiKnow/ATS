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

            var query = (from y in _context.TestAssignment
                         join x in _context.TestBank on y.TestBankId equals x.TestBankId
                         select new TestAssignmentModel
                         {
                             MarksObtained = y.MarksObtained,
                             StatusId = y.StatusId,
                             TestBankId = y.TestBankId,
                             UserId = y.UserId,
                             TestBankName=x.Description,
                             UserInfo = (from l in _context.UserInfo
                                         select new UserInfoModel
                                         {
                                             UserId = l.UserId,
                                             Email = l.Email,
                                             FName = l.FName,
                                             LName = l.LName,
                                             Mobile = l.Mobile
                                         }).Where(l => l.UserId == y.UserId).FirstOrDefault(),
                         }).Where(p => userId.Contains(p.UserId)).AsQueryable<TestAssignmentModel>();

            return query;
        }
    }
}
