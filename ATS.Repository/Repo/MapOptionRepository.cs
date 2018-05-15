using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.Repo
{
    public class MapOptionRepository : Repository<QuestionOptionMapping>, IMapOptionRepository
    {
        private readonly ATSDBContext _context;
        public MapOptionRepository(ATSDBContext context) : base(context)
        {
            this._context = new ATSDBContext();
        }

        public IQueryable<QuestionOptionMapModel> Select(Func<QuestionOptionMapModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.QuestionOptionMapping
                             select new QuestionOptionMapModel
                             {
                                 Id = x.Id,
                                 Answer = x.Answer,
                                 OptionKeyId = x.OptionKeyId,
                                 QId = x.QId,

                             }).Where(condition).AsQueryable<QuestionOptionMapModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
