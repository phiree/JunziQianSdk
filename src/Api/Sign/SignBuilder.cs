using JunziQianSdk.Api.Sign.Consts;
using JunziQianSdk.Infra.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace JunziQianSdk.Api.Sign
{

    //每个申请的企业方都是一样的,应该自动加入
    public class SignBuilder
    {


        SignRequest request = new SignRequest();

        /// <summary>
        /// 牛奶过程
        /// </summary>
        /// <param name="signName"></param>
        /// <param name="serverCa"></param>
        /// <param name="signInOrder">按顺序签字</param>
        public SignBuilder(string contractName, int? serverCa,
            bool? signInOrder, string notifyUrl, PositionType? positionType

            )
        {
            request.PositionType = positionType;
            request.FaceThreshold = 67;
            request.ContractName = contractName;
            request.OrderFlag = signInOrder ?? false;
            request.NotifyUrl = notifyUrl;
            





        }
        int needQifengSign;
        int noBorderSign;
        DealType dealType;
        /// <summary>
        /// 签章
        /// </summary>
        /// <param name="dealType"></param>
        /// <param name="fileHashValue"></param>
        /// <param name="qifengSign"></param>
        /// <param name="noBorder"></param>
        /// <exception cref="HashRequired"></exception>
        public SignBuilder SetDeal(DealType dealType, int? fileHashValue, bool? qifengSign, bool? noBorder)
        {
            request.NeedQifengSign = qifengSign ?? false;
            request.NoBorderSign = noBorder ?? false;
            request.DealType = dealType;
            if (dealType == DealType.HashOnlyKeep)
            {
                if (fileHashValue == null)
                {
                    throw new HashRequired();
                }
                request.HashValue = fileHashValue;
            }
            return this;
        }

        public SignBuilder UseTemplate(string templateNo, IList<TemplateParam> templateParams)
        {

            SetFile(FileType.ApiTemplatePdf, null, null,
                templateNo, templateParams, null
                );
            return this;
        }
        /// <summary>
        /// 文件
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="fileSuffix"></param>
        /// <param name="file"></param>
        /// <param name="templateNo"></param>
        /// <param name="templateParams"></param>
        /// <param name="attachFiles"></param>
        /// <exception cref="FileSuffixRequired"></exception>
        /// <exception cref="FileRequired"></exception>
        /// <exception cref="TemplateNoRequired"></exception>
        /// <exception cref="TemplateParamsNotValidJson"></exception>
        /// <exception cref="FileTooLarge"></exception>
        private void SetFile(FileType? fileType, FileSuffix? fileSuffix, IFormFile file,
            string templateNo, IList<TemplateParam> templateParams, FileStream[] attachFiles
            )
        {
            //不能超过30M


            if (dealType != DealType.HashOnlyKeep && (fileType == null || fileType == FileType.UploadDoc))
            {
                if (file == null)
                {
                    throw new FileRequired();
                }
                request.File = file;
            }
            if (fileType == FileType.ApiTemplate || fileType == FileType.ApiTemplatePdf)
            {
                if (string.IsNullOrEmpty(templateNo))
                {
                    throw new TemplateNoRequired();
                }
                if (null == templateParams)
                {

                    throw new TemplateNoRequired();
                }
                request.TemplateNo = templateNo;
                request.TemplateParams = templateParams;
            }

            if (file != null)
            {
                var totalLength = file.Length;
                if (attachFiles != null)
                {
                    foreach (var attachFile in attachFiles)
                    {
                        totalLength += attachFile.Length;
                    }
                }
                if (totalLength > 1024 * 1024 * 30)
                {
                    throw new FileTooLarge();
                }

            }
            request.FileType = fileType.Value;
        }
        /// <summary>
        /// 添加签约方
        /// </summary>

        public SignBuilder AddSignator(Signator signator)
        {
            if (request.Signatories.Any(x => x.IdentityCard == signator.IdentityCard))
            {
                throw new SignatorIdentityCardShouldUnique();
            }
            request.Signatories.Add(signator);
            return this;
        }

        /// <summary>
        /// 添加其他签署方
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idCardNo"></param>
        /// <param name="mobile"></param>

        public void AddSignator(string name, string identityCard,
             IdentityType identityType, string personalMobile, string enterpriseEmail, bool? serverAutoCa = false
            )
        {
            var signatorBuilder = new SignatorBuilder(name, identityCard);
            //null值 将在 filler里面补充完成
            signatorBuilder.SetIdentityInfo(identityType, 
                personalMobile, enterpriseEmail, serverAutoCa
                );
            var signator = signatorBuilder.Build();
            AddSignator(signator);
        }

        public SignRequest Build()
        {

            // todo
            request.CheckParams();
            return request;
        }

    }

}
