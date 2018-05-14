using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Uow;

namespace ATS.Bll
{
    public class TypeDefBo
    {
        public ApiResult Create(TypeDefModel typeDefModel)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    TypeDef typeDef = new TypeDef();
                    typeDefModel.TypeId = Guid.NewGuid();
                    Utility.CopyEntity(out typeDef, typeDefModel);

                    var flag = unitOfWork.TypeDefRepo.Create(ref typeDef);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Status = true;
                        apiResult.Message.Add(typeDefModel.Description + " created successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult+= result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add(typeDefModel.Description + " creation failed");
                    }
                }
                catch(Exception ex)
                {
                    unitOfWork.Dispose();
                    throw;
                }
            }            
            return apiResult;
        }

        public ApiResult Delete(TypeDefModel typeDefModel)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            TypeDef typeDef = new TypeDef();

            Utility.CopyEntity(out typeDef, typeDefModel);

            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flag = false;
                    flag = unitOfWork.TypeDefRepo.Delete(typeDef);

                    if (flag)
                    {
                        unitOfWork.Commit();
                        apiResult.Message.Add(typeDefModel.Description + " deleted successfully.");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult += result;
                        }
                    }
                    else
                    {
                        apiResult.Message.Add(typeDefModel.Description + " deletion failed.");
                        apiResult.Status = false;
                    }
                }
                catch
                {
                    unitOfWork.Dispose();
                }
            }
            return apiResult;
        }

        public ApiResult GetByValue(int value)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var queryable = unitOfWork.TypeDefRepo.GetByValue(value);

                var typeDefModel = queryable.FirstOrDefault();

                if (typeDefModel != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = typeDefModel;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found for this type");
                }
            }
            return apiResult;
        }

        public ApiResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                SimpleQueryBuilder<TypeDefModel> simpleQry = new SimpleQueryBuilder<TypeDefModel>();
                var queryable= unitOfWork.TypeDefRepo.Select(simpleQry.GetQuery(query: qry).Compile());

                var typeInfo = queryable.ToList();                

                if (typeInfo != null)
                {
                    apiResult.Status = true;
                    apiResult.Data = typeInfo;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found");
                }
            }
            return apiResult;
        }

        public ApiResult Update(TypeDefModel typeDefModel)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    TypeDef typeDef = new TypeDef();

                    Utility.CopyEntity(out typeDef, typeDefModel);

                    var flag = unitOfWork.TypeDefRepo.Update(ref typeDef);

                    if (flag)
                    {
                        unitOfWork.Commit();

                        apiResult.Message.Add(typeDefModel.Description + " updated successfully");

                        var result = Select(null);

                        if (result != null)
                        {
                            apiResult += result;
                        }
                    }
                    else
                    {
                        apiResult.Status = false;
                        apiResult.Message.Add(typeDefModel.Description + " updation failed");
                    }
                }
                catch
                {
                    unitOfWork.Dispose();
                }
            }
            return apiResult;
        }

        public ApiResult Validate(string typeName)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var flag = unitOfWork.TypeDefRepo.Validate(typeName);

                if (flag)
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("Type Name : " + typeName + " already exists.");
                }
                else
                {
                    apiResult.Status = true;
                    apiResult.Data = false;
                }
            }
            return apiResult;
        }
    }
}
