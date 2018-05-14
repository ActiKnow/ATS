using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.Repo
{
    public class MapOptionRepository : Repository<QuestionOptionMapping>, IMapOptionRepository
    {
        private readonly ATSDBContext _context;
        public MapOptionRepository(ATSDBContext context) : base(context)
        {
            this._context = new ATSDBContext();
        }

        //public bool Create(QuestionOptionMapModel input)
        //{
        //    bool isCreated = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                if (input != null)
        //                {
        //                    Create(input, context);
        //                    dbContextTransaction.Commit();
        //                    isCreated = true;
        //                }
        //            }
        //            catch
        //            {
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return isCreated;
        //    }
        //}
        //public void Create(QuestionOptionMapModel input, ATSDBContext context)
        //{
        //    if (input != null)
        //    {
        //        input.Id = Guid.NewGuid();
        //        QuestionOptionMapping mapQues = new QuestionOptionMapping
        //        {
        //            Id = input.Id,
        //            QId = input.QId,
        //            OptionKeyId = input.OptionKeyId,
        //            Answer = input.Answer
        //        };
        //        context.QuestionOptionMapping.Add(mapQues);
        //        context.SaveChanges();
        //    }
        //}
        //public bool Delete(QuestionOptionMapModel input)
        //{
        //    bool isDeleted = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                Delete(input, context);
        //                dbContextTransaction.Commit();
        //                isDeleted = true;
        //            }
        //            catch
        //            {
        //                dbContextTransaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return isDeleted;
        //    }
        //}
        //public void Delete(QuestionOptionMapModel input, ATSDBContext context)
        //{
        //    QuestionOptionMapping dataFound = null;
        //    dataFound = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
        //    if (dataFound != null)
        //    {
        //        context.QuestionOptionMapping.Remove(dataFound);
        //        context.SaveChanges();
        //    }
        //}
        //public QuestionOptionMapModel Retrieve(QuestionOptionMapModel input)
        //{
        //    QuestionOptionMapModel result = null;
        //    using (var context = GetConnection())
        //    {
        //        try
        //        {
        //            //result = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return result;
        //    }
        //}
        //public List<QuestionOptionMapModel> Select(Func<QuestionOptionMapModel, bool> condition)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<QuestionOptionMapModel> Select(ATSDBContext context, Func<QuestionOptionMapModel, bool> condition)
        //{
        //    List<QuestionOptionMapModel> result = null;
        //    var qry = (from option in context.QuestionOptionMapping.AsNoTracking()
        //               select new QuestionOptionMapModel
        //               {
        //                   Id = option.Id,
        //                   QId = option.QId,
        //                   OptionKeyId = option.OptionKeyId,
        //                   Answer = option.Answer

        //               }).AsQueryable();
        //    result = qry.Where(condition).ToList();
        //    return result;

        //}

        //public bool Update(QuestionOptionMapModel input)
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
        //public void Update(QuestionOptionMapModel input, ATSDBContext context)
        //{
        //    QuestionOptionMapping dataFound = context.QuestionOptionMapping.Where(x => x.Id == input.Id).FirstOrDefault();
        //    if (dataFound != null)
        //    {
        //        //updation start
        //        dataFound.Answer = input.Answer;
        //        //updateion end
        //        context.SaveChanges();
        //    }
        //}

        public IQueryable<QuestionOptionMapModel> Select(Func<QuestionOptionMapModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.QuestionOptionMapping
                             select new QuestionOptionMapModel
                             {
                                 Id = x.Id,
                                 Answer = x.Answer,
                                 OptionKeyId = x.OptionKeyId,
                                 QId = x.QId,

                             }).Where(condition).AsQueryable<QuestionOptionMapModel>();
                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
