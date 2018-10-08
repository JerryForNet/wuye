using System.Collections.Generic;

namespace HuRongClub.CodeGenerator.Model
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.1.15 9:54
    /// 描 述：表格信息
    /// </summary>
    public class GridModel
    {
        /// <summary>
        /// 工具栏按钮[刷新、新增、编辑、删除]
        /// </summary>
        public List<string> ButtonList { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPage { get; set; }

        /// <summary>
        /// 显示行号
        /// </summary>
        public bool Rownumbers { get; set; }
    }
}