using System;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
   public interface IUserRepository: ICRUD<UserInfoModel>
    {
        Guid ValidateUser(UserCredentialModel userCredential);
    }
}
