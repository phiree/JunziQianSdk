using System.Collections.Generic;
using JunziQianSdk.Infra.Request;

namespace JunziQianSdk.Infra.Helper
{
    /// <summary>
    /// 公共参数
    /// </summary>
    public class PublicParameters : IFormBuilder
    {
        public PublicParameters(string ts, string nonce, string app_key, string sign, string encrypt_method)
        {
            this.ts = ts;
            this.nonce = nonce;
            this.app_key = app_key;
            this.sign = sign;
            this.encrypt_method = encrypt_method;
        }

        public string ts { get; set; }
        public string nonce { get; set; }
        public string app_key { get; set; }
        public string sign { get; set; }
        public string encrypt_method { get; set; }

        public IDictionary<string, string> BuildForms()
        {

            return this.ToDictionary();

            //return new List<KeyValuePair<string, string>> {
            //    new KeyValuePair<string, string>("ts",ts),
            //    new KeyValuePair<string, string>("nonce",nonce),
            //    new KeyValuePair<string, string>("app_key",app_key),
            //    new KeyValuePair<string, string>("sign",sign),
            //    new KeyValuePair<string, string>("encrypt_method",encrypt_method)

            //    };


        }
    }
}
