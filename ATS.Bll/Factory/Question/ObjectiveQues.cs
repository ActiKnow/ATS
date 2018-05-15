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
    public class ObjectiveQues : IQuestion
    {
        readonly UnitOfWork _unitOfWork;
        public ObjectiveQues(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool Create(QuestionBankModel input)
        {
            QuestionBank questionBank = new QuestionBank();
            Utility.CopyEntity(out questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Create(ref questionBank);

            if (flag)
            {
                string optionKeyId = questionBank.QId.ToString();
                List<Guid> answers = new List<Guid>();

                for (int indx = 0; indx < input.Options.Count(); indx++)
                {
                    var op = input.Options[indx];
                    op.KeyId = optionKeyId;

                    QuestionOption questionOption = new QuestionOption();
                    Utility.CopyEntity(out questionOption, op);
                    questionOption.Id = Guid.NewGuid();
                    flag = _unitOfWork.OptionRepo.Create(ref questionOption);
                    if (flag)
                    {
                        if (op.IsAnswer)
                        {
                            answers.Add(questionOption.Id);
                        }
                    }
                }

                //Set answers
                input.MappedOptions = new List<QuestionOptionMapModel>();
                foreach (var ans in answers)
                {
                    QuestionOptionMapping map = new QuestionOptionMapping
                    {
                        Id = Guid.NewGuid(),
                        QId = input.QId,
                        OptionKeyId = optionKeyId,
                        Answer = ans.ToString(),
                    };
                    //input.MappedOptions.Add(map);

                    flag = _unitOfWork.MapOptionRepo.Create(ref map);
                }
            }
            return flag;
        }

        public List<QuestionBankModel> Select(Func<QuestionBankModel, bool> condition)
        {
            var queryable = _unitOfWork.QuestionRepo.Select(condition);

            var resultDB = queryable.ToList();
            List<QuestionBankModel> result = null;
            if (resultDB != null)
            {
                result = new List<QuestionBankModel>();
                Utility.CopyEntity(out result, resultDB.ToList());
                for (int indx = 0; indx < result.Count; indx++)
                {
                    var mapOp = _unitOfWork.MapOptionRepo.Select(x => x.QId == result[indx].QId);
                    var mappedOptions = result[indx].MappedOptions;
                    Utility.CopyEntity(out mappedOptions, mapOp.ToList());
                    result[indx].MappedOptions = mappedOptions;
                    foreach (var map in result[indx].MappedOptions)
                    {
                        var options = _unitOfWork.OptionRepo.Select(x => x.KeyId == map.OptionKeyId);
                        var tmpOp = result[indx].Options;
                        Utility.CopyEntity(out tmpOp, options.ToList());
                        result[indx].Options = tmpOp;
                    }
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
                string optionKeyId = input.QId.ToString();
                List<Guid> answers = new List<Guid>();

                //Set Options
                for (int indx = 0; indx < input.Options.Count(); indx++)
                {
                    var op = input.Options[indx];
                    op.KeyId = optionKeyId;
                    QuestionOption questionOption = new QuestionOption();
                    Utility.CopyEntity(out questionOption, op);

                    var opFound = _unitOfWork.OptionRepo.Select(x => x.Id == op.Id);
                    if (opFound != null)
                    {
                        flag = _unitOfWork.OptionRepo.Update(ref questionOption);
                    }
                    else
                    {
                        flag = _unitOfWork.OptionRepo.Create(ref questionOption);
                    }
                    if (flag)
                    {
                        if (op.IsAnswer)
                        {
                            answers.Add(op.Id);
                        }
                    }
                }
                //Delete Old Map
                var queryable = _unitOfWork.MapOptionRepo.Select(x => x.QId == input.QId);
                var oldMaps = queryable.ToList();

                List<QuestionOptionMapping> list = new List<QuestionOptionMapping>();

                Utility.CopyEntity(out list, oldMaps);
                foreach (var map in list)
                {
                    flag = _unitOfWork.MapOptionRepo.Delete(map);
                }
                //Set answers
                if (flag)
                {
                    input.MappedOptions = new List<QuestionOptionMapModel>();
                    foreach (var ans in answers)
                    {
                        QuestionOptionMapping map = new QuestionOptionMapping
                        {
                            Id = Guid.NewGuid(),
                            QId = input.QId,
                            OptionKeyId = optionKeyId,
                            Answer = ans.ToString(),
                        };

                        input.MappedOptions.Add(new QuestionOptionMapModel
                        {
                            Id = map.Id,
                            QId = input.QId,
                            OptionKeyId = optionKeyId,
                            Answer = ans.ToString()
                        });

                        flag = _unitOfWork.MapOptionRepo.Create(ref map);
                    }
                }
            }
            return flag;
        }
    }
}
