using System;
using System.Collections.Generic;
using ATS.Core.Model;
using ATS.Repository.Interface;
using System.Linq;

namespace ATS.Repository.DAO
{
    class OptionRepository : BaseRepository, IOptionRepository
    {
        public bool Create(QuestionOption input)
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
                            input.Id = Guid.NewGuid();
                            context.QuestionOption.Add(input);
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

        public bool Delete(QuestionOption input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuestionOption dataFound = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
                        if (dataFound != null)
                        {
                            context.QuestionOption.Remove(dataFound);
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

        public QuestionOption Retrieve(QuestionOption input)
        {
            QuestionOption result;
            using (var context = GetConnection())
            {
                try
                {
                    result = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public ICollection<QuestionOption> Select(params object[] inputs)
        {
            throw new NotImplementedException();
        }

        public bool Update(QuestionOption input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuestionOption dataFound = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
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
