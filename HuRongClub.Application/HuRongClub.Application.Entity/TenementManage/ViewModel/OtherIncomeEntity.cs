using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage.ViewModel
{
    public class OtherIncomeEntity
    {
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal check_money { get; set; }

        /// <summary>
        /// 费用项目
        /// </summary>
        public string  fn { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string subject { get; set; }
    }
}
