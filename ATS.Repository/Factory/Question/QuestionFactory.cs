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
        private IQuestion _question;
       public IQuestion Question { get { return _question; } }

        public QuestionFactory(string quesType)
        {
            _question = null;
            if (quesType == Constants.OPTION)
            {
                _question = new ObjectiveQues();
            }
            else if (quesType == Constants.BOOL)
            {
                _question = new BoolQues();
            }
            else if (quesType == Constants.TEXT)
            {
                _question = new SubjectiveQues();
            }
        
        }
    }
}
