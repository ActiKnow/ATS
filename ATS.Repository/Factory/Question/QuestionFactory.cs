using ATS.Core.Global;
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
        public QuestionFactory(CommonType quesType)
        {
            Question = null;
            switch (quesType)
            {
                case CommonType.OPTION:
                    ObjectiveQues objective = new ObjectiveQues();
                    Question = objective;
                    QuestionSelector = objective;
                    break;
                case CommonType.BOOL:
                    BoolQues boolQues = new BoolQues();
                    Question = boolQues;
                    QuestionSelector = boolQues;
                    break;
                case CommonType.TEXT:
                    SubjectiveQues subjective = new SubjectiveQues();
                    Question = subjective;
                    QuestionSelector = subjective;
                    break;
            }
        }

    }
}
