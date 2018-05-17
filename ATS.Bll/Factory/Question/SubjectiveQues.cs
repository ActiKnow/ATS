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
        public bool Create(ref QuestionBankModel input)
        {
            Utility.CopyEntity(out QuestionBank questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Create(ref questionBank);

            QuestionOptionMapping map = new QuestionOptionMapping
            {
                Id = Guid.NewGuid(),
                Answer = input.AnsText,
                QId = input.QId,
                OptionKeyId = input.QuesTypeValue.ToString()
            };
            flag = _unitOfWork.MapOptionRepo.Create(ref map);
            return flag;
        }

        public List<QuestionBankModel> Select(Func<QuestionBankModel, bool> condition)
        {
            var resultDB = _unitOfWork.QuestionRepo.Select(condition).ToList();
            List<QuestionBankModel> result = null;
            if (resultDB != null)
            {
                result = new List<QuestionBankModel>();

                foreach (var ques in resultDB)
                {
                   
                    ques.MappedOptions = _unitOfWork.MapOptionRepo.Select(x => x.QId == ques.QId).ToList();
                    var mapAns = ques.MappedOptions.FirstOrDefault();
                    if (mapAns != null)
                    {
                        ques.AnsText = mapAns.Answer;
                    }
                }
                result = resultDB;
            }
            return result;
        }

        public bool Update(ref QuestionBankModel input)
        {
            Utility.CopyEntity(out QuestionBank questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Update(ref questionBank);
            if (flag)
            {
                //Delete Old Map
                var oldMaps = _unitOfWork.MapOptionRepo.Select(x => x.QId == questionBank.QId).ToList();

                Utility.CopyEntity(out List<QuestionOptionMapping>  list, oldMaps);
                foreach (var map in list)
                {
                    flag = _unitOfWork.MapOptionRepo.Delete(map);
                }

                //Set Mapping
                if (flag)
                {
                    QuestionOptionMapping map = new QuestionOptionMapping
                    {
                        Id = Guid.NewGuid(),
                        QId = input.QId,
                        OptionKeyId = input.QuesTypeValue.ToString(),
                        Answer = input.AnsText,
                    };
                    flag = _unitOfWork.MapOptionRepo.Create(ref map);
                }
            }
            return flag;
        }
    }
}
