using System;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface IUserCredentialRepository : IRepository<UserCredential>
    {
        Guid ValidateUser(UserCredential userCredential);
    }
}
