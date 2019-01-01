using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 统计数量用
    /// </summary>
    public class BillReportModel
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public int GoodsId;

        /// <summary>
        /// 总数
        /// </summary>
        public int TotCount;

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotPrice;

        /// <summary>
        /// 月份
        /// </summary>
        public int Month;
    }
}
