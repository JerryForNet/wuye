using HuRongClub.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    [NotMapped]
    public class InbillModel : InbillEntity
    {
        /// <summary>
        /// 列表用户名
        /// </summary>
        public string TrueName { get; set; }
    }
}