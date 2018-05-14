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

        //public bool Create(UserInfoModel input)
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
        //                    UserInfo userInfo = new UserInfo();
        //                    userInfo.CreatedBy = input.CreatedBy;
        //                    userInfo.CreatedDate = input.CreatedDate;
        //                    userInfo.Email = input.Email;
        //                    userInfo.FName = input.FName;
        //                    userInfo.LastUpdatedBy = input.LastUpdatedBy;
        //                    userInfo.LastUpdatedDate = input.LastUpdatedDate;
        //                    userInfo.LName = input.LName;
        //                    userInfo.Mobile = input.Mobile;
        //                    userInfo.StatusId = input.StatusId;
        //                    userInfo.UserId = Guid.NewGuid();      
        //                    userInfo.RoleTypeId = input.RoleTypeId;

        //                    context.UserInfo.Add(userInfo);

        //                    context.SaveChanges();

        //                    isCreated = true;
        //                    if (isCreated)
        //                    {
        //                        UserCredential userCredential = new UserCredential();
        //                        userCredential.Id = Guid.NewGuid();
        //                        userCredential.EmailId = input.Email;
        //                        userCredential.CurrPassword = input.UserCredentials[0].CurrPassword;
        //                        userCredential.UserId = userInfo.UserId;
        //                        userCredential.CreatedBy = input.UserCredentials[0].CreatedBy;
        //                        userCredential.CreatedDate = input.UserCredentials[0].CreatedDate;
        //                        context.UserCredential.Add(userCredential);
        //                        context.SaveChanges();
        //                        isCreated = true;
        //                    }
        //                    else
        //                    {
        //                        isCreated = false;
        //                    }
        //                    if (isCreated)
        //                    {
        //                        dbContextTransaction.Commit();
        //                    }
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

        //public bool Delete(UserInfoModel input)
        //{
        //    bool isDeleted = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                UserCredential userCredential = context.UserCredential.AsNoTracking().Where(x => x.UserId == input.UserId).FirstOrDefault();
        //                if (userCredential != null)
        //                {
        //                    context.UserCredential.Remove(userCredential);
        //                    context.SaveChanges();
        //                    isDeleted = true;

        //                    UserInfo userInfo = context.UserInfo.AsNoTracking().Where(x => x.UserId == input.UserId).FirstOrDefault();
        //                    if (userInfo != null && isDeleted == true)
        //                    {
        //                        context.UserInfo.Remove(userInfo);
        //                        context.SaveChanges();
        //                        isDeleted = true;
        //                    }
        //                    else
        //                    {
        //                        isDeleted = false;
        //                    }

        //                }                        
        //                if (isDeleted)
        //                {
        //                    dbContextTransaction.Commit();
        //                }                        

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

        //public UserInfoModel Retrieve(UserInfoModel input)
        //{
        //    UserInfoModel userInfo;
        //    using (var context = GetConnection())
        //    {
        //        try
        //        {
        //            userInfo = Select(context, x => x.UserId == input.UserId).FirstOrDefault();
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return userInfo;
        //    }
        //} 

        //public List<UserInfoModel> Select(Func<UserInfoModel, bool> condition)
        //{
        //    List<UserInfoModel> userInfos = null;
        //    using (var context = GetConnection())
        //    {
        //        try
        //        {
        //            userInfos=Select(context, condition);
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return userInfos;
        //    }
        //}

        //public List<UserInfoModel> Select(ATSDBContext context, Func<UserInfoModel, bool> condition)
        //{
        //    List<UserInfoModel> userInfos = null;

        //    var query = (from x in context.UserInfo.AsNoTracking()
        //                 join y in context.TypeDef on x.RoleTypeId equals y.TypeId
        //                 join z in context.UserCredential on x.UserId equals z.UserId

        //                 select new UserInfoModel
        //                 {
        //                     UserId = x.UserId,
        //                     CreatedBy = x.CreatedBy,
        //                     CreatedDate = x.CreatedDate,
        //                     Email = x.Email,
        //                     FName = x.FName,
        //                     LastUpdatedBy = x.LastUpdatedBy,
        //                     LastUpdatedDate = x.LastUpdatedDate,
        //                     LName = x.LName,
        //                     Mobile = x.Mobile,
        //                     StatusId = x.StatusId,
        //                     //UserTypeId = x.UserTypeId,
        //                     RoleTypeId = x.RoleTypeId,
        //                     RoleDescription = y.Description,
        //                     RoleValue = y.Value,
        //                     CurrPassword=z.CurrPassword                             
        //                 });
        //    if (condition != null)
        //    {
        //        userInfos = query.Where(condition).ToList();
        //    }
        //    else
        //    {
        //        userInfos = query.ToList();
        //    }

        //    return userInfos;

        //}

        //public bool Update(UserInfoModel input)
        //{
        //    bool isUpdated = false;
        //    using (var context = GetConnection())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var userInfo = context.UserInfo.AsNoTracking().Where(x => x.UserId == input.UserId).FirstOrDefault();

        //                if (userInfo != null)
        //                {
        //                    userInfo.LastUpdatedBy = input.LastUpdatedBy;
        //                    userInfo.LastUpdatedDate = input.LastUpdatedDate;
        //                    //userInfo.UserTypeId = input.UserTypeId;
        //                    userInfo.FName = input.FName;
        //                    userInfo.LName = input.LName;
        //                    userInfo.Mobile = input.Mobile;
        //                    userInfo.StatusId = input.StatusId;

        //                    context.SaveChanges();
        //                    dbContextTransaction.Commit();
        //                    isUpdated = true;
        //                }
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

        //public Guid ValidateUser(UserCredentialModel userCredential)
        //{
        //    Guid guid = Guid.Empty;
        //    using (var context = new ATSDBContext())
        //    {
        //        try
        //        {
        //            var userDetail = context.UserCredential.AsNoTracking().Where(x => x.EmailId == userCredential.EmailId && x.CurrPassword == userCredential.CurrPassword).FirstOrDefault();

        //            if (userDetail != null)
        //            {
        //                guid = userDetail.UserId;
        //            }
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        return guid;
        //    }
        //}

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
