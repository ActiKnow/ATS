using System;
using System.Collections.Generic;
using ATS.Core.Model;
using ATS.Repository.Interface;
using System.Linq;

namespace ATS.Repository.DAO
{
   public class MapOptionRepository : BaseRepository, IMapOptionRepository
    {
        public bool Create(QuestionOptionMapping input)
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
                            context.QuestionOptionMapping.Add(input);
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

        public bool Delete(QuestionOptionMapping input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuestionOptionMapping dataFound = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
                        if (dataFound != null)
                        {
                            context.QuestionOptionMapping.Remove(dataFound);
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

        public QuestionOptionMapping Retrieve(QuestionOptionMapping input)
        {
            QuestionOptionMapping result;
            using (var context = GetConnection())
            {
                try
                {
                    result = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public List<QuestionOptionMapping> Select(params object[] inputs)
        {
            throw new NotImplementedException();
        }

        public bool Update(QuestionOptionMapping input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        QuestionOptionMapping dataFound = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
                        if (dataFound != null)
                        {
                            //updation start
                            dataFound.Answer = input.Answer;
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
