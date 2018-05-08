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
            QuesDAO.Create(ref input, context);
            if (input.MappedOptions != null && input.MappedOptions.Count > 0)
            {
                input.MappedOptions[0].Answer = input.AnsText;
                input.MappedOptions[0].QId = input.QId;
                input.MappedOptions[0].OptionKeyId = input.QuesTypeId.ToString();
                MapOptionDAO.Create(input.MappedOptions[0], context);
            }
        }

        public List<QuestionBankModel> Select(ATSDBContext context, Func<QuestionBankModel, bool> condition)
        {
            var result = (from bank in context.QuestionBank
                          select new QuestionBankModel
                          {
                              QId = bank.QId,
                              Description = bank.Description,
                              QuesTypeId = bank.QuesTypeId,
                              LevelTypeId = bank.LevelTypeId,
                              CategoryTypeId = bank.CategoryTypeId,
                              DefaultMark = bank.DefaultMark,
                          });
            return result.Where(condition).ToList();
        }

        public void Update(QuestionBankModel input, ATSDBContext context)
        {
            QuesDAO.Update( input, context);
            if (input.MappedOptions != null && input.MappedOptions.Count > 0)
            {
                input.MappedOptions[0].Answer = input.AnsText;
                input.MappedOptions[0].QId = input.QId;
                input.MappedOptions[0].OptionKeyId = input.QuesTypeId.ToString();
                MapOptionDAO.Update(input.MappedOptions[0], context);
            }
        }
    }
}
