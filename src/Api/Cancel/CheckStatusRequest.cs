using System;
using System.Collections.Generic;
using System.Text;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Helper;
using JunziQianSdk.Infra.Request;

namespace JunziQianSdk.Api.GetLink
{
    
     /// <summary>
     /// 撤消合同
     /// </summary>
    public class CancelRequest : BaseRequest
    {
        public CancelRequest(string applyNo)
        {
           this. applyNo = applyNo; 
        }

        public override string ApiPath => "/v2/sign/cancel";

        public string applyNo { get; set; }
    
        public override IDictionary<string, string> BuildForms()
        {
           return new Dictionary<string, string> { {"applyNo",applyNo } };
        }
    }
}
