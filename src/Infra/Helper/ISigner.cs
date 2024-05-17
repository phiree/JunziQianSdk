namespace JunziQianSdk.Infra.Helper
{
    /// <summary>
    /// 参数签名
    /// </summary>
    public interface ISigner
    {
        PublicParameters Sign(string encryMethod = "sha256");
    }
}