using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Model;
using ATS.Repository.Interface;
using ATS.Core.Model;
using ATS.Core.Global;

namespace ATS.Repository.Repo
{
    public class TypeDefRepository : Repository<TypeDef>, ITypeDefRepository
    {
        private ATSDBContext _context;
        public TypeDefRepository(ATSDBContext context) : base(context)
        {
            this._context = context;
        }        

        public bool Validate(string typeName)
        {
            var flag = false;
           try
                {
                    flag = _context.TypeDef.Any(x => x.Description == typeName);
                }
                catch
                {
                    throw;
                }
                return flag;
        }

        public IQueryable<TypeDefModel> GetByValue(int value)
        {
            try
            {
                var query = (from x in _context.TypeDef
                             join y in _context.TypeDef on x.StatusId equals y.Value
                             select new TypeDefModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 Description = x.Description,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 ParentKey = x.ParentKey,
                                 StatusDescription = y.Description,
                                 StatusId = x.StatusId,
                                 TypeId = x.TypeId,
                                 Value = x.Value,
                             }).AsQueryable<TypeDefModel>();

                query = query.Where(x => x.Value == value);
                return query;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TypeDefModel> Select(Func<TypeDefModel,bool> condition)
        {
            try
            {
                var query = (from x in _context.TypeDef
                             join y in _context.TypeDef on x.ParentKey equals y.Value into emptyType
                             from parent in emptyType.DefaultIfEmpty()
                             join z in _context.TypeDef on x.StatusId equals z.Value
                             select new TypeDefModel
                             {
                                 CreatedBy = x.CreatedBy,
                                 CreatedDate = x.CreatedDate,
                                 Description = x.Description,
                                 LastUpdatedBy = x.LastUpdatedBy,
                                 LastUpdatedDate = x.LastUpdatedDate,
                                 ParentKey = x.ParentKey,
                                 StatusDescription = z.Description,
                                 StatusId = x.StatusId,
                                 TypeId = x.TypeId,
                                 Value = x.Value,
                                 ParentDescription = parent.Description
                             }).Where(condition).AsQueryable<TypeDefModel>();
                               
                return query;
            }
            catch
            {
                throw;
            }
        }
    }
}
