namespace JunziQianSdk.Api.Sign
{
    /// <summary>
    ///  完善请求. 
    ///  
    /// </summary>
    public interface ISignRequestConfigurator
    {
        SignRequest FillRequest(SignBuilder buider, string templateNo);
    }

}
