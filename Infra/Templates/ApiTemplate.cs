using System;
using System.Collections.Generic;
using System.Text;
using JunziQianSdk.Api.Sign;

namespace JunziQianSdk.Infra.Templates
{

    public interface ITemplateParamAdapter
    {

        IList<TemplateParam> AdaptParams();
    }

}
