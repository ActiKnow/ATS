using System;
using System.Collections.Generic;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Interface
{
   public interface IUserRepository
    {
        bool RegisterUser();
        Guid ValidateUser(UserCredential userCredential);
        UserInfo GetUserInfo(Guid guid); 
    }
}
