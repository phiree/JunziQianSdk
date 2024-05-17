using System;
using System.Collections.Generic;
using System.Text;

namespace JunziQianSdk.Api.Sign
{
    /// <summary>
    /// Api模板配置.默认读取 appsettings.json
    /// </summary>
    public class TemplateConfig
    {
        /// <summary>
        /// api模板编号
        /// </summary>
        public string TemplateNo { get; set; }
        /// <summary>
        /// 模板对应的业务类型,和 IContract.BusinessTypeName对应.
        /// </summary>
        public string BusinessType { get; set; }
        /// <summary>
        /// 企业方签章位置
        /// </summary>
        public SignPosition EnterprisePosition { get; set; }
        /// <summary>
        /// 其他方签章位置
        /// </summary>
        public SignPosition OtherPosition { get; set; }

        public class SignPosition
        {
            /// <summary>
            /// 签章ID. 企业方必须设置. 乙方无需设置
            /// </summary>
            public long SignId { get; set; }
            /// <summary>
            /// 签章页码, 从0开始
            /// </summary>
            public int PageNo { get; set; }
            /// <summary>
            /// 注意:该值是 百分比,范围[0,1]
            /// 实际调测过程中,可以使用"签章位置工具"大概确定该值.
            /// </summary>
            public float OffsetX { get; set; }
            public float OffsetY { get; set; }
        }
    }
}
