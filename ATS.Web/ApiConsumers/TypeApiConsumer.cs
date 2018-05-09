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

        public static ApiResult RetrieveType(TypeDefModel typeDef)   // Getting TypeDef by using ID
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Retrieve";
                apiResult = ConsumerMethods.Post<List<TypeDefModel>>(url, typeDef);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
        public static ApiResult SelectType(SimpleQueryModel qry)   // Getting TypeDef by using query
        {
            ApiResult apiResult = null;
            try
            {
                string url = "api/TypeDef/Select";
                apiResult = ConsumerMethods.Post<List<TypeDefModel>>(url, qry);
            }
            catch
            {
                throw;
            }
            return apiResult;
        }
    }
}