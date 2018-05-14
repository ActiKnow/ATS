using System;
using System.Collections.Generic;
using System.Linq;
using ATS.Core.Model;
using ATS.Repository.Model;

namespace ATS.Repository.Interface
{
    public interface ITypeDefRepository : IRepository<TypeDef>
    {
        bool Validate(string typeName, int typeValue);
        IQueryable<TypeDefModel> GetByValue(int value);
        IQueryable<TypeDefModel> Select(Func<TypeDefModel,bool> condition);
    }
}
