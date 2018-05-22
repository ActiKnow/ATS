using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Global;
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

        public IQueryable<UserFeedback> Retrieve(Guid Id)
        {
            try
            {
                return Select(x=>x.Id==Id);                
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<UserFeedback> Select(Func<UserFeedback, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserFeedback
                             join z in _context.TypeDef on x.StatusId equals z.Value
                             select new UserFeedback
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 StatusId = x.StatusId,
                                 UserId = x.UserId,
                                 Feedback = x.Feedback,
                                 Id = x.Id,
                                 Reating = x.Reating,
                                 UserInfo = (from y in _context.UserInfo
                                             select new UserInfo
                                             {
                                                 Email = y.Email,
                                                 FName = y.FName,
                                                 LName = y.LName,
                                                 Mobile = y.Mobile,
                                                 UserId = y.UserId,
                                             }).FirstOrDefault()
                             }).AsQueryable<UserFeedback>();

                var deletedStatus = (int)CommonType.DELETED;
                return query.Where(x => x.StatusId != deletedStatus);
            }
            catch
            {
                throw;
            }
        }

        public int Count()
        {
            try
            {
                var deletedStatus = (int)CommonType.DELETED;
                var count = _context.UserFeedback.Where(x => x.StatusId != deletedStatus).Count();

                return count;
            }
            catch
            {
                throw;
            }
        }
    }
}
