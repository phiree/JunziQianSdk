using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace JunziQianSdk.Api.Sign
{
    public class SignRequestModel
    {
        public string ContractName { get; set; }
        public string Signatories { get; set; }
        public int? ServerCa { get; set; }
        public int DealType { get; set; }
        public int? HashValue { get; set; }
        public int? FileSuffix { get; set; }
        public int FileType { get; set; }
        public IFormFile File { get; set; }
        /// <summary>
        /// ofd文件追加内容（0不能追加内容，1允许追加内容），允许追加内容时noEbqSign需要设置为2
        /// </summary>
        public int? AddPage { get; set; }
        public string Url { get; set; }
        public string TemplateNo { get; set; }
        public string TemplateParams { get; set; }
        public string HtmlContent { get; set; }
        public int? PositionType { get; set; }
        public int? FaceThreshold { get; set; }
        public string Complexity { get; set; }
        public int? OrderFlag { get; set; }
        public string SequenceInfo { get; set; }
        public string NotifyUrl { get; set; }
        public int? NoEbqSign { get; set; }
        public IList<IFormFile> AttachFiles { get; set; }
        public int? NeedQifengSign { get; set; }
        public int? NoBorderSign { get; set; }
    }

}
