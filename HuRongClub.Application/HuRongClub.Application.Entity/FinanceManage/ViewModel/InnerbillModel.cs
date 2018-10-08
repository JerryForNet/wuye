using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.FinanceManage.ViewModel
{
    /// <summary>
    /// 视图Model--费用支出详情
    /// </summary>
    [NotMapped]
    public class InnerbillModel : InnerbillEntity
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string deptname { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double feemoney { get; set; }

        /// <summary>
        /// 费用详情的子表
        /// </summary>
        public IList<InnerbillitemEntity> billitems { get; set; }
    }
}