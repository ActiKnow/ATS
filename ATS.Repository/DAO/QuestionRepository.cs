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
                            QuestionFactory quesFactory = new QuestionFactory(input.QuesTypeId);
                            quesFactory.Question.Create(input, context);
                            dbContextTransaction.Commit();
                            isCreated = true;
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

        public void CreateTask(QuestionBankModel input, ATSDBContext context)
        {
            if (input != null)
            {
                input.QId = Guid.NewGuid();
                //context.QuestionBank.Add(input);
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
                    // result = context.QuestionBank.Where(x => x.QId == input.QId).FirstOrDefault();
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
            throw new NotImplementedException();
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
                        UpdateTask(input, context);
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
