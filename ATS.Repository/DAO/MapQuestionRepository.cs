using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.DAO
{
    public class MapQuestionRepository : BaseRepository, IMapQuestionRepository
    {
        public bool Create(TestQuestionMapModel input)
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
                            Create(ref input, context);
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

        public void Create(ref TestQuestionMapModel input, ATSDBContext context)
        {
            if (input != null)
            {
                input.Id = Guid.NewGuid();
                TestQuestionMapping newData = new TestQuestionMapping
                {
                    Id = input.Id,
                    TestBankId = input.TestBankId,
                    QId = input.QId,
                    Marks = input.Marks
                };
                context.TestQuestionMapping.Add(newData);
                context.SaveChanges();
            }
        }

        public bool Create(List<TestQuestionMapModel> inputs)
        {
            bool isCreated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        for (int indx = 0; indx < inputs.Count; indx++)
                        {
                            var map = inputs[indx];
                            Create(ref map, context);
                        }
                        dbContextTransaction.Commit();
                        isCreated = true;
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            }
            return isCreated;
        }

        public bool Delete(TestQuestionMapModel input)
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

        public void Delete(TestQuestionMapModel input, ATSDBContext context)
        {
            TestQuestionMapping dataFound = context.TestQuestionMapping.Where(x => x.QId == input.QId).FirstOrDefault();
            if (dataFound != null)
            {
                context.TestQuestionMapping.Remove(dataFound);
                context.SaveChanges();
            }
        }

        public bool Delete(List<TestQuestionMapModel> inputs)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        for (int indx = 0; indx < inputs.Count; indx++)
                        {
                            var map = inputs[indx];
                            Delete(map, context);
                        }
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

        public TestQuestionMapModel Retrieve(TestQuestionMapModel input)
        {
            TestQuestionMapModel data = null; ;
            using (var context = GetConnection())
            {
                try
                {
                    data = Select(context, x => x.Id == input.Id).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return data;
            }
        }

        public List<TestQuestionMapModel> Select(Func<TestQuestionMapModel, bool> condition)
        {
            List<TestQuestionMapModel> data = null; ;
            using (var context = GetConnection())
            {
                try
                {
                    data = Select(context, condition);
                }
                catch
                {
                    throw;
                }
                return data;
            }
        }

        public List<TestQuestionMapModel> Select(ATSDBContext context, Func<TestQuestionMapModel, bool> condition)
        {
            List<TestQuestionMapModel> result = null;
            var qry = (from map in context.TestQuestionMapping
                       select new TestQuestionMapModel
                       {
                           Id = map.Id,
                           TestBankId = map.TestBankId,
                           QId = map.QId,
                           Marks = map.Marks
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

        public bool Update(TestQuestionMapModel input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Update(input, context);
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

        public void Update(TestQuestionMapModel input, ATSDBContext context)
        {
            TestQuestionMapping dataFound = context.TestQuestionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
            if (dataFound != null)
            {
                //updation start
                dataFound.Marks = input.Marks;
                //updateion end
                context.SaveChanges();
            }
        }
    }
}
