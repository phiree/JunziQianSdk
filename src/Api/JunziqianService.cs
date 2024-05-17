using JunziQianSdk.Api.GetLink;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Request;
using JunziQianSdk.Infra.Responses;
using JunziQianSdk.Infra.Templates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JunziQianSdk.Api
{

    public class JunziqianService : IJunziqianService
    {
        IHttpService contractService;
        IList<TemplateConfig> templates;
        ISignRequestConfigurator signRequestFiller;

        public JunziqianService(IHttpService contractService, IList<TemplateConfig> templates, ISignRequestConfigurator signRequestFiller)
        {
            this.contractService = contractService;
            this.templates = templates;
            this.signRequestFiller = signRequestFiller;
        }
        public async Task<string> Sign(IContract junziqianContract, ITemplateParamAdapter templateParamAdapter)
        {
            TemplateConfig template;
            
           var  signRequestCreator=new ContractSignRequestCreator(junziqianContract, templateParamAdapter,signRequestFiller,templates);
            var businessNameForTemplate = junziqianContract.BusinessNameForTemplate;
            try
            {
                template = templates.Single(x => x.BusinessType == businessNameForTemplate);

            }
            catch (Exception ex)
            {

                throw new Exception($"没有对应配置:{businessNameForTemplate}");
            }

            var response = await contractService.MakeRequest<BaseRequest, string>
                 (signRequestCreator.CreateRequest());

            return response;
        }


        public async Task<string> GetSignLink(GetLinkRequest getLinkRequest)
        {
            return await contractService.MakeRequest<GetLinkRequest, string>(getLinkRequest);
        }

        public async Task<SignStatus> CheckSignStatus(string applyNo)
        {
            return await contractService.MakeRequest<CheckStatusRequest, SignStatus>(new CheckStatusRequest(applyNo));
        }

        public async Task<bool> CancelSign(string applyNo)
        {
            //该接口使用外部框架的 success 作为业务执行结果,架构不清晰. 此处返回值无意义.
            return await contractService.MakeRequest<CancelRequest, bool>(new CancelRequest(applyNo));

        }

        public async Task<string> GetDownloadLink(string applyNo)
        {
            return await contractService.MakeRequest<GetDownloadLinkRequest, string>(new GetDownloadLinkRequest(applyNo));

        }

    }
}
