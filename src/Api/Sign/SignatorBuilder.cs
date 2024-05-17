using JunziQianSdk.Api.Sign.Consts;
using JunziQianSdk.Infra.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using static JunziQianSdk.Api.Sign.TemplateConfig;

namespace JunziQianSdk.Api.Sign
{
    public class SignatorBuilder
    {

        Signator signator = null;
        public SignatorBuilder(string fullName, string identityCard)
        {
            signator = new Signator(fullName, identityCard);
        }

        //public SignatorBuilder SetPersonalIdentity(string personalMobile
        //    ,PositionType positionType,int? pageNo,float offsetX,float offsetY
        //    )
        //{
        //    SetIdentityInfo(IdentityType.IdCard, personalMobile, ""
        //        ,positionType,pageNo,offsetX,offsetY);
        //    return this;
        //}
        //public SignatorBuilder SetEnterpriseIdentity(string enterpriseEmail
        //       , PositionType positionType, int? pageNo, float offsetX, float offsetY)
        //{
        //    SetIdentityInfo(IdentityType.UnifyCreditCode,"", enterpriseEmail
        //        , positionType, pageNo, offsetX, offsetY);
        //    return this;
        //}
        /// <summary>
        /// todo: 乙方的签字位置可以从配置文件中读取
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="personalMobile"></param>
        /// <param name="enterpriseEmail"></param>
        /// <param name="signPosition"></param>
        /// <param name="pageNo">签字页面, 从0开始</param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="serverAutoCa">自动签署</param>
        /// <exception cref="PersonalMobileRequired"></exception>
        /// <exception cref="EnterpriseEmailRequired"></exception>
        public SignatorBuilder SetIdentityInfo(IdentityType identityType,
            string personalMobile, string enterpriseEmail, bool? serverAutoCa = false
            )
        {
            signator.IdentityType = identityType;
            if (signator.IsPersonal)
            {
                if (string.IsNullOrEmpty(personalMobile))
                {
                    throw new PersonalMobileRequired();
                }
                signator.Mobile = personalMobile;
                signator.ServerCaAuto = false;

            }
            else
            {
                if (string.IsNullOrEmpty(enterpriseEmail))
                {
                    throw new EnterpriseEmailRequired();
                }

                signator.Email = enterpriseEmail;
                signator.ServerCaAuto = serverAutoCa;

            }

            return this;
        }


        public SignatorBuilder SetOrder(bool signInOrder, int? orderNo)
        {

            if (signInOrder)
            {
                if (orderNo == null)
                {
                    throw new OrderNoRequired();
                }
                if (orderNo < 0 || orderNo >= 100)
                {
                    throw new OrderNoOutOfRange();
                }
                signator.OrderNum = orderNo.Value;
            }
            return this;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionType"></param>
        /// <param name="chaptes"></param>
        /// <param name="chapteName"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        /// <exception cref="ChaptesRequired"></exception>
        /// <exception cref="ChapteNameRequired"></exception>
        /// <exception cref="SearchKeyRequired"></exception>
        public SignatorBuilder SetChapt(PositionType positionType
            , IList<Chapte> chaptes, string chapteName, string searchKey)
        {
            if (positionType == PositionType.Coordinate)
            {
                if (chaptes == null)
                {
                    throw new ChaptesRequired();
                }
                signator.Chaptes = chaptes;
            }
            else if (positionType == PositionType.Form)
            {
                if (string.IsNullOrEmpty(chapteName))
                {
                    throw new ChapteNameRequired();
                }
                signator.ChapteName = chapteName;
            }
            else if (positionType == PositionType.Keyword)
            {
                if (string.IsNullOrEmpty(searchKey))
                {
                    throw new SearchKeyRequired();
                }
                signator.SearchKey = searchKey;
            }
            return this;
        }
        public SignatorBuilder SetAuthLevel(IList<AuthLevel> authLevels)
        {
            signator.AuthLevels = authLevels;
            return this;
        }
        public SignatorBuilder SetVerify(bool need)
        {
            signator.NoNeedVerify = need;
            return this;
        }
        public SignatorBuilder SetSignId(long signId)
        {
            signator.SignId = signId;
            return this;
        }
        public SignatorBuilder SetQiFengOffset(float qiFentOffset)
        {
            signator.QiFengOffset = qiFentOffset;
            return this;
        }
        public Signator Build()
        {
            return signator;
        }
    }

}
