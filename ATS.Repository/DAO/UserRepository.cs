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
                    var query = (from x in context.UserInfo.Where(x => x.UserId == input.UserId)
                                 join y in context.TypeDef on x.RoleTypeId equals y.TypeId
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
                                     StatusId = x.StatusId,
                                     UserTypeId = x.UserTypeId,
                                     RoleTypeId = x.RoleTypeId,
                                     RoleDescription = y.Description,
                                     RoleValue = y.Value
                                 });

                    userInfo = query.FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return userInfo;
            }
        }

        public List<UserInfoModel> Select(params object[] inputs)
        {
            List<UserInfoModel> userInfos = null;
            using (var context = GetConnection())
            {
                try
                {
                    var query = (from x in context.UserInfo.AsNoTracking()
                                 join y in context.TypeDef on x.RoleTypeId equals y.TypeId
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
                                     StatusId = x.StatusId,
                                     UserTypeId = x.UserTypeId,
                                     RoleTypeId = x.RoleTypeId,
                                     RoleDescription = y.Description,
                                     RoleValue = y.Value
                                 });

                    userInfos = query.ToList();
                }
                catch
                {
                    throw;
                }
                return userInfos;
            }
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
