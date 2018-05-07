using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.DAO
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
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
                            input.QId = Guid.NewGuid();
                            //context.QuestionBank.Add(input);
                            context.SaveChanges();
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

        public bool Delete(QuestionBankModel input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuestionBank dataFound = context.QuestionBank.Where(x => x.QId == input.QId).FirstOrDefault();
                        if (dataFound != null)
                        {
                            context.QuestionBank.Remove(dataFound);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            isDeleted = true;
                        }
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

        public QuestionBankModel Retrieve(QuestionBankModel input)
        {
            QuestionBankModel result=null;
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
                        QuestionBank dataFound = context.QuestionBank.Where(x => x.QId == input.QId).FirstOrDefault();
                        if (dataFound != null)
                        {
                            //updation start
                            dataFound.Description = input.Description;
                            //updateion end
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            isUpdated = true;
                        }
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
    }
}
