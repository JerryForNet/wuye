using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 物质库存统计数据
    /// Author:Jerry.Li
    /// </summary>
    [NotMapped]
    public class GoodsinfoReportModel : GoodsinfoEntity
    {
        /// <summary>
        /// 每月库存信息
        /// </summary>
        public List<MonthBill> monthBills;
    }

    /// <summary>
    /// 每月库存信息
    /// </summary>
    public class MonthBill
    {
        /// <summary>
        /// 月份
        /// </summary>
        public int mon;

        /// <summary>
        /// 入库数量
        /// </summary>
        public int monInBillCount;

        /// <summary>
        /// 入库金额
        /// </summary>
        public decimal monInBillPrice;

        /// <summary>
        /// 出库数量
        /// </summary>
        public int monOutBillCount;

        /// <summary>
        /// 出库金额
        /// </summary>
        public decimal monOutBillPrice;
    }
}