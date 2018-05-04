using System;
using ATS.Core.Model;

namespace ATS.Repository.Interface
{
   public interface IUserRepository : ICRUD<UserInfo>
    {
        bool RegisterUser();
        Guid ValidateUser(UserCredential userCredential);
        UserInfo GetUserInfo(Guid guid); 
    }
}
