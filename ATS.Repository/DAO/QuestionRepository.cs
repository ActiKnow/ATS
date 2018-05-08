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

        public void Create(ref QuestionBankModel input, ATSDBContext context)
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
                        Delete(input, context);
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

        public void Delete(QuestionBankModel input, ATSDBContext context)
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
                    result = selector.QuestionSelector.Select(context, x => x.QId == input.QId).FirstOrDefault();

                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public List<QuestionBankModel> Select(Func<QuestionBankModel, bool> condition)
        {
            List<QuestionBankModel> result = null;
            using (var context = GetConnection())
            {
                try
                {
                    var dataFound = Select(context, condition);
                    if (dataFound != null)
                    {
                        result = new List<QuestionBankModel>();
                        foreach (var ques in dataFound)
                        {
                            QuestionFactory selector = new QuestionFactory(ques.QuesTypeValue);
                            result.Add( selector.QuestionSelector.Select(context, x => x.QId == ques.QId).FirstOrDefault());
                        }
                    }
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public List<QuestionBankModel> Select(ATSDBContext context, Func<QuestionBankModel, bool> condition)
        {
            List<QuestionBankModel> result = null;
            var qry = (from ques in context.QuestionBank
                       join t in context.TypeDef on ques.QuesTypeId equals t.TypeId into emptyType
                       from type in emptyType.DefaultIfEmpty()
                       select new QuestionBankModel {
                           QId = ques.QId,
                           Description = ques.Description,
                           QuesTypeId = ques.QuesTypeId,
                           LevelTypeId = ques.LevelTypeId,
                           CategoryTypeId = ques.CategoryTypeId,
                           DefaultMark = ques.DefaultMark,
                           QuesTypeValue =type.Value
                       }).AsQueryable();

            if (condition != null)
            {
                result = qry.Where(condition).ToList();
            }
            else
            {
                result = qry.ToList();
            }
            return result;
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

        public void Update(QuestionBankModel input, ATSDBContext context)
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
