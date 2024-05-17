using JunziQianSdk.Api.Sign.Consts;
using JunziQianSdk.Infra.Exceptions;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JunziQianSdk.Api.Sign
{

    public class SignatorModel
    {
        public string FullName { get; set; }
        public int IdentityType { get; set; }
        public string IdentityCard { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public int OrderNum { get; set; }
        public string ChapteJson { get; set; }
        /// <summary>
        /// 签字位置-表单域名ID:positionType=1时必须传入
        /// </summary>
        public string ChapteName { get; set; }
        /// <summary>
        /// 签字位置-按关键字签署，positionType=2时必须传入，关键字支持多个;以英文;分隔
        /// </summary>
        public string SearchKey { get; set; }
        public string[] SearchKeys => string.IsNullOrEmpty(SearchKey) ? new string[] { }
                                    : SearchKey.Split(';');

        public string AuthLevel { get; set; }
        public int NoNeedVerify { get; set; }
        public int? ServerCaAuto { get; set; }
        /// <summary>
        /// 企业用户指定签章ID:此值需为商户上传的自定义公章ID
        /// ，或商户创建的企业的自定义公章ID。
        /// 自定义公章可通过sass或api上传
        /// </summary>
        public long? SignId { get; set; }
        public float? QiFengOffset { get; set; }
    }
    public class Signator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="identityCard"></param>
        /// <param name="serverCaAuto">自动签署</param>
        public Signator(string fullName,
            string identityCard)
        {
            FullName = fullName;


            IdentityCard = identityCard;

        }
        public string FullName { get; set; }
        public IdentityType IdentityType { get; set; }
        public string IdentityCard { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 是否是个人
        /// </summary>
        public bool IsPersonal => IdentityType == IdentityType.Passport
            || IdentityType == IdentityType.IdCard
            || IdentityType == IdentityType.Taiwan
            || IdentityType == IdentityType.HongkongMacao;
        /// <summary>
        /// 签字顺序:连续签（orderNum只是针对于当前合同,对批量签多个合同顺序不能指定）,顺序签时必传,[0,100)
        /// </summary>
        public int OrderNum { get; set; }
        public IList<Chapte> Chaptes { get; set; }
        public string ChapteJson { get; set; }
        /// <summary>
        /// 签字位置-表单域名ID:positionType=1时必须传入
        /// </summary>
        public string ChapteName { get; set; }
        /// <summary>
        /// 签字位置-按关键字签署，positionType=2时必须传入，关键字支持多个;以英文;分隔
        /// </summary>
        public string SearchKey { get; set; }


        public IList<AuthLevel> AuthLevels { get; set; }
        public bool NoNeedVerify { get; set; } = true;
        public bool? ServerCaAuto { get; set; }
        /// <summary>
        /// 企业用户指定签章ID:此值需为商户上传的自定义公章ID
        /// ，或商户创建的企业的自定义公章ID。
        /// 自定义公章可通过sass或api上传
        /// </summary>
        public long? SignId { get; set; }
        public float? QiFengOffset { get; set; }

        /// <summary>
        /// 更新签章信息
        /// </summary>
        /// <param name="signPosition"></param>
        /// <param name="pageNo"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="signId"></param>
        /// <exception cref="ChaptesRequired"></exception>
        public void SetChapte(PositionType? signPosition, int? pageNo, float? offsetX, float? offsetY, long? signId)
        {
            signPosition = signPosition ?? PositionType.Coordinate;
            if (signPosition == PositionType.Coordinate)
            {
                if (pageNo == null || offsetX == null || offsetY == null)
                {

                    throw new ChaptesRequired();
                }
                Chaptes = new List<Chapte> {

                    new Chapte{ Page=pageNo.Value, Chaptes=new List<Chapte._chapte>{
                         new Chapte._chapte{offsetX=offsetX.Value,offsetY=offsetY.Value}
                        } }
                    };
                SignId = signId;

            }
        }

        public SignatorModel MapToModel()
        {

            SignatorModel model = new SignatorModel();
            model.IdentityCard = IdentityCard;
            model.IdentityType = (int)IdentityType;
            model.SignId = SignId;
            model.AuthLevel = JsonConvert.SerializeObject(AuthLevels);
            model.FullName = FullName;
            model.OrderNum = OrderNum;
            model.NoNeedVerify = NoNeedVerify ? 1 : 0;

            model.ServerCaAuto = ServerCaAuto == null ? 0 : ServerCaAuto.Value ? 1 : 0;
            model.QiFengOffset = QiFengOffset;
            model.NoNeedVerify = NoNeedVerify ? 1 : 0;
            model.ChapteJson = JsonConvert.SerializeObject(Chaptes, new JsonSerializerSettings
            {
                ContractResolver
                    = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            model.Email = Email;
            model.ChapteName = ChapteName;
            model.Mobile = Mobile;
            model.SearchKey = SearchKey;


            return model;

        }
    }
    //签章
    public class Chapte
    {
        public int Page { get; set; }
        public IList<_chapte> Chaptes { get; set; }
        public class _chapte
        {
            public float offsetX { get; set; }
            public float offsetY { get; set; }
        }
    }

    /// <summary>
    /// 1-4 是个人签署
    /// 11,12 是企业签署?
    /// </summary>
    public enum IdentityType
    {
        IdCard = 1,
        Passport = 2,
        Taiwan = 3,
        HongkongMacao = 4,
        /// <summary>
        /// 营业执照
        /// </summary>
        BusinessLicense = 11,
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        UnifyCreditCode = 12
    }

}

