﻿using System;
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
                        UserInfo dataFound = context.UserInfo.Where(x => x.UserId == input.UserId).FirstOrDefault();
                        if (dataFound != null)
                        {
                            context.UserInfo.Remove(dataFound);
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
            UserInfo result;
            using (var context = GetConnection())
            {
                try
                {
                    result = context.UserInfo.Where(x => x.UserId == input.UserId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
                return result;
            }
        }

        public ICollection<UserInfo> Select(params object[] inputs)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserInfo input)
        {
            bool isUpdated= false;
            using (var context = GetConnection())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        UserInfo dataFound = context.UserInfo.Where(x => x.UserId == input.UserId).FirstOrDefault();
                        if (dataFound != null)
                        {
                            //updation start
                            dataFound.FName = input.FName;
                            //updateion end
                            context.SaveChanges();
                            isUpdated = true;
                            dbContextTransaction.Commit();
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
