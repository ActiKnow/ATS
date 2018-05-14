using ATS.Core.Model;
using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using System.Linq;

namespace ATS.Repository.Interface
{
   public interface IMapOptionRepository : IRepository<QuestionOptionMapping>
    {
    //    void Create(QuestionOptionMapModel input, ATSDBContext context);
    //    void Update(QuestionOptionMapModel input, ATSDBContext context);
    //    void Delete(QuestionOptionMapModel input, ATSDBContext context);
          IQueryable<QuestionOptionMapModel> Select(Func<QuestionOptionMapModel, bool> condition);
    }
}
