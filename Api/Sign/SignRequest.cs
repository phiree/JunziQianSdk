using JunziQianSdk.Api.Sign.Consts;
using JunziQianSdk.Infra.Helper;
using JunziQianSdk.Infra.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JunziQianSdk.Api.Sign
{
    public class SignRequest : BaseRequest
    {
        public override string ApiPath => "/v2/sign/applySign";

        /// <summary>
        /// 默认值设定
        /// </summary>
        public SignRequest() { 
            
            
            NoEbqSign=true;
            ServerCA=true;
            FaceThreshold=67;
            }

        [MaxLength(100)]
        public string ContractName { get; set; }
        public IList<Signator> Signatories { get; set; } = new List<Signator>();
        /// <summary>
        /// 企业用户
        /// </summary>

        public bool? ServerCA { get; set; }   
        public DealType DealType { get; set; }
        public int? HashValue { get; set; }
        public int? FileSuffix { get; set; }
        public FileType FileType { get; set; }
        public IFormFile File { get; set; }
        /// <summary>
        /// ofd文件追加内容（0不能追加内容，1允许追加内容），允许追加内容时noEbqSign需要设置为2
        /// </summary>
        public int? AddPage { get; set; }
        public string Url { get; set; }
        public string TemplateNo { get; set; }
        public IList<TemplateParam> TemplateParams { get; set; }
        public string HtmlContent { get; set; }
        public PositionType? PositionType { get; set; }
        public int? FaceThreshold { get; set; }
        public string Complexity { get; set; }
        public bool? OrderFlag { get; set; }
        public SequenceInfo SequenceInfo { get; set; }
        public string NotifyUrl { get; set; }
        public bool? NoEbqSign { get; set; }
        public IList<IFormFile> AttachFiles { get; set; }
        public bool? NeedQifengSign { get; set; }
        public bool? NoBorderSign { get; set; }
        
        public SignRequestModel MapToModel()
        {

            var model = new SignRequestModel();

            model.ContractName = ContractName;
            model.Signatories = JsonConvert.SerializeObject(
                Signatories.Select(x => x.MapToModel())
                , new JsonSerializerSettings
                {
                    ContractResolver
                    = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });
            model.ServerCa = ServerCA == null ? 0 : ServerCA.Value ? 1 : 0;
            model.DealType = (int)DealType;
            model.HashValue = HashValue;
            model.FileSuffix = FileSuffix;
            model.FileType = (int)FileType;
            model.File = File;
            model.AddPage = AddPage;
            model.Url = Url;
            model.TemplateNo = TemplateNo;
            var dict = new Dictionary<string, string>();
            foreach (var tp in TemplateParams)
            {
                dict.Add(tp.Name, tp.Value);
            }
            model.TemplateParams = JsonConvert.SerializeObject(dict);
            model.PositionType = PositionType == null ? 0 : (int)PositionType;
            model.FaceThreshold = FaceThreshold;
            model.Complexity = Complexity;
            model.OrderFlag = OrderFlag == null ? 0 : OrderFlag.Value ? 1 : 0; ;
            model.SequenceInfo = JsonConvert.SerializeObject(SequenceInfo, new JsonSerializerSettings
            {
                ContractResolver
                    = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            model.NotifyUrl = NotifyUrl;
            model.NoEbqSign = NoEbqSign == null ? 0 : NoEbqSign.Value ? 1 : 0; ; ;
            model.AttachFiles = AttachFiles;
            model.NeedQifengSign = NeedQifengSign == null ? 0 : NeedQifengSign.Value ? 1 : 0; ; ;
            model.NoBorderSign = NoBorderSign == null ? 0 : NoBorderSign.Value ? 1 : 0; ; ;

            return model;

        }

        public void CheckParams()
        {

        }

        public override IDictionary<string, string> BuildForms()
        {
            //var kv=this.ToKeyValue();
            //var kv=this.ToDictionary();

            var dictionary = MapToModel().ToDictionary();

            return dictionary;
        }

    }

}
