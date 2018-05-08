using ATS.Core.Model;
using ATS.Repository.DAO;
using System;
using System.Collections.Generic;

namespace ATS.Repository.Interface
{
   public interface IMapOptionRepository : ICRUD<QuestionOptionMapModel>
    {
        void CreateTask(QuestionOptionMapModel input, ATSDBContext context);
        void UpdateTask(QuestionOptionMapModel input, ATSDBContext context);
        void DeleteTask(QuestionOptionMapModel input, ATSDBContext context);
        List<QuestionOptionMapModel> SelectTask(ATSDBContext context, Func<QuestionOptionMapModel, bool> condition);
    }
}
