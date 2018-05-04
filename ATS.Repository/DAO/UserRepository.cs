using System;
using System.Collections.Generic;
using ATS.Core.Model;
using ATS.Repository.Interface;
using System.Linq;

namespace ATS.Repository.DAO
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public bool Create(UserInfo input)
        {
            bool isCreated= false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (input != null)
                        {
                            input.UserId = Guid.NewGuid();
                            context.UserInfo.Add(input);
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

        public bool Delete(UserInfo input)
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

        public UserInfo Retrieve(UserInfo input)
        {
            UserInfo userInfo;
            using (var context = GetConnection())
            {
                try
                {
                    userInfo = context.UserInfo.AsNoTracking().Where(x => x.UserId == input.UserId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return userInfo;
            }
        }

        public List<UserInfo> Select(params object[] inputs)
        {
            List<UserInfo> userInfos;
            using (var context = GetConnection())
            {
                try
                {
                    userInfos = context.UserInfo.AsNoTracking().ToList();
                }
                catch
                {
                    throw;
                }
                return userInfos;
            }
        }

        public bool Update(UserInfo input)
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
                            userInfo.Status = input.Status;  

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

        public Guid ValidateUser(UserCredential userCredential)
        {
            Guid userId = Guid.Empty;
            using (var context = new ATSDBContext())
            {
                try
                {
                    var userDetail = context.UserCredential.AsNoTracking().Where(x => x.EmailId == userCredential.EmailId && x.CurrPassword == userCredential.CurrPassword).FirstOrDefault();

                    if (userDetail != null)
                    {
                        userId = userDetail.UserId;
                    }
                }
                catch
                {
                    throw;
                }
                return userId;
            }
        }

    }
}
