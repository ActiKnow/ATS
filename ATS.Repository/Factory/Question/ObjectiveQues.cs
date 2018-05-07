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
    public class ObjectiveQues : IQuestion
    {
        IQuestionRepository QuesDAO;
        IOptionRepository OptionDAO;
        IMapOptionRepository MapOptionDAO;
        public ObjectiveQues()
        {
            OptionDAO = new OptionRepository();
            MapOptionDAO = new MapOptionRepository();
            QuesDAO = new QuestionRepository();
        }

        public void Create(QuestionBankModel input, ATSDBContext context)
        {
            QuesDAO.CreateTask(ref input, context);
            string optionKeyId = input.QId.ToString();
            List<Guid> answers = new List<Guid>();


            //Set Options
            for (int indx = 0; indx < input.Options.Count(); indx++)
            {
                var op = input.Options[indx];
                op.KeyId = optionKeyId;
                OptionDAO.CreateTask(ref op, context);
                if (op.IsAnswer)
                {
                    answers.Add(op.Id);
                }
            }
            //Set answers
            foreach (var ans in answers)
            {
                input.MapOptions.OptionKeyId = optionKeyId;
                QuestionOptionMapModel map = new QuestionOptionMapModel
                {
                    QId = input.QId,
                    OptionKeyId = optionKeyId,
                    Answer = ans.ToString(),
                };
                MapOptionDAO.CreateTask(map, context);
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
            QuesDAO.UpdateTask( input, context);
            string optionKeyId = input.QId.ToString();
            List<Guid> answers = new List<Guid>();

            //Set Options
            for (int indx = 0; indx < input.Options.Count(); indx++)
            {
                var op = input.Options[indx];
                op.KeyId = optionKeyId;
                var opFound = OptionDAO.SelectTask(context, x=>x.Id == op.Id);
                if (opFound != null)
                {
                    OptionDAO.UpdateTask(op, context);
                }
                else
                {
                    OptionDAO.CreateTask(ref op, context);
                }
                if (op.IsAnswer)
                {
                    answers.Add(op.Id);
                }
            }
            //Set answers
            foreach (var ans in answers)
            {
                input.MapOptions.OptionKeyId = optionKeyId;
                QuestionOptionMapModel map = new QuestionOptionMapModel
                {
                    QId = input.QId,
                    OptionKeyId = optionKeyId,
                    Answer = ans.ToString(),
                };
                MapOptionDAO.UpdateTask(map, context);
            }
        }
    }
}
