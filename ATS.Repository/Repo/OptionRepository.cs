using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.Repo
{
   public class OptionRepository : Repository<QuestionOption>, IOptionRepository
    {
        private readonly ATSDBContext _context;
        public OptionRepository(ATSDBContext context) : base(context)
        {
            this._context = new ATSDBContext();
        }

        public IQueryable<QuestionOptionModel> Select(Func<QuestionOptionModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.QuestionOption
                             select new QuestionOptionModel
                             {
                                 Id = x.Id,
                                 KeyId = x.KeyId,
                                 Description = x.Description,
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 StatusId = x.StatusId
                             }).Where(condition).AsQueryable<QuestionOptionModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
