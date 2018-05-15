using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Model;
using ATS.Repository.Interface;
using ATS.Repository.Model;

namespace ATS.Repository.Repo
{
    public class UserCredentialRepository : Repository<UserCredential>, IUserCredentialRepository
    {
        private ATSDBContext _context;
        public UserCredentialRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }

        public Guid ValidateUser(UserCredential userCredential)
        {
            Guid guid = Guid.Empty;
            try
            {
                var userDetail = _context.UserCredential.AsNoTracking().Where(x => x.EmailId == userCredential.EmailId && x.CurrPassword == userCredential.CurrPassword).FirstOrDefault();

                if (userDetail != null)
                {
                    guid = userDetail.UserId;
                }
            }
            catch
            {
                throw;
            }
            return guid;           
        }

        public IQueryable<UserCredentialModel> Retrieve(Guid userId)
        {
            try
            {
                var query = (from x in _context.UserCredential
                             where x.UserId == userId
                             select new UserCredentialModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 EmailId = x.EmailId,                                
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,                                
                                 StatusId = x.StatusId,
                                 UserId = x.UserId,
                                 Id=x.Id,
                                 CurrPassword=x.CurrPassword,
                                 PrevPassword=x.PrevPassword,
                                  
                             }).AsQueryable<UserCredentialModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<UserCredentialModel> Select(Func<UserCredentialModel, bool> condition)
        {
            try
            {
                var query = (from x in _context.UserCredential                           
                             select new UserCredentialModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 EmailId = x.EmailId,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 StatusId = x.StatusId,
                                 UserId = x.UserId,
                                 Id = x.Id,
                                 CurrPassword = x.CurrPassword,
                                 PrevPassword = x.PrevPassword,
                             }).Where(condition).AsQueryable<UserCredentialModel>();

                return query;
            }
            catch
            {
                throw;
            }
        }

    }
}
