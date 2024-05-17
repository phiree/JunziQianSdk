using System;
using System.Security.Cryptography;
using System.Text;

namespace JunziQianSdk.Infra.Helper
{
    /// <summary>
    /// 参数签名
    /// </summary>
    public class Signer : ISigner
    {
        string appKey, appSecret;


        public Signer(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        public PublicParameters Sign(string encryMethod = "sha256")
        {
            //var ts = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var ts = ((DateTimeOffset)DateTime.Now.ToUniversalTime()).ToUnixTimeMilliseconds().ToString();
            string nonce = Encrypt.Hash("md5", ts);
            string signSrc = "nonce" + nonce + "ts" + ts + "app_key" + appKey + "app_secret" + appSecret;

            var sign = Encrypt.Hash(encryMethod, signSrc);
            return new PublicParameters(ts, nonce, appKey, sign, encryMethod);

        }

    }
}
