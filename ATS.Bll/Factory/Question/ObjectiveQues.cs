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

        public bool Create(ref QuestionBankModel input)
        {
            Utility.CopyEntity(out QuestionBank questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Create(ref questionBank);

            if (flag)
            {
                var optionKeyId = input.QId;
                List<Guid> answers = new List<Guid>();

                for (int indx = 0; indx < input.Options.Count(); indx++)
                {
                    var op = input.Options[indx];
                    op.KeyId = optionKeyId.ToString();
                    op.Id = Guid.NewGuid();

                    Utility.CopyEntity(out QuestionOption questionOption, op);

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
                if (answers.Count > 0)
                {
                    foreach (var ans in answers)
                    {
                        QuestionOptionMapping map = new QuestionOptionMapping
                        {
                            Id = Guid.NewGuid(),
                            QId = input.QId,
                            OptionKeyId = optionKeyId.ToString(),
                            Answer = ans.ToString(),
                        };
                        input.MappedOptions.Add(new QuestionOptionMapModel
                        {
                            Id = map.Id,
                            QId = input.QId,
                            OptionKeyId = optionKeyId.ToString(),
                            Answer = ans.ToString()
                        });

                        flag = _unitOfWork.MapOptionRepo.Create(ref map);
                    }
                }
                else
                {
                    QuestionOptionMapping map = new QuestionOptionMapping
                    {
                        Id = Guid.NewGuid(),
                        QId = input.QId,
                        OptionKeyId = optionKeyId.ToString(),
                        Answer = null,
                    };
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
                    if (mapOp != null)
                    {
                        Utility.CopyEntity(out List<QuestionOptionMapModel> mappedOptions, mapOp.ToList());
                        result[indx].MappedOptions = mappedOptions;
                    }
                    foreach (var map in result[indx].MappedOptions)
                    {
                        var options = _unitOfWork.OptionRepo.Select(x => x.KeyId == map.OptionKeyId);
                        if (options != null)
                        {
                            Utility.CopyEntity(out List<QuestionOptionModel> tmpOp, options.ToList());
                            result[indx].Options = tmpOp;
                        }
                    }
                }

            }
            return result;
        }

        public bool Update(ref QuestionBankModel input)
        {
            QuestionBank questionBank = new QuestionBank();
            Utility.CopyEntity(out questionBank, input);
            var flag = false;
            flag = _unitOfWork.QuestionRepo.Update(ref questionBank);
            if (flag)
            {
                //Delete old Options
                var oldOptions = _unitOfWork.OptionRepo.Select(x => x.KeyId == questionBank.QId.ToString()).ToList();

                Utility.CopyEntity(out List<QuestionOption> optionList, oldOptions);
                foreach (var op in optionList)
                {
                    flag = _unitOfWork.OptionRepo.Delete(op);
                }
                if (flag)
                {
                    //Set Options
                    var optionKeyId = input.QId;
                    List<Guid> answers = new List<Guid>();
                    for (int indx = 0; indx < input.Options.Count(); indx++)
                    {
                        var op = input.Options[indx];
                        op.KeyId = optionKeyId.ToString();
                        op.Id = Guid.NewGuid();

                        Utility.CopyEntity(out QuestionOption questionOption, op);

                        flag = _unitOfWork.OptionRepo.Create(ref questionOption);
                        if (flag)
                        {
                            if (op.IsAnswer)
                            {
                                answers.Add(op.Id);
                            }
                        }
                    }
                    //Delete Old Map
                    var oldMaps = _unitOfWork.MapOptionRepo.Select(x => x.QId == questionBank.QId).ToList();

                    Utility.CopyEntity(out List<QuestionOptionMapping> list, oldMaps);
                    foreach (var map in list)
                    {
                        flag = _unitOfWork.MapOptionRepo.Delete(map);
                    }
                    //Set answers
                    if (flag)
                    {
                        input.MappedOptions = new List<QuestionOptionMapModel>();
                        if (answers.Count > 0)
                        {
                            foreach (var ans in answers)
                            {
                                QuestionOptionMapping map = new QuestionOptionMapping
                                {
                                    Id = Guid.NewGuid(),
                                    QId = input.QId,
                                    OptionKeyId = optionKeyId.ToString(),
                                    Answer = ans.ToString(),
                                };

                                input.MappedOptions.Add(new QuestionOptionMapModel
                                {
                                    Id = map.Id,
                                    QId = input.QId,
                                    OptionKeyId = optionKeyId.ToString(),
                                    Answer = ans.ToString()
                                });

                                flag = _unitOfWork.MapOptionRepo.Create(ref map);
                            }
                        }
                        else
                        {
                            QuestionOptionMapping map = new QuestionOptionMapping
                            {
                                Id = Guid.NewGuid(),
                                QId = input.QId,
                                OptionKeyId = optionKeyId.ToString(),
                                Answer = null,
                            };
                            flag = _unitOfWork.MapOptionRepo.Create(ref map);
                        }
                    }
                }
            }
            return flag;
        }
    }
}
