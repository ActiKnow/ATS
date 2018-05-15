using System;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface IUserCredentialRepository : IRepository<UserCredential>
    {
        Guid ValidateUser(UserCredential userCredential);
        IQueryable<UserCredentialModel> Retrieve(Guid userId);
        IQueryable<UserCredentialModel> Select(Func<UserCredentialModel, bool> condition);
    }
}
