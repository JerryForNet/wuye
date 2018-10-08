using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 导出业主明细
    /// </summary>
    public class ArrearsExportEntity
    {
        /// <summary>
        /// 物业名称
        /// </summary>
        public string property_name { set; get; }
        /// <summary>
        /// 单元名称
        /// </summary>
        public string building_name { set; get; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string room_name { set; get; }
        /// <summary>
        /// 业主名称
        /// </summary>
        public string owner_name { set; get; }
        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal? fee_arrears { set; get; }
        /// <summary>
        /// 应收日期
        /// </summary>
        public DateTime? fee_years { set; get; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string feeitem_name { set; get; }        
    }
    /// <summary>
    /// 导出租户明细
    /// </summary>
    public class ArrearsExportByZHEntity
    {
        /// <summary>
        /// 物业名称
        /// </summary>
        public string property_name { set; get; }
        /// <summary>
        /// 租户
        /// </summary>
        public string customername { set; get; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string feeitem_name { set; get; }
        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal? fee_arrears { set; get; }
        /// <summary>
        /// 应收日期
        /// </summary>
        public DateTime? fee_years { set; get; }
    }
}
