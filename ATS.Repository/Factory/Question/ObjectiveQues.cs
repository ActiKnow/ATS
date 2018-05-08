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
            QuesDAO.Create(ref input, context);
            string optionKeyId = input.QId.ToString();
            List<Guid> answers = new List<Guid>();


            //Set Options
            for (int indx = 0; indx < input.Options.Count(); indx++)
            {
                var op = input.Options[indx];
                op.KeyId = optionKeyId;
                OptionDAO.Create(ref op, context);
                if (op.IsAnswer)
                {
                    answers.Add(op.Id);
                }
            }
            //Set answers
            input.MappedOptions = new List<QuestionOptionMapModel>();
            foreach (var ans in answers)
            {
                QuestionOptionMapModel map = new QuestionOptionMapModel
                {
                    QId = input.QId,
                    OptionKeyId = optionKeyId,
                    Answer = ans.ToString(),
                };
                input.MappedOptions.Add(map);
                MapOptionDAO.Create(map, context);
            }
        }

        public List<QuestionBankModel> Select(ATSDBContext context, Func<QuestionBankModel, bool> condition)
        {
            List<QuestionBankModel> result  = QuesDAO.Select(context, condition);
            if (result != null)
            {
                foreach (var ques in result)
                {
                    ques.MappedOptions = MapOptionDAO.Select(context, x => x.QId == ques.QId);
                    foreach (var map in ques.MappedOptions)
                    {
                        ques.Options = OptionDAO.Select(context, x => x.KeyId == map.OptionKeyId);
                    }
                }
            }
            return result;
        }

        public void Update(QuestionBankModel input, ATSDBContext context)
        {
            QuesDAO.Update(input, context);
            string optionKeyId = input.QId.ToString();
            List<Guid> answers = new List<Guid>();

            //Set Options
            for (int indx = 0; indx < input.Options.Count(); indx++)
            {
                var op = input.Options[indx];
                op.KeyId = optionKeyId;
                var opFound = OptionDAO.Select(context, x => x.Id == op.Id);
                if (opFound != null)
                {
                    OptionDAO.Update(op, context);
                }
                else
                {
                    OptionDAO.Create(ref op, context);
                }
                if (op.IsAnswer)
                {
                    answers.Add(op.Id);
                }
            }
            //Delete Old Map
            var oldMaps = MapOptionDAO.Select(context, x => x.QId == input.QId);
            foreach (var map in oldMaps)
            {
                MapOptionDAO.Delete(map, context);
            }
            //Set answers
            input.MappedOptions = new List<QuestionOptionMapModel>();
            foreach (var ans in answers)
            {
                
                QuestionOptionMapModel map = new QuestionOptionMapModel
                {
                    QId = input.QId,
                    OptionKeyId = optionKeyId,
                    Answer = ans.ToString(),
                };
                input.MappedOptions.Add(map);
                MapOptionDAO.Create(map, context);
            }
        }
    }
}
