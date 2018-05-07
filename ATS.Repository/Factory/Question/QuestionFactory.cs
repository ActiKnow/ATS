using ATS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository.Factory.Question
{
    public class QuestionFactory
    {
        public IQuestion Question { get; }
        public ISelectable<QuestionBankModel> QuestionSelector { get; }
        public QuestionFactory(string quesType)
        {
            Question = null;
            if (quesType == Constants.OPTION)
            {
                ObjectiveQues obj = new ObjectiveQues();
                Question = obj;
                QuestionSelector = obj;
            }
            else if (quesType == Constants.BOOL)
            {
                BoolQues obj = new BoolQues();
                Question = obj;
                QuestionSelector = obj;
            }
            else if (quesType == Constants.TEXT)
            {
                SubjectiveQues obj = new SubjectiveQues();
                Question = obj;
                QuestionSelector = obj;
            }
        }

    }
}
