using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.Core.Model;

namespace ATS.Web.ApiConsumers
{
    public class TypeApiConsumer
    {
        public static ApiResult CreateType(TypeDefModel typeDef)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Create";
                apiResult = ConsumerMethods.Post<List<TypeDefModel>>(url, typeDef);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult UpdateType(TypeDefModel typeDef)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Update";
                apiResult = ConsumerMethods.Post<List<TypeDefModel>>(url, typeDef);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult DeleteType(TypeDefModel typeDef)
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Delete";
                apiResult = ConsumerMethods.Post<List<TypeDefModel>>(url, typeDef);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult RetrieveType(int value)   // Getting TypeDef by using ID
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Retrieve/"+ value;
                apiResult = ConsumerMethods.Get<TypeDefModel>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult SelectTypes(SimpleQueryModel query = null)   // Getting TypeDef by using query
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Select";
                apiResult = ConsumerMethods.Post<List<TypeDefModel>>(url, query);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }

        public static ApiResult ValidateType(string typeName,int typeValue)   // Getting TypeDef by using ID
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/ValidateType/"+typeName+"/"+typeValue;
                apiResult = ConsumerMethods.Get<bool>(url);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}