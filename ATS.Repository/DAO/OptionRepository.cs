using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.DAO
{
    class OptionRepository : BaseRepository, IOptionRepository
    {
        public bool Create(QuestionOptionModel input)
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
                            CreateTask(ref input, context);
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

        public void CreateTask(ref QuestionOptionModel input, ATSDBContext context)
        {
            if (input != null)
            {
                input.Id = Guid.NewGuid();
                QuestionOption option = new QuestionOption {
                    Id= input.Id,
                    KeyId=input.KeyId,
                    Description=input.Description
                };

                context.QuestionOption.Add(option);
                context.SaveChanges();
            }
        }

        public bool Delete(QuestionOptionModel input)
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

        public void DeleteTask(QuestionOptionModel input, ATSDBContext context)
        {
            QuestionOption dataFound = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
            if (dataFound != null)
            {
                context.QuestionOption.Remove(dataFound);
                context.SaveChanges();
            }
        }

        public QuestionOptionModel Retrieve(QuestionOptionModel input)
        {
            QuestionOptionModel result = null;
            using (var context = GetConnection())
            {
                try
                {
                    // result = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public List<QuestionOptionModel> Select(params object[] inputs)
        {
            throw new NotImplementedException();
        }

        public bool Update(QuestionOptionModel input)
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

        public void UpdateTask(QuestionOptionModel input, ATSDBContext context)
        {
            QuestionOption dataFound = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
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
