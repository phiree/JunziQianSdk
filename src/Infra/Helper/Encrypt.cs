using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JunziQianSdk.Infra.Helper
{
    public class Encrypt
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return Convert.ToHexString(hashBytes); // .NET 5 +

                //Convert the byte array to hexadecimal string prior to.NET 5
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
        public static string Hash(string method, string value)
        {
            StringBuilder Sb = new StringBuilder();
            HashAlgorithm hashAlgorithm = null;
            if (method == "sha256")
            {
                hashAlgorithm = SHA256.Create();
            }
            else if (method == "sha1")
            {
                hashAlgorithm = SHA1.Create();
            }
            else if (method == "sha3")
            {
                hashAlgorithm = SHA384.Create();
            }
            else if (method == "md5")
            {
                hashAlgorithm = MD5.Create();
            }
            else
            {
                throw new Exception("尚未实现这种hash方式:" + method);
            }
            using (hashAlgorithm)
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hashAlgorithm.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
