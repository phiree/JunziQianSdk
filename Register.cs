using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using JunziQianSdk.Api;
using JunziQianSdk.Api.Sign;
using JunziQianSdk.Infra.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace JunziQianSdk
{
    /// <summary>
    /// 配置和服务注入
    /// </summary>
    public static class RegisterExtension
    {

 
        /// <summary>
        /// 服务注册,使用autofac. 暂不支持内置di.
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="enterpriseName">企业名称,必须和网站一致,用于合同签署</param>
        /// <param name="enterpriseEmail">企业邮箱,同上</param>
        /// <param name="enterpriseCreditId">企业统一社会信用码,同上</param>
        /// <param name="templates">配置</param>
        public static void AddJunziqianSdk(this ContainerBuilder containerBuilder
            , IConfiguration configuration

            )
        {
            string serviceUrl = configuration["Junziqian:ServiceUrl"]
                , appKey = configuration["Junziqian:AppKey"]
                , appSecret = configuration["Junziqian:AppSecret"]
            , enterpriseName = configuration["Junziqian:EnterpriseSignator:Name"]
            , enterpriseEmail = configuration["Junziqian:EnterpriseSignator:Email"]
            , enterpriseCreditId = configuration["Junziqian:EnterpriseSignator:IdCardNo"];
            IList<TemplateConfig> templates = new List<TemplateConfig>();
            configuration.GetSection("Junziqian:Templates").Bind(templates);




            containerBuilder.RegisterType<HttpService>().As<IHttpService>()
                .WithParameter("serviceUrl", serviceUrl)
                .WithParameter("appKey", appKey)
                .WithParameter("appSecret", appSecret)
                ;
            containerBuilder.RegisterType<SignRequestConfigurator>().As<ISignRequestConfigurator>()
                .WithParameter("enterpriseName", enterpriseName)
                .WithParameter("enterpriseCreditId", enterpriseCreditId)
                .WithParameter("enterpriseEmail", enterpriseEmail)

                .WithParameter("templateConfigs", templates)


                ;
            containerBuilder.RegisterType<JunziqianService>().As<IJunziqianService>()
              .WithParameter("templates", templates)
              ;
        }
    }
}
