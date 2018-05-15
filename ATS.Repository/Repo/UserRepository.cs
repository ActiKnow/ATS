using System;
using System.Collections.Generic;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Core.Helper;

namespace ATS.Repository.Repo
{
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {
        private readonly ATSDBContext _context;
        public UserRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public IQueryable<UserInfoModel> Retrieve(Guid userId)
        {
            try
            {
                var query = (from x in _context.UserInfo
                             join y in _context.TypeDef on x.RoleTypeValue equals y.Value
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
                                 RoleTypeValue = x.RoleTypeValue,
                                 UserId = x.UserId
                             }).AsQueryable<UserInfoModel>();

                return query;
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
                                 RoleTypeValue = x.RoleTypeValue,
                                 UserId = x.UserId,
                                 UserCredentials = (from z in _context.UserCredential.Where(z => z.UserId == x.UserId)
                                                    select new UserCredentialModel
                                                    {
                                                        Id = z.Id,
                                                        UserId = z.UserId
                                                    }).FirstOrDefault(),
                             }).Where(condition).AsQueryable<UserInfoModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
