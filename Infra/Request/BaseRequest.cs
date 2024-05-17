using System.Collections.Generic;
using JunziQianSdk.Infra.Helper;

namespace JunziQianSdk.Infra.Request
{
    public abstract class BaseRequest : IFormBuilder
    {
        public abstract string ApiPath { get; }
        public virtual IDictionary<string, string> BuildForms()
        {
            return this.ToDictionary();
        }
    }

}
