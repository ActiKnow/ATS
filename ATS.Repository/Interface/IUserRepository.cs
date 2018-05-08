using System;
using System.Collections.Generic;
using ATS.Core.Model;
using ATS.Repository.DAO;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
   public interface IUserRepository: ICRUD<UserInfoModel>
    {
        Guid ValidateUser(UserCredentialModel userCredential);
        List<UserInfoModel> Select(ATSDBContext context, Func<UserInfoModel, bool> condition);
    }
}
