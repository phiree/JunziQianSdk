namespace JunziQianSdk.Api.Sign
{
    /// <summary>
    /// 合同数据接口, 业务对象需要实现此接口,提供创建合同的必要数据
    /// </summary>

    public interface IContract
    {
        string BusinessNameForTemplate { get; }
        string Name { get; }
        string IdCardNo { get; }
        /// <summary>
        /// 签署链接对应的名称: 如 卖家, 号商 等.
        /// </summary>
        string NameForSignUrl { get; }
        string Phone { get; }
        string ContractName { get; }
    }

}
