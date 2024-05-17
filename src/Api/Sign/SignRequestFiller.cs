using JunziQianSdk.Api.Sign.Consts;
using JunziQianSdk.Infra.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JunziQianSdk.Api.Sign
{
    /// <summary>
    ///  根据配置文件, 配置请求
    ///  .企业信息
    ///  .个人和企业的签章位置
    ///  .认证方式
    /// </summary>
    public class SignRequestConfigurator : ISignRequestConfigurator
    {
        string enterpriseName, enterpriseCreditId, enterpriseEmail;
        IList<TemplateConfig> templateConfigs;

        public SignRequestConfigurator(string enterpriseName, string enterpriseCreditId,

             IList<TemplateConfig> templateConfigs, string enterpriseEmail)
        {
            this.enterpriseName = enterpriseName;
            this.enterpriseCreditId = enterpriseCreditId;
            this.templateConfigs = templateConfigs;
            this.enterpriseEmail = enterpriseEmail;
        }

        public SignRequest FillRequest(SignBuilder builder, string templateNo)
        {
            if (templateConfigs.Count(x => x.TemplateNo == templateNo) != 1)
            {
                throw new TemplateConfiguRequired();
            }
            var template = templateConfigs.Single(x => x.TemplateNo == templateNo);

            //企业用户
            builder.AddSignator(enterpriseName, enterpriseCreditId, IdentityType.UnifyCreditCode
                , "", enterpriseEmail, true
                // ,template.EnterprisePosition.PageNo,
                // template.EnterprisePosition.OffsetY,
                //template.EnterprisePosition.OffsetY

                );
            var signRequest = builder.Build();

            //根据配置文件补充签章位置
            foreach (var signator in signRequest.Signatories)
            {
                if (signator.IsPersonal)
                {
                    signator.SetChapte(Consts.PositionType.Coordinate,
                        template.OtherPosition.PageNo,
                        template.OtherPosition.OffsetX,
                        template.OtherPosition.OffsetY,
                        null
                        );
                    signator.AuthLevels = new List<AuthLevel> { AuthLevel.Face };

                }
                else
                {
                    signator.SetChapte(Consts.PositionType.Coordinate,
                        template.EnterprisePosition.PageNo,
                        template.EnterprisePosition.OffsetX,
                        template.EnterprisePosition.OffsetY,
                        template.EnterprisePosition.SignId


                        );

                }

            }


            return builder.Build();
        }
    }

}
