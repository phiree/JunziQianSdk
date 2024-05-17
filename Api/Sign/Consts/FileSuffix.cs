using System;
using System.Collections.Generic;
using System.Text;

namespace JunziQianSdk.Api.Sign.Consts
{
    public enum FileSuffix
    {
        Pdf = 0,
        ofd = 1,
        word = 2
    }
    /// <summary>
    /// 合同上传方式
    /// </summary>
    public enum FileType
    {
        UploadDoc = 0,
        FileUrl = 1,
        ApiTemplate = 2,
        UploadHtml = 3,
        ApiTemplatePdf = 4

    }
    /// <summary>
    /// 签章位置类型
    /// null: 不指定位置
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// 坐标位置
        /// </summary>
        Coordinate = 0,
        /// <summary>
        /// 1表单域定位(表单域如果上传为pdf时,需pdf自行定义好表单域,html及url及tmpl等需定义好input标签)
        /// </summary>
        Form = 1,
        Keyword = 2
    }
}
