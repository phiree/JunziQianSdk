using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Security;
using JunziQianSdk.Infra.Responses;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Helper;

namespace JunziQianSdk.Infra.Request
{
    public class HttpService : IHttpService
    {
        string appKey, appSecret, serviceUrl;
        HttpClient httpClient;
        ILogger<HttpService> logger;

        public HttpService(string serviceUrl, string appKey, string appSecret, HttpClient httpClient, ILogger<HttpService> logger)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.httpClient = httpClient;
            this.serviceUrl = serviceUrl;
            this.logger = logger;

        }


        public async Task<T> MakeRequest<TRequest, T>(TRequest request)
          
            where TRequest : BaseRequest
        {

            var publicParams = new Signer(appKey, appSecret).Sign().BuildForms();
            var businessParams = request.BuildForms();
            var allForms = publicParams.Concat(businessParams);
            var queryString = string.Join("&", publicParams.Select(x => x.Key + "=" + x.Value));
            logger.LogDebug(queryString);

            var formContent = new FormUrlEncodedContent(allForms);

            HttpResponseMessage resp;
            try
            {
                var handler = new HttpClientHandler
                {

                    UseDefaultCredentials = true,
                };
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3| SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                //handler.ClientCertificateOptions= ClientCertificateOption.Automatic;
                httpClient = new HttpClient(handler);
                var url = serviceUrl + request.ApiPath + "?" + queryString;
                var jsonBody = JsonContent.Create(request);
                var formBody = new FormUrlEncodedContent(allForms);
                resp = await httpClient.PostAsync(url, formBody);

                resp.EnsureSuccessStatusCode();
                //resp = await httpClient.SendAsync(httpRequestMessage);
            }

            catch (Exception ex)
            {
                logger.LogError("请求失败:" + ex.ToString());
                throw new Exception($"请求接口错误.url:{serviceUrl + request.ApiPath},ex:{ex.Message}");
            }
            BaseResponse<T> result;
            string strResp = await resp.Content.ReadAsStringAsync();
            try
            {

                result = await resp.Content.ReadFromJsonAsync<BaseResponse<T>>();


            }
            catch (Exception ex)
            {
                string respStr = await resp.Content.ReadAsStringAsync();
                logger.LogError("结果:" + respStr);
                throw new Exception("解析结果错误:" + ex.Message);
            }
            if (!result.success)
            {
                throw new Exception("接口返回错误:" + strResp);
            }
            return result.data;

        }



    }
}
