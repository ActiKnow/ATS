using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Helper;
using ATS.Core.Model;
using ATS.Repository.Model;
using ATS.Repository.Repo;
using ATS.Repository.Uow;

namespace ATS.Bll
{
    public class UserInfoBo
    {
        public ApiResult Create(UserInfoModel input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            var flag = false;
            UserInfo userInfo = new UserInfo();

            input.UserId =Guid.NewGuid();

            Utility.CopyEntity(out userInfo, input);

            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    flag = unitOfWork.UserRepo.Create(ref userInfo);

                    if (flag)
                    {
                        input.UserId = userInfo.UserId;

                        UserCredential userCredential = new UserCredential();
                        userCredential.CreatedBy = input.CreatedBy;
                        userCredential.CreatedDate = input.CreatedDate;
                        userCredential.CurrPassword = input.CurrPassword;
                        userCredential.EmailId = input.Email;
                        userCredential.StatusId = input.StatusId;
                        userCredential.UserId = input.UserId;
                        userCredential.Id = Guid.NewGuid();

                        flag = unitOfWork.UserCredentialRepo.Create(ref userCredential);

                        if (flag)
                        {
                            unitOfWork.Commit();

                            Utility.CopyEntity(out input, userInfo);

                            apiResult.Message.Add("User created successfully.");

                            //var result = Select(null);  // Getting all records, when we will pass null in Select method.

                            //if (result != null)
                            //{
                            //    apiResult+= result;
                            //}
                        }
                        else
                        {
                            apiResult.Message.Add("User creation failed.");
                            apiResult.Status = false;
                            unitOfWork.Dispose();
                        }
                    }
                    else
                    {
                        apiResult.Message.Add("User creation failed.");
                        apiResult.Status = false;
                    }
                }
                catch(Exception ex)
                {
                    unitOfWork.Dispose();
                }
            }
            return apiResult;
        }

        public ApiResult Delete(UserInfoModel input)
        {
            ApiResult apiResultDelete = new ApiResult(false, new List<string>());
            var flag = false;
            UserInfo userInfo = new UserInfo();

            Utility.CopyEntity(out userInfo, input);

            using (var unitOfWork = new UnitOfWork())
            {
                flag = unitOfWork.UserRepo.Update(ref userInfo);

                if (flag)
                {
                    UserCredential userCredential = new UserCredential();

                    Utility.CopyEntity(out userCredential, input.UserCredentials);

                    flag = unitOfWork.UserCredentialRepo.Update(ref userCredential);                    
                }
                if (flag)
                {
                    apiResultDelete.Message.Add("User deleted successfully.");

                    var result = Select(null);  // Getting all records, when we will pass null in Select method.

                    if (result != null)
                    {

                        apiResultDelete+= result;
                    }
                }
                else
                {
                    apiResultDelete.Message.Add("User deletion failed.");
                    apiResultDelete.Status = false;
                }
            }
            return apiResultDelete;
        }

        public ApiResult GetById(Guid guid)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            using (var unitOfWork = new UnitOfWork())
            {
                var queryable = unitOfWork.UserRepo.Retrieve(guid);

                UserInfoModel userInfoModel = queryable.FirstOrDefault();

                if (userInfoModel != null)
                {
                    var queryable2= unitOfWork.UserCredentialRepo.Retrieve(guid);
                    var userCredentials= queryable2.FirstOrDefault();
                    userInfoModel.UserCredentials = userCredentials;
                    apiResult.Status = true;
                    apiResult.Data = userInfoModel;
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("No record found for this user.");
                }
            }
            return apiResult;
        }
        
        public ApiResult Select(SimpleQueryModel qry)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());

            using (var unitOfWork = new UnitOfWork())
            {

                SimpleQueryBuilder<UserInfoModel> simpleQry = new SimpleQueryBuilder<UserInfoModel>();

                var queryableList = unitOfWork.UserRepo.Select(simpleQry.GetQuery(query: qry).Compile());

                var userInfoList = queryableList.ToList();

                if (userInfoList != null)
                {
                    apiResult.Data = userInfoList; 
                    apiResult.Status = true;
                }
                else
                {
                    apiResult.Message.Add("No record found");
                    apiResult.Status = false;
                }
            }
            return apiResult;
        }

        public ApiResult Update(UserInfoModel input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            var flag = false;
            UserInfo userInfo = new UserInfo();

            Utility.CopyEntity(out userInfo, input);

            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    flag = unitOfWork.UserRepo.Update(ref userInfo);

                    if (flag)
                    {
                        UserCredential userCredential = new UserCredential();
                        userCredential.CreatedBy = input.CreatedBy;
                        userCredential.CreatedDate = input.CreatedDate;
                        userCredential.CurrPassword = input.CurrPassword;
                        userCredential.EmailId = input.Email;
                        userCredential.StatusId = input.StatusId;
                        userCredential.UserId = input.UserId;

                        flag = unitOfWork.UserCredentialRepo.Update(ref userCredential);
                        if (flag)
                        {
                            unitOfWork.Commit();
                            apiResult.Message.Add("User updated successfully.");

                            var result = Select(null);  // Getting all records, when we will pass null in Select method.

                            if (result != null)
                            {
                                apiResult+= result;
                            }
                        }
                        else
                        {
                            apiResult.Message.Add("User updation failed.");
                            apiResult.Status = false;
                        }
                    }
                    else
                    {
                        apiResult.Message.Add("User updation failed.");
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

        public ApiResult Validate(UserCredentialModel input)
        {
            ApiResult apiResult = new ApiResult(false, new List<string>());
            UserCredential userCredential = new UserCredential();

            Utility.CopyEntity(out userCredential, input);

            using (var unitOfWork = new UnitOfWork())
            {
                var userId = unitOfWork.UserCredentialRepo.ValidateUser(userCredential);

                if (userId != Guid.Empty)
                {
                    apiResult.Status = true;

                    var result = GetById(userId);

                    if (result != null)
                    {
                        apiResult+= result;
                    }
                }
                else
                {
                    apiResult.Status = false;
                    apiResult.Message.Add("Invalid Credentials");
                }
            }
            return apiResult;
        }
    }
}
