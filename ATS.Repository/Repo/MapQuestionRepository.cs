using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Factory.Question;

namespace ATS.Repository.Repo
{
    public class MapQuestionRepository : Repository<TestQuestionMapping>, IMapQuestionRepository
    {
        private ATSDBContext _context;
        public MapQuestionRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public bool MapQuotions(List<TestQuestionMapping> inputs)
        {
            bool isCreated = false;
            try
            {
                for (int indx = 0; indx < inputs.Count; indx++)
                {
                    var map = inputs[indx];
                    isCreated = Create(ref map);
                }
            }
            catch
            {
                throw;
            }
            return isCreated;
        }

        public bool DeleteMappedQuestions(List<TestQuestionMapping> inputs)
        {
            bool isDeleted = false;
            try
            {
                for (int indx = 0; indx < inputs.Count; indx++)
                {
                    var map = inputs[indx];
                    Delete(map);
                }
                isDeleted = true;
            }
            catch
            {
                throw;
            }
            return isDeleted;
        }

        public IQueryable<TestQuestionMapModel> Retrieve(Guid guid)
        {
            try
            {
                var query = (from x in _context.TestQuestionMapping
                             select new TestQuestionMapModel
                             {
                                 Id = x.Id,
                                 Marks = x.Marks,
                                 QId = x.QId,
                                 TestBankId = x.TestBankId
                             }).Where(x => x.Id == guid).AsQueryable<TestQuestionMapModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TestQuestionMapModel> Select(Func<TestQuestionMapModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.TestQuestionMapping
                             select new TestQuestionMapModel
                             {
                                 Id = x.Id,
                                 Marks = x.Marks,
                                 QId = x.QId,
                                 TestBankId = x.TestBankId
                             }).Where(condition).AsQueryable<TestQuestionMapModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

    }
}
