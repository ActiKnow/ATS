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

            optionKeyId = input.QuesTypeId;
            //Set Options
            foreach (var op in input.Options)
            {
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
                MapOptionDAO.CreateTask(input.MapOptions, context);
            }
        }

        public void Update(QuestionBankModel input, ATSDBContext context)
        {
            throw new NotImplementedException();
        }
    }
}
