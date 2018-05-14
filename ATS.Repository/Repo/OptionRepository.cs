using System;
using System.Collections.Generic;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;

namespace ATS.Repository.Repo
{
   public class OptionRepository : Repository<QuestionOption>, IOptionRepository
    {
        private readonly ATSDBContext _context;
        public OptionRepository(ATSDBContext context) : base(context)
        {
            this._context = new ATSDBContext();
        }

        //public bool Create(QuestionOptionModel input)
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
        //                    Create(ref input, context);
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

        //public void Create(ref QuestionOptionModel input, ATSDBContext context)
        //{
        //    if (input != null)
        //    {
        //        input.Id = Guid.NewGuid();
        //        QuestionOption option = new QuestionOption
        //        {
        //            Id = input.Id,
        //            KeyId = input.KeyId,
        //            Description = input.Description
        //        };

        //        context.QuestionOption.Add(option);
        //        context.SaveChanges();
        //    }
        //}

        //public bool Delete(QuestionOptionModel input)
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

        //public void Delete(QuestionOptionModel input, ATSDBContext context)
        //{
        //    QuestionOption dataFound = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
        //    if (dataFound != null)
        //    {
        //        context.QuestionOption.Remove(dataFound);
        //        context.SaveChanges();
        //    }
        //}

        //public QuestionOptionModel Retrieve(QuestionOptionModel input)
        //{
        //    QuestionOptionModel result = null;
        //    using (var context = GetConnection())
        //    {
        //        try
        //        {
        //            result = Select(context, x => x.Id == input.Id).FirstOrDefault();
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return result;
        //    }
        //}
        //public List<QuestionOptionModel> Select(ATSDBContext context, Func<QuestionOptionModel, bool> condition)
        //{
        //    List<QuestionOptionModel> result = null;
        //    result = (from option in context.QuestionOption
        //              select new QuestionOptionModel
        //              {
        //                  Id = option.Id,
        //                  KeyId = option.KeyId,
        //                  Description = option.Description,

        //              }).Where(condition).ToList();
        //    return result;
        //}
        //public List<QuestionOptionModel> Select(Func<QuestionOptionModel, bool> condition)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Update(QuestionOptionModel input)
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

        //public void Update(QuestionOptionModel input, ATSDBContext context)
        //{
        //    QuestionOption dataFound = context.QuestionOption.Where(x => x.Id == input.Id).FirstOrDefault();
        //    if (dataFound != null)
        //    {
        //        //updation start
        //        dataFound.Description = input.Description;
        //        //updateion end
        //        context.SaveChanges();
        //    }
        //}

        public IQueryable<QuestionOptionModel> Select(Func<QuestionOptionModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.QuestionOption
                             select new QuestionOptionModel
                             {
                                 Id = x.Id,
                                 KeyId = x.KeyId,
                                 Description = x.Description,
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 StatusId = x.StatusId
                             }).Where(condition) as IQueryable<QuestionOptionModel>;
                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
