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
            QuesDAO.CreateTask(input, context);
            input.MapOptions.Answer = input.AnsText;
            input.MapOptions.QId = input.QId;
            input.MapOptions.OptionKeyId = input.QuesTypeId.ToString();
            MapOptionDAO.CreateTask(input.MapOptions, context);
        }
    }
}
