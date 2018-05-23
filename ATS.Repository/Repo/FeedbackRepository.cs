﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Global;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
   public class FeedbackRepository : Repository<UserFeedback>, IFeedbackRepository
    {
        private readonly ATSDBContext _context;
        public FeedbackRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public bool Disable(List<Guid> listId)
        {
            bool isDeleted = false;
            try
            {
                foreach(var id in listId)
                {
                    var feedBack = _context.UserFeedback.Where(x => x.Id == id).FirstOrDefault();
                    if (feedBack != null)
                    {
                        feedBack.ReadStatus = true;
                        feedBack.StatusId = (int)CommonType.DELETED;
                        isDeleted = true;
                    }
                    else
                    {
                        isDeleted = false;
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
            return isDeleted;
        }

        public IQueryable<UserFeedbackModel> Retrieve(Guid Id)
        {
            try
            {              
                var query=Select(x=>x.Id==Id);
                var query2 =_context.UserFeedback.Where(x => x.Id == Id).FirstOrDefault();
                if (query2!= null){
                    query2.LastUpdatedDate = DateTime.Now;
                    query2.ReadStatus = true;                    
                }
                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<UserFeedbackModel> Select(Func<UserFeedbackModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserFeedback
                             join z in _context.TypeDef on x.StatusId equals z.Value
                             select new UserFeedbackModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 StatusId = x.StatusId,
                                 UserId = x.UserId,
                                 Feedback = x.Feedback,
                                 Id = x.Id,
                                 Rating = x.Rating,
                                 ReadStatus=x.ReadStatus,
                                 userInfoModel = (from y in _context.UserInfo
                                             select new UserInfoModel
                                             {
                                                 Email = y.Email,
                                                 FName = y.FName,
                                                 LName = y.LName,
                                                 Mobile = y.Mobile,
                                                 UserId = y.UserId,
                                             }).FirstOrDefault()
                             }).Where(condition).AsQueryable<UserFeedbackModel>();

                var deletedStatus = (int)CommonType.DELETED;
                return query.Where(x => x.StatusId != deletedStatus);
            }
            catch
            {
                throw;
            }
        }

        public int Count(Func<UserFeedbackModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserFeedback
                             select new UserFeedbackModel
                             {                                
                                 Id=x.Id,
                                 StatusId=x.StatusId,
                                 UserId=x.UserId,
                                 Rating=x.Rating,
                                 ReadStatus=x.ReadStatus,
                                 CreatedDate=x.CreatedDate,
                             }).AsQueryable();

                var count = query.Where(condition).Count();
                return count;
            }
            catch
            {
                throw;
            }
        }
    }
}
