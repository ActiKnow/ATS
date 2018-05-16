using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;


namespace ATS.Repository.Repo
{
    public class QuestionRepository : Repository<QuestionBank>, IQuestionRepository
    {
        private readonly ATSDBContext _context;
        public QuestionRepository(ATSDBContext context) : base(context)
        {
            this._context = new ATSDBContext();
        }

        public IQueryable<QuestionBankModel> Retrieve(Guid qId)
        {
            try
            {
                var query = Select().Where(x => x.QId == qId).AsQueryable <QuestionBankModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<QuestionBankModel> Select(Func<QuestionBankModel, bool> condition)
        {
            try
            {
                var query = Select().Where(condition).AsQueryable<QuestionBankModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }

        private IQueryable<QuestionBankModel> Select()
        {
            var query = (from x in _context.QuestionBank
                         join p in _context.TypeDef on x.CategoryTypeValue equals p.Value
                         join q in _context.TypeDef on x.LevelTypeValue equals q.Value
                         join r in _context.TypeDef on x.QuesTypeValue equals r.Value
                         select new QuestionBankModel
                         {
                             CategoryTypeValue = x.CategoryTypeValue,
                             CreatedBy = x.CreatedBy,
                             CreatedDate = x.CreatedDate,
                             DefaultMark = x.DefaultMark,
                             Description = x.Description,
                             LastUpdatedBy = x.LastUpdatedBy,
                             LastUpdatedDate = x.LastUpdatedDate,
                             LevelTypeValue = x.LevelTypeValue,
                             QId = x.QId,
                             QuesTypeValue = x.QuesTypeValue,
                             StatusId = x.StatusId,
                             CategoryTypeDescription = p.Description,
                             LevelTypeDescription = q.Description,
                             QuesTypeDescription = r.Description
                         }).AsQueryable<QuestionBankModel>();

            return query;
        }
    }
}
