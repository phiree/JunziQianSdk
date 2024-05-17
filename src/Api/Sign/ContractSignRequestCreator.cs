using JunziQianSdk;
using JunziQianSdk.Api.Sign.Consts;
using JunziQianSdk.Infra.Exceptions;
using JunziQianSdk.Infra.Request;
using JunziQianSdk.Infra.Templates;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace JunziQianSdk.Api.Sign
{
    /// <summary>
    ///  创建者,
    /// </summary>
    public class ContractSignRequestCreator : AbsRequestCreator
    {
        IContract trade;
        IList<TemplateConfig> configs;
        ISignRequestConfigurator signRequestConfigurator;
        ITemplateParamAdapter templateParamAdapter;
        public ContractSignRequestCreator(IContract trade,
            ITemplateParamAdapter templateParamAdapter,
            ISignRequestConfigurator signRequestConfigurator,
            IList<TemplateConfig> configs)

        {

            this.trade = trade;

            this.signRequestConfigurator = signRequestConfigurator;
            this.configs = configs;
            this.templateParamAdapter = templateParamAdapter;
        }
        public override BaseRequest CreateRequest()
        {
            string contractName = trade.ContractName;

            var signBuilder = new SignBuilder(contractName, 0,
                null, "", PositionType.Coordinate);

            //根据业务数据 构建个人签署用户
            signBuilder.AddSignator(trade.Name, trade.IdCardNo, IdentityType.IdCard, trade.Phone, ""
               );

            signBuilder.SetDeal(DealType.PartlyAuto, null, null, null);
            var templateNo = configs.Single(x => x.BusinessType == trade.BusinessNameForTemplate).TemplateNo;
            signBuilder.UseTemplate(templateNo, templateParamAdapter.AdaptParams());

            var request = signRequestConfigurator.FillRequest(signBuilder, templateNo);

            return request;

        }
        
    }
}
