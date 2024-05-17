using System;
using System.Collections.Generic;
using System.Text;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Helper;
using JunziQianSdk.Infra.Request;

namespace JunziQianSdk.Api.GetLink
{
    /// <summary>
    /// 获取签署链接
    /// </summary>

    public class GetLinkRequest : BaseRequest
    {
        public GetLinkRequest(string applyNo, string fullName, string identityCard, IdentityType identityType, int? viewMode)
        {
            ApplyNo = applyNo;
            FullName = fullName;
            IdentityCard = identityCard;
            IdentityType = identityType;
            ViewMode = viewMode;
        }

        public override string ApiPath => "/v2/sign/link";

        public string ApplyNo { get; set; }
        public string FullName { get; set; }
        public string IdentityCard { get; set; }
        public IdentityType IdentityType { get; set; }
        public int? ViewMode { get; set; }


        public override IDictionary<string, string> BuildForms()
        {
            var dict = new Dictionary<string, string> {
                {"applyNo",ApplyNo},
                {"fullName",FullName},
                {"identityCard",IdentityCard},
                {"identityType",((int)IdentityType).ToString()}
                };

            if (ViewMode != null)
            {
                dict.Add("viewMode", ViewMode.ToString());
            }

            return dict;

        }
    }
}
