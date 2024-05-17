using System;
using System.Collections.Generic;
using System.Text;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Helper;
using JunziQianSdk.Infra.Request;

namespace JunziQianSdk.Api.GetLink
{
    
    /// <summary>
    /// 获取下载链接
    /// </summary>
   
    public class GetDownloadLinkRequest : BaseRequest
    {
        public GetDownloadLinkRequest(string applyNo)
        {
           this. applyNo = applyNo; 
        }

        public override string ApiPath => "/v2/sign/linkFile";

        public string applyNo { get; set; }
    
        public override IDictionary<string, string> BuildForms()
        {
           return new Dictionary<string, string> { {"applyNo",applyNo } };
        }
    }
}
