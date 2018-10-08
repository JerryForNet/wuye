using HuRongClub.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-27 14:25
    /// 描 述：月结账页面model
    /// </summary>
    [NotMapped]
    public class MonthcheckModel : MonthcheckEntity
    {
        /// <summary>
        /// 大类名称
        /// </summary>
        /// <returns></returns>
        public string ftypename { get; set; }
    }
}