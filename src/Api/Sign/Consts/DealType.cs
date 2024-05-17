namespace JunziQianSdk.Api.Sign.Consts
{
    public enum DealType
    {
        /// <summary>
        /// 手签合同,用户有感知
        /// </summary>
        ManualSign = 0,
        /// <summary>
        /// 只保全?
        /// </summary>
        OnlyKeep = 2,
        /// <summary>
        /// 部分自动
        /// </summary>
        PartlyAuto = 5,
        /// <summary>
        /// hash只保全?
        /// </summary>
        HashOnlyKeep = 6,
        /// <summary>
        /// 收集信息批量签
        /// </summary>
        BatchSign = 17
    }

}
