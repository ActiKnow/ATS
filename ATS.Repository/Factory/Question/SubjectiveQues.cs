using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Model;
using ATS.Repository.DAO;
using ATS.Repository.Interface;

namespace ATS.Repository.Factory.Question
{
    public class SubjectiveQues : IQuestion
    {
        IQuestionRepository QuesDAO;
        IMapOptionRepository MapOptionDAO;
        public SubjectiveQues()
        {
            QuesDAO = new QuestionRepository();
            MapOptionDAO = new MapOptionRepository();
        }
        public void Create(QuestionBankModel input, ATSDBContext context)
        {
            QuesDAO.CreateTask(ref input, context);
            input.MapOptions.Answer = input.AnsText;
            input.MapOptions.QId = input.QId;
            input.MapOptions.OptionKeyId = input.QuesTypeId;
            MapOptionDAO.CreateTask(input.MapOptions, context);
        }

        public List<QuestionBankModel> Select(ATSDBContext context, params object[] inputs)
        {
            throw new NotImplementedException();
        }

        public void Update(QuestionBankModel input, ATSDBContext context)
        {
            throw new NotImplementedException();
        }
    }
}
