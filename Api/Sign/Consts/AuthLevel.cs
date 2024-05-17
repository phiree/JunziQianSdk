namespace JunziQianSdk.Api.Sign.Consts
{
    public enum AuthLevel
    {
        /// <summary>
        /// 银行卡四要素
        /// </summary>
        BankCardFour = 2,
        /// <summary>
        /// 银行卡三要素
        /// </summary>
        BankCardThree = 10,
        Face = 11,
        SMS = 12,
        /// <summary>
        /// 运营商三要素
        /// </summary>
        OperatorThree = 13
    }

}
