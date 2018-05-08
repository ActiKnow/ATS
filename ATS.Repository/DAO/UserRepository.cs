﻿using System;
using System.Collections.Generic;
using ATS.Repository.Interface;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.DAO
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public bool Create(UserInfoModel input)
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
                            UserInfo userInfo = new UserInfo();
                            userInfo.CreatedBy = input.CreatedBy;
                            userInfo.CreatedDate = input.CreatedDate;
                            userInfo.Email = input.Email;
                            userInfo.FName = input.FName;
                            userInfo.LastUpdatedBy = input.LastUpdatedBy;
                            userInfo.LastUpdatedDate = input.LastUpdatedDate;
                            userInfo.LName = input.LName;
                            userInfo.Mobile = input.Mobile;
                            userInfo.StatusId = input.StatusId;
                            userInfo.UserId = Guid.NewGuid();
                            userInfo.UserTypeId = input.UserTypeId;

                            context.UserInfo.Add(userInfo);

                            context.SaveChanges();
                            
                            isCreated = true;
                            if (isCreated)
                            {
                                UserCredential userCredential = new UserCredential();
                                userCredential.EmailId = input.Email;
                                userCredential.CurrPassword = input.UserCredentials[0].CurrPassword;
                                userCredential.UserId = userInfo.UserId;
                                context.UserCredential.Add(userCredential);
                                context.SaveChanges();
                            }
                            dbContextTransaction.Commit();
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

        public bool Delete(UserInfoModel input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        UserInfo userInfo = context.UserInfo.AsNoTracking().Where(x => x.UserId == input.UserId).FirstOrDefault();
                        if (userInfo != null)
                        {
                            context.UserInfo.Remove(userInfo);
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

        public UserInfoModel Retrieve(UserInfoModel input)
        {
            UserInfoModel userInfo;
            using (var context = GetConnection())
            {
                try
                {
                    userInfo = Select(context, x => x.UserId == input.UserId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return userInfo;
            }
        }

        public List<UserInfoModel> Select(Func<UserInfoModel, bool> condition)
        {
            List<UserInfoModel> userInfos = null;
            using (var context = GetConnection())
            {
                try
                {
                    Select(context, condition);
                }
                catch
                {
                    throw;
                }
                return userInfos;
            }
        }

        public List<UserInfoModel> Select(ATSDBContext context, Func<UserInfoModel, bool> condition)
        {
            List<UserInfoModel> userInfos = null;

            var query = (from x in context.UserInfo.AsNoTracking()
                         join y in context.TypeDef on x.RoleTypeId equals y.TypeId
                         select new UserInfoModel
                         {
                             UserId = x.UserId,
                             CreatedBy = x.CreatedBy,
                             CreatedDate = x.CreatedDate,
                             Email = x.Email,
                             FName = x.FName,
                             LastUpdatedBy = x.LastUpdatedBy,
                             LastUpdatedDate = x.LastUpdatedDate,
                             LName = x.LName,
                             Mobile = x.Mobile,
                             StatusId = x.StatusId,
                             UserTypeId = x.UserTypeId,
                             RoleTypeId = x.RoleTypeId,
                             RoleDescription = y.Description,
                             RoleValue = y.Value
                         });
            if (condition != null)
            {
                userInfos = query.Where(condition).ToList();
            }
            else
            {
                userInfos = query.ToList();
            }

            return userInfos;

        }

        public bool Update(UserInfoModel input)
        {
            bool isUpdated = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var userInfo = context.UserInfo.AsNoTracking().Where(x => x.UserId == input.UserId).FirstOrDefault();

                        if (userInfo != null)
                        {
                            userInfo.LastUpdatedBy = input.LastUpdatedBy;
                            userInfo.LastUpdatedDate = input.LastUpdatedDate;
                            userInfo.UserTypeId = input.UserTypeId;
                            userInfo.FName = input.FName;
                            userInfo.LName = input.LName;
                            userInfo.Mobile = input.Mobile;
                            userInfo.StatusId = input.StatusId;

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

        public Guid ValidateUser(UserCredentialModel userCredential)
        {
            Guid guid = Guid.Empty;
            using (var context = new ATSDBContext())
            {
                try
                {
                    var userDetail = context.UserCredential.AsNoTracking().Where(x => x.EmailId == userCredential.EmailId && x.CurrPassword == userCredential.CurrPassword).FirstOrDefault();

                    if (userDetail != null)
                    {
                        guid = userDetail.UserId;
                    }
                }
                catch
                {
                    throw;
                }
                return guid;
            }
        }

    }
}
