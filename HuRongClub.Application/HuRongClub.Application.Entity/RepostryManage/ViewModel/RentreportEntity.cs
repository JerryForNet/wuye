using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 租户月收费中心
    /// </summary>
    public class RentreportEntity
    {
        /// <summary>
        /// 主列表
        /// </summary>
        public class RentreportListEntity
        {
            /// <summary>
            /// 编号
            /// </summary>
            public string contractid { set; get; }
            /// <summary>
            /// 客户名称
            /// </summary>
            public string customername { set; get; }
            /// <summary>
            /// 房号
            /// </summary>
            public string room_name { set; get; }
            /// <summary>
            /// 入住面积
            /// </summary>
            public decimal rentarea { set; get; }
        }
        /// <summary>
        /// 统计
        /// </summary>
        public class RentreportExtEntity
        {
            /// <summary>
            /// 编号
            /// </summary>
            public string rentcontract_id { set; get; }
            /// <summary>
            /// 费用项目编号
            /// </summary>
            public string feeitem_id { set; get; }
            /// <summary>
            /// 应收
            /// </summary>
            public decimal cshould { set; get; }
            /// <summary>
            /// 实收
            /// </summary>
            public decimal creceive { set; get; }
            /// <summary>
            /// 费用项目名称
            /// </summary>
            public string feeitem_name { set; get; }
        }
    }
}
