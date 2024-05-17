using System.Threading.Tasks;
using JunziQianSdk.Infra.Responses;

namespace JunziQianSdk.Infra.Request
{
    public interface IHttpService
    {

        Task<T> MakeRequest<TRequest,T>(TRequest request)
           
            where TRequest : BaseRequest;


    }
}