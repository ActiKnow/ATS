using ATS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Uow;
using ATS.Repository.Model;

namespace ATS.Bll.Factory.Question
{
    public interface IQuestion: ISelectable<QuestionBankModel>
    {
        bool Create(QuestionBankModel input);
        bool Update(QuestionBankModel input);
    }
}
