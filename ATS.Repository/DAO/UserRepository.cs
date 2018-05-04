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
                            context.UserInfo.Add(input);
                            context.SaveChanges();
                            isCreated = true;
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

        public bool Delete(UserInfo input)
        {
            bool isDeleted = false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        UserInfo userInfo = context.UserInfo.Where(x => x.UserId == input.UserId).FirstOrDefault();
                        if (userInfo != null)
                        {
                            context.UserInfo.Remove(userInfo);
                            context.SaveChanges();
                            isDeleted = true;
                            dbContextTransaction.Commit();
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
                    userInfo = context.UserInfo.Where(x => x.UserId == input.UserId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return userInfo;
            }
        }

        public ICollection<UserInfo> Select(params object[] inputs)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserInfo input)
        {
            throw new NotImplementedException();
        }

        public Guid ValidateUser(UserCredential userCredential)
        {
            Guid userId = Guid.Empty;
            using (var context = new ATSDBContext())
            {
                try
                {
                    var userDetail = context.UserCredential.Where(x => x.EmailId == userCredential.EmailId && x.CurrPassword == userCredential.CurrPassword).FirstOrDefault();

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
