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
    public class BoolQues : IQuestion
    {
        IQuestionRepository QuesDAO;
        IMapOptionRepository MapOptionDAO;
        public BoolQues()
        {
            QuesDAO = new QuestionRepository();
            MapOptionDAO = new MapOptionRepository();
        }
        public void Create(QuestionBankModel input, ATSDBContext context)
        {
            QuesDAO.Create(ref input, context);
            QuestionOptionMapModel map = new QuestionOptionMapModel
            {
                Answer = input.AnsText,
                QId = input.QId,
                OptionKeyId = Constants.BOOL
            };
            MapOptionDAO.Create(map, context);
        }

        public List<QuestionBankModel> Select(ATSDBContext context, Func<QuestionBankModel, bool> condition)
        {
            List<QuestionBankModel> result = QuesDAO.Select(context, condition);
            if (result != null)
            {
                foreach (var ques in result)
                {
                    ques.MappedOptions = MapOptionDAO.Select(context, x => x.QId == ques.QId);
                }
            }
            return result;
        }

        public void Update(QuestionBankModel input, ATSDBContext context)
        {
            QuesDAO.Update(input, context);
            if (input.MappedOptions != null && input.MappedOptions.Count > 0)
            {
                input.MappedOptions[0].Answer = input.AnsText;
                input.MappedOptions[0].QId = input.QId;
                input.MappedOptions[0].OptionKeyId = Constants.BOOL;
                MapOptionDAO.Update(input.MappedOptions[0], context);
            }
        }
    }
}
