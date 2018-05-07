using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Factory.Question;

namespace ATS.Repository.DAO
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        IOptionRepository OptionDAO;
        IMapOptionRepository MapOptionDAO;
        public QuestionRepository()
        {
            OptionDAO = new OptionRepository();
            MapOptionDAO = new MapOptionRepository();
        }
        public bool Create(QuestionBankModel input)
        {
            bool isCreated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (input != null)
                        {
                            QuestionFactory quesFactory = new QuestionFactory(input.QuesTypeValue);
                            if (quesFactory.Question != null)
                            {
                                quesFactory.Question.Create(input, context);
                                dbContextTransaction.Commit();
                                isCreated = true;
                            }
                        }
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isCreated;
            }
        }

        public void CreateTask(ref QuestionBankModel input, ATSDBContext context)
        {
            if (input != null)
            {
                input.QId = Guid.NewGuid();
                QuestionBank ques = new QuestionBank
                {
                    QId = input.QId,
                    Description = input.Description,
                    QuesTypeId = input.QuesTypeId,
                    LevelTypeId = input.LevelTypeId,
                    DefaultMark = input.DefaultMark,
                    CategoryTypeId = input.CategoryTypeId,
                    CreatedBy = input.CreatedBy
                };
                context.QuestionBank.Add(ques);
                context.SaveChanges();
            }
        }

        public bool Delete(QuestionBankModel input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DeleteTask(input, context);
                        dbContextTransaction.Commit();
                        isDeleted = true;

                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isDeleted;
            }
        }

        public void DeleteTask(QuestionBankModel input, ATSDBContext context)
        {
            QuestionBank dataFound = context.QuestionBank.Where(x => x.QId == input.QId).FirstOrDefault();
            if (dataFound != null)
            {
                context.QuestionBank.Remove(dataFound);
                context.SaveChanges();
            }
        }

        public QuestionBankModel Retrieve(QuestionBankModel input)
        {
            QuestionBankModel result = null;
            using (var context = GetConnection())
            {
                try
                {
                    QuestionFactory selector = new QuestionFactory(input.QuesTypeValue);
                    result = selector.QuestionSelector.Select( context,input.QId).FirstOrDefault();
                   
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public List<QuestionBankModel> Select(params object[] inputs)
        {
            List<QuestionBankModel> result = null;
            string quesType = Constants.OPTION;
            using (var context = GetConnection())
            {
                try
                {
                    QuestionFactory selector = new QuestionFactory(quesType);
                    result = selector.QuestionSelector.Select(context, inputs);

                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public bool Update(QuestionBankModel input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuestionFactory quesFactory = new QuestionFactory(input.QuesTypeValue);
                        quesFactory.Question.Update(input, context);
                        dbContextTransaction.Commit();
                        isUpdated = true;

                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
                return isUpdated;
            }
        }

        public void UpdateTask(QuestionBankModel input, ATSDBContext context)
        {
            QuestionBank dataFound = context.QuestionBank.Where(x => x.QId == input.QId).FirstOrDefault();
            if (dataFound != null)
            {
                //updation start
                dataFound.Description = input.Description;
                //updateion end
                context.SaveChanges();
            }
        }
    }
}
