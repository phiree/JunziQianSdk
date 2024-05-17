using System;
using System.Collections.Generic;
using System.Text;

namespace JunziQianSdk.Infra.Responses
{
 
    public class BaseResponse<T>  
    {

        public T data { get; set; }
        public bool success { get; set; }
        public string msg { get; set; }
        public string resultCode { get; set; }
    }
}
