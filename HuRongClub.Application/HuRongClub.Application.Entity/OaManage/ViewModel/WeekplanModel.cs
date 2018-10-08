using HuRongClub.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.OaManage.ViewModel
{
    /// <summary>
    /// 工作周记页面model
    /// </summary>
    [NotMapped]
    public class WeekplanModel : WeekplanEntity
    {
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string checks { get; set; }
    }
}