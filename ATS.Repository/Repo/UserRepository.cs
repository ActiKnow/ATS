using System;
using System.Collections.Generic;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Core.Helper;
using ATS.Core.Global;

namespace ATS.Repository.Repo
{
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {
        private readonly ATSDBContext _context;
        public UserRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public bool Disable(UserInfoModel userInfoModel)
        {
            bool isDisabled = false;
            try
            {
                var userInfo = _context.UserInfo.Where(x => x.UserId == userInfoModel.UserId).FirstOrDefault();
                if (userInfo != null)
                {
                    userInfo.StatusId = (int)CommonType.DELETED;
                    isDisabled = true;
                }
                else
                {
                     isDisabled = false;
                }
                
            }            
            catch
            {
                throw;
            }
            return isDisabled;
        }

        public IQueryable<UserInfoModel> Retrieve(Guid userId)
        {
            try
            {
                var query = (from x in _context.UserInfo
                             join y in _context.TypeDef on x.RoleTypeValue equals y.Value
                             join z in _context.TypeDef on x.StatusId equals z.Value
                             where x.UserId == userId
                             select new UserInfoModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 Email = x.Email,
                                 FName = x.FName,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 LName = x.LName,
                                 Mobile = x.Mobile,
                                 RoleDescription = y.Description,
                                 StatusId = x.StatusId,
                                 StatusDescription=z.Description,
                                 RoleTypeValue = x.RoleTypeValue,
                                 UserId = x.UserId
                             }).AsQueryable<UserInfoModel>();

                var deletedStatus = (int)CommonType.DELETED;
                return query.Where(x => x.StatusId != deletedStatus);
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<UserInfoModel> Select(Func<UserInfoModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserInfo
                             join y in _context.TypeDef on x.RoleTypeValue equals y.Value
                             join p in _context.TypeDef on x.StatusId equals p.Value
                             select new UserInfoModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 Email = x.Email,
                                 FName = x.FName,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 LName = x.LName,
                                 Mobile = x.Mobile,
                                 RoleDescription = y.Description,
                                 StatusId = x.StatusId,
                                 StatusDescription=p.Description,
                                 RoleTypeValue = x.RoleTypeValue,
                                 UserId = x.UserId,
                                 UserCredentials = (from z in _context.UserCredential.Where(z => z.UserId == x.UserId)
                                                    select new UserCredentialModel
                                                    {
                                                        Id = z.Id,
                                                        UserId = z.UserId
                                                    }).FirstOrDefault(),
                             }).Where(condition).AsQueryable<UserInfoModel>();

                var deletedStatus = (int)CommonType.DELETED;
                return query.Where(x=>x.StatusId!= deletedStatus);
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
                var count = _context.UserInfo.Where(x => x.StatusId != deletedStatus).Count();
                             
                return count;
            }
            catch
            {
                throw;
            }
        }
    }
}
