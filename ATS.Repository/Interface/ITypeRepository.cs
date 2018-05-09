using ATS.Core.Model;

namespace ATS.Repository.Interface
{
    public interface ITypeRepository : ICRUD<TypeDefModel>
    {
        bool Validate(string typeName, string typeValue);
    }
}
