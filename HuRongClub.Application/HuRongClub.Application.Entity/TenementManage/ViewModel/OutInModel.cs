using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage.ViewModel
{
    public class OutInModel
    {
        /// <summary>
        /// 统计金额
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int years { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public int months { get; set; }
    }
}
