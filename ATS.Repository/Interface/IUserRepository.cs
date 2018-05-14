using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Repo;

namespace ATS.Repository.Interface
{
   public interface IUserRepository: IRepository<UserInfo>
    {
        //Guid ValidateUser(UserCredentialModel userCredential);
        IQueryable<UserInfoModel> Select(Func<UserInfoModel, bool> condition);
        IQueryable<UserInfoModel> Retrieve(Guid userId);
    }
}
