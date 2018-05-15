using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Model;
using ATS.Repository.Uow;

namespace ATS.Bll.Factory.Question
{
    public class SubjectiveQues : IQuestion
    {
        readonly UnitOfWork _unitOfWork;
        public SubjectiveQues(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool Create(QuestionBankModel input)
        {
            QuestionBank questionBank = new QuestionBank();
            Utility.CopyEntity(out questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Create(ref questionBank);

            QuestionOptionMapping map = new QuestionOptionMapping
            {
                Answer = input.AnsText,
                QId = input.QId,
                OptionKeyId = input.QuesTypeValue.ToString()
            };
            flag = _unitOfWork.MapOptionRepo.Create(ref map);
            return flag;
        }

        public List<QuestionBankModel> Select( Func<QuestionBankModel, bool> condition)
        {
            var queryable1= _unitOfWork.QuestionRepo.Select(condition);
            var resultDB = queryable1.ToList();
            List<QuestionBankModel> result = null;
            if (resultDB != null)
            {
                result = new List<QuestionBankModel>();
                
                foreach (var ques in resultDB)
                {
                    var queryable2= _unitOfWork.MapOptionRepo.Select(x => x.QId == ques.QId);
                    var mapOp = queryable2.ToList();
                    ques.MappedOptions = mapOp;
                }
            }
            return result;
        }

        public bool Update(QuestionBankModel input)
        {
            QuestionBank questionBank = new QuestionBank();
            Utility.CopyEntity(out questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Update(ref questionBank);
            if (flag)
            {
                if (input.MappedOptions != null && input.MappedOptions.Count > 0)
                {
                    QuestionOptionMapping map = new QuestionOptionMapping
                    {
                        Id = input.MappedOptions[0].Id,
                        QId = input.QId,
                        OptionKeyId = input.QuesTypeValue.ToString(),
                        Answer = input.AnsText,
                    };
                    flag = _unitOfWork.MapOptionRepo.Update(ref map);
                }
            }
            return flag;
        }
    }
}
