using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.Repo
{
    public class TestAssignmentRepository : Repository<TestAssignment>, ITestAssignmentRepository
    {
        private readonly ATSDBContext _context;

        public TestAssignmentRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public TestAssignment Retrieve(TestAssignment input)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TestAssignmentModel> Select(Func<TestAssignmentModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.TestAssignment
                             select new TestAssignmentModel
                             {
                                 ID = x.ID,
                                 UserId = x.UserId,
                                 TestBankId =x.TestBankId,
                                 StatusId = x.StatusId
                             }).Where(condition).AsQueryable<TestAssignmentModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

        //private IQueryable<TestAssignmentModel> Select()
        //{
        //    var query = (from x in _context.TestAssignment
                        
        //                 select new TestAssignmentModel
        //                 {
        //                     CreatedBy = x.CreatedBy,
        //                     CreatedDate = x.CreatedDate,                           
        //                     LastUpdatedBy = x.LastUpdatedBy,
        //                     LastUpdatedDate = x.LastUpdatedDate,
        //                     StatusId = x.StatusId,
        //                     TestBankId = x.TestBankId,
        //                     UserId = x.UserId,
        //                 });

        //    return query;
        //}

        public bool Assign(List<TestAssignment> testAssignmentModel)
        {
            bool isCreated = false;
            try
            {
                for (int indx = 0; indx < testAssignmentModel.Count; indx++)
                {
                    var map = testAssignmentModel[indx];
                    map.ID = Guid.NewGuid();
                    isCreated = Create(ref map);
                }
            }
            catch
            {
                throw;
            }
            return isCreated;
        }
        public bool DeleteMappedTest(TestAssignment input)
        {
            bool isDeleted = false;
            try
            {
                Delete(input);
                isDeleted = true;
            }
            catch
            {
                throw;
            }
            return isDeleted;
        }

    }
}
