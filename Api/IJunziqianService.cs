using JunziQianSdk.Api.GetLink;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Request;
using JunziQianSdk.Infra.Responses;

using System.Threading.Tasks;

namespace JunziQianSdk.Api
{
    public interface IJunziqianService
    {

        Task<string> GetSignLink(GetLinkRequest getLinkRequest);

        Task<SignStatus> CheckSignStatus(string applyNo);
        /// <summary>
        /// 撤销签约
        /// </summary>
        /// <param name="applyNo"></param>
        /// <returns></returns>
        Task<bool> CancelSign(string applyNo);
        Task<string> GetDownloadLink(string applyNo);
        Task<string> Sign(IContract junziqianContract, AbsRequestCreator absSignRequestCreator);
    }
}
