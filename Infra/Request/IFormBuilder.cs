using System.Collections.Generic;

namespace JunziQianSdk.Infra.Request
{
    /// <summary>
    /// 构造form
    /// </summary>
    public interface IFormBuilder
    {

        IDictionary<string, string> BuildForms();
    }

}
