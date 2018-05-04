using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Repository.DBContext;
using System.Linq;

namespace ATS.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserInfo GetUserInfo(Guid userId)
        {
            UserInfo userInfo;

            using (var context = new ATSDBContext())
            {
                try
                {
                    userInfo= context.UserInfoes.Where(x => x.UserId == userId).FirstOrDefault();                                     
                }
                catch
                {
                    throw;
                }
                return userInfo;
            }
        }

        public bool RegisterUser()
        {
            throw new NotImplementedException();
        }

        public Guid ValidateUser(UserCredential userCredential)
        {
            Guid userId = Guid.Empty;
            using (var context=new ATSDBContext())
            {
                try
                {
                    var userDetail = context.UserCredentials.Where(x => x.EmailId == userCredential.EmailId && x.CurrPassword == userCredential.CurrPassword).FirstOrDefault();

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

        bool ICRUD<UserInfo>.Create(UserInfo input)
        {
            throw new NotImplementedException();
        }

        bool ICRUD<UserInfo>.Delete(UserInfo input)
        {
            throw new NotImplementedException();
        }

        UserInfo IUserRepository.GetUserInfo(Guid guid)
        {
            throw new NotImplementedException();
        }

        bool IUserRepository.RegisterUser()
        {
            throw new NotImplementedException();
        }

        ICollection<UserInfo> ICRUD<UserInfo>.Retrieve(UserInfo input)
        {
            throw new NotImplementedException();
        }

        bool ICRUD<UserInfo>.Update(UserInfo input)
        {
            throw new NotImplementedException();
        }

        Guid IUserRepository.ValidateUser(UserCredential userCredential)
        {
            throw new NotImplementedException();
        }
    }
}
