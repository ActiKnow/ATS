using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ATS.Core.CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ATS.Web.ApiConsumers
{
    public class ConsumerMethods
    {
        static ConsumerMethods()
        {
            if (string.IsNullOrWhiteSpace(Constants.ApiBaseUrl))
            {
                throw new InvalidOperationException("Invalid or No API Base URL defined");
            }
        }

        public static ApiResult Post<T>(string url, object data)
        {
            ApiResult apiResult = null;
            string type = Constants.Post;
            try
            {
                apiResult= CallApi<T>(url, type, data);
            }
            catch
            {
                throw;
            }

            return apiResult;
        }

        public static ApiResult Get<T>(string url)
        {
            ApiResult apiResult = null;
            string type = Constants.Get;
            try
            {
                apiResult = CallApi<T>(url, type);
            }
            catch
            {
                throw;
            }

            return apiResult;
        }

        public static ApiResult Delete<T>(string url)
        {
            ApiResult apiResult = null;
            string type = Constants.Delete;
            try
            {
                apiResult = CallApi<T>(url, type);
            }
            catch
            {
                throw;
            }

            return apiResult;
        }

        private static ApiResult CallApi<T>(string url,string type, object data=null)
        {
            ApiResult apiResult = null;
           Task<HttpResponseMessage> responseMessage = null;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Constants.ApiBaseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //if (!string.IsNullOrWhiteSpace(Token))
                //{
                //    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", Token));
                //}

                var stringContent = new StringContent("");

                if (type == "POST")
                {
                    stringContent = new StringContent(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    }), Encoding.UTF8, "application/json");

                    responseMessage = client.PostAsync(url, stringContent);
                }

                else if (type=="GET")
                {
                    responseMessage = client.GetAsync(url);
                }
                else if (type == "DELETE")
                {
                    responseMessage = client.DeleteAsync(url);
                }

                //Sending request to find web api REST service resource using HttpClient  
                responseMessage.Wait();

                var result = responseMessage.Result;

                //Storing the response details recieved from web api   
                var Response = result.Content.ReadAsStringAsync().Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (result.IsSuccessStatusCode)
                {
                    //Deserializing the response recieved from web api and storing into the object
                    apiResult = JsonConvert.DeserializeObject<ApiResult>(Response);

                    if (apiResult.Status && apiResult.Data != null)
                    {
                        if (apiResult.Data.GetType() == typeof(JArray))
                        {
                            var respData = ((Newtonsoft.Json.Linq.JArray)apiResult.Data).ToObject<T>();
                            apiResult.Data = respData;
                        }
                        else if (apiResult.Data.GetType() == typeof(JObject))
                        {
                            var respData = ((Newtonsoft.Json.Linq.JObject)apiResult.Data).ToObject<T>();
                            apiResult.Data = respData;
                        }
                    }
                }
                else
                {
                    apiResult = new ApiResult(Response, false, null);
                }
            }
            return apiResult;
        }
    }
}