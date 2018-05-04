using System;
using ATS.Core.Model;

namespace ATS.Repository.Interface
{
   public interface IUserRepository : ICRUD<UserInfo>
    {
        Guid ValidateUser(UserCredential userCredential);
    }
}
