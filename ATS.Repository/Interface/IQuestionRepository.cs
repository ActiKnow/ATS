using ATS.Core.Model;
using ATS.Repository.DAO;
using ATS.Repository.Model;
using System;
using System.Collections.Generic;

namespace ATS.Repository.Interface
{
    public interface IQuestionRepository : ICRUD<QuestionBankModel>
    {
        void Create(ref QuestionBankModel input, ATSDBContext context);
        void Update( QuestionBankModel input, ATSDBContext context);
        void Delete( QuestionBankModel input, ATSDBContext context);
        List<QuestionBankModel> Select(ATSDBContext context,Func<QuestionBankModel,bool> condition);
    }
}
