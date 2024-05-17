# 功能及使用

*   功能

    五个功能: 发起签约(基于api模板),获取签约链接,检查签约状态,撤销签约,下载文件
    



        public interface IJunziqianService
        {  
            
            Task<string> Sign(IContract junziqianContract, ITemplateParamAdapter templateParamAdapter);
            Task<string> GetSignLink(GetLinkRequest getLinkRequest);
            Task<SignStatus> CheckSignStatus(string applyNo);
            Task<bool> CancelSign(string applyNo);
            Task<string> GetDownloadLink(string applyNo);
            Task<string> Sign(IContract junziqianContract, ITemplateParamAdapter templateParamAdapter);
        }

*   签约发起

    *   实现IContract

             public class Trade :IContract {
                 
                 public string Name { get;set;}
                 ..........

                 public string BusinessNameForTemplate =>"Trade";

                 public string NameForSignUrl => "卖家";

                 public string ContractName => "交易合同 标的物:" + AccountNo;
             }

    *   实现模板变量绑定

               public class TradeTemplateAdapter : ITemplateParamAdapter
               {
                   Trade trade;
                   public TradeTemplateAdapter(Trade trade)
                   {
                       this.trade = trade;
                   }
                   public IList<TemplateParam> AdaptParams()
                   {
                       //todo: 做成配置
                       return new List<TemplateParam> {

                           new TemplateParam{  Name="双方交易_姓名" , Value=trade.Name },
                           new TemplateParam{  Name="双方交易_身份证" , Value=trade.IdCardNo },
                           new TemplateParam{  Name="双方交易_电话" , Value=trade.Phone},
                           new TemplateParam{  Name="双方交易_游戏账号" , Value=trade.AccountNo },
                           new TemplateParam{  Name="双方交易_单号" , Value=trade.OrderNo },
                           new TemplateParam{  Name="双方交易_游戏名称" , Value=trade.GameName },
                           new TemplateParam{  Name="双方交易_通讯地址" , Value=trade.Address },
                           new TemplateParam{  Name="双方交易_金额" , Value=trade.Amount.ToString() },
                           new TemplateParam{  Name="双方交易_金额大写" , Value=trade.AmountChinese },
                           new TemplateParam{  Name="双方交易_支付宝户名" , Value=trade.AlipayName },
                           new TemplateParam{  Name="双方交易_支付宝账户" , Value=trade.AlipayNo },
                           new TemplateParam{  Name="双方交易_换绑手机" , Value=trade.RebindPhone},

                   };
                   }
               }

    *   传入上述两个参数,返回applyNo

        ```
          IJunziQianService junziQianService;//依赖注入
          
          var applyNo = await junziQianService.Sign(model.ForAutoFlow, templateParamAdapter);

        ```

*   其他四个接口传参简单,不赘述.

# 准备

*   引入nuget包

        dotnet add package JunqiQianSdk

*   配置 appsettings.\[env].json

    ```JSON
    {
      "Junziqian": {
                            //服务配置
        "ServiceUrl": "https://api.sandbox.junziqian.com", 
        "AppKey": "*****************",
        "AppSecret": ""*****************",",
                            //公司信息
        "CorpSignator": {
          "Name": "****",
          "Email": "*****",
          "IdCardNo": "*********"

        },
        
        "AuthLevel": [11], //签署验证方式
       
        "Templates": [     //Api模板设置
          {
         
            "BusinessType": "Trade",     //模板业务类型
          
            "TemplateNo": "*********************",    //模板编号
           
            "EnterprisePosition": {    //企业签章位置
              "PageNo": 5,
              "OffsetX": 0.3676,
              "OffsetY": 0.788,
              "SignId": 316399

            },
    //乙方签章位置
            "OtherPosition": {
              "PageNo": 5,
              "OffsetX": 0.3207,
              "OffsetY": 0.5817

            }
          },
          // 另一个业务模板
          {
            "BusinessType": "Dealer",
            "TemplateNo": "*************",
            "OtherPosition": {
              "PageNo": 4,
              "OffsetX": 0.3377,
              "OffsetY": 0.4192
            },
            // 0.007 约等于 4 毫米
            "EnterprisePosition": {
              "PageNo": 4,
              "OffsetX": 0.3597,
              "OffsetY": 0.3187,
              "SignId": 316399

            }
          }
        ]

      }}
      
    ```

*   使用Autofac注入服务:

    ```C#
    container.AddJunziqianSdk(  IConfiguration configurtion );
    ```



