using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Factory.Question;

namespace ATS.Repository.Repo
{
    public class MapQuestionRepository : Repository<TestQuestionMapping>, IMapQuestionRepository
    {
        private ATSDBContext _context;
        public MapQuestionRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        //public IEnumerable<QuestionBankModel> SelectMappedQuestion(Guid testBankId)
        //{            
        //    try
        //    {
        //    var query = (from x in _context.TestQuestionMapping.AsNoTracking().Where(x=>x.TestBankId == testBankId)
        //                        join y in _context.QuestionBank.AsNoTracking() on x.QId equals y.QId
        //                        join p in _context.TypeDef.AsNoTracking() on y.LevelTypeId equals p.TypeId
        //                        join q in _context.TypeDef.AsNoTracking() on y.CategoryTypeId equals q.TypeId
        //                        join r in _context.TypeDef.AsNoTracking() on y.QuesTypeId equals r.TypeId
        //                        select new QuestionBankModel
        //                        {
        //                            CategoryTypeId=q.TypeId,
        //                            CategoryTypeValue=q.Value,
        //                            CategoryTypeDescription=q.Description,
        //                            LevelTypeId=p.TypeId,
        //                            LevelTypeValue=p.Value,
        //                            LevelTypeDescription=p.Description,
        //                            CreatedBy=y.CreatedBy,
        //                            CreatedDate=y.CreatedDate,
        //                            DefaultMark=y.DefaultMark,
        //                            Description=y.Description,
        //                            LastUpdatedBy=y.LastUpdatedBy,
        //                            LastUpdatedDate=y.LastUpdatedDate,
        //                            QId=x.QId,
        //                            QuesTypeDescription=r.Description,
        //                            QuesTypeId=r.TypeId,
        //                            QuesTypeValue=r.Value,
        //                            StatusId=r.StatusId,
        //                            MappedOptions=(from l in _context.QuestionOptionMapping.Where(x=>x.QId == x.QId)
        //                                            select new QuestionOptionMapModel
        //                                            {
        //                                                OptionKeyId=l.OptionKeyId,
        //                                                Answer=l.Answer,
        //                                                QId=l.QId,
        //                                                Id=l.Id,
        //                                            }),

        //                            Options= (from l in _context.QuestionOptionMapping.Where(x => x.QId == x.QId)
        //                                    join m in _context.QuestionOption on l.OptionKeyId equals m.KeyId
        //                                    select new QuestionOptionModel
        //                                    {                                                   
        //                                        Id = l.Id,
        //                                        Description=m.Description,
        //                                        KeyId=m.KeyId,
        //                                        StatusId=m.StatusId,
        //                                        CreatedBy=m.CreatedBy,
        //                                        CreatedDate=m.CreatedDate,
        //                                        LastUpdatedBy=m.LastUpdatedBy,
        //                                        LastUpdatedDate=m.LastUpdatedDate

        //                                    }),                                      
        //                        }) as IEnumerable<QuestionBankModel>;
        //    return query;
        //    }
        //    catch
        //    {
        //        throw;
        //    }           
        //}       

        //public List<TestQuestionMapModel> SelectMappedQuestion(Func<TestQuestionMapModel, bool> condition)
        //{
        //    List<TestQuestionMapModel> result = null;
        //    var qry = (from map in context.TestQuestionMapping
        //               select new TestQuestionMapModel
        //               {
        //                   Id = map.Id,
        //                   TestBankId = map.TestBankId,
        //                   QId = map.QId,
        //                   Marks = map.Marks
        //               }).AsQueryable();
        //    if (condition != null)
        //    {
        //        result = qry.Where(condition).ToList();
        //    }
        //    else
        //    {
        //        result = qry.ToList();
        //    }
        //    return result;
        //}

        //public bool Update(TestQuestionMapModel input)
        //{
        //    bool isUpdated = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                Update(input, context);
        //                dbContextTransaction.Commit();
        //                isUpdated = true;
        //            }
        //            catch
        //            {
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return isUpdated;
        //    }
        //}

        //public void Update(TestQuestionMapModel input, ATSDBContext context)
        //{
        //    TestQuestionMapping dataFound = context.TestQuestionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
        //    if (dataFound != null)
        //    {
        //        //updation start
        //        dataFound.Marks = input.Marks;
        //        //updateion end
        //        context.SaveChanges();
        //    }
        //}

        public bool MapQuotions(List<TestQuestionMapping> inputs)
        {
            bool isCreated = false;
            try
            {
                for (int indx = 0; indx < inputs.Count; indx++)
                {
                    var map = inputs[indx];
                    isCreated = Create(ref map);
                }
            }
            catch
            {
                throw;
            }
            return isCreated;
        }

        public bool DeleteMappedQuestions(List<TestQuestionMapping> inputs)
        {
            bool isDeleted = false;
            try
            {
                for (int indx = 0; indx < inputs.Count; indx++)
                {
                    var map = inputs[indx];
                    Delete(map);
                }
                isDeleted = true;
            }
            catch
            {
                throw;
            }
            return isDeleted;
        }

        public IQueryable<TestQuestionMapModel> Retrieve(Guid guid)
        {
            try
            {
                var query = (from x in _context.TestQuestionMapping
                             select new TestQuestionMapModel
                             {
                                 Id = x.Id,
                                 Marks = x.Marks,
                                 QId = x.QId,
                                 TestBankId = x.TestBankId
                             }).Where(x => x.Id == guid).AsQueryable<TestQuestionMapModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TestQuestionMapModel> Select(Func<TestQuestionMapModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.TestQuestionMapping
                             select new TestQuestionMapModel
                             {
                                 Id = x.Id,
                                 Marks = x.Marks,
                                 QId = x.QId,
                                 TestBankId = x.TestBankId
                             }).Where(condition).AsQueryable<TestQuestionMapModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

    }
}
