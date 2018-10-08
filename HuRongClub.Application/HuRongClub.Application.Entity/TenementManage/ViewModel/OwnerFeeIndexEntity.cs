using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 客户费用标准
    /// </summary>
    public class OwnerFeeIndexEntity
    {
        /// <summary>
        /// 业主收费ID
        /// </summary>
        public string owner_feeid { set; get; }
        /// <summary>
        /// fee_money
        /// </summary>
        public decimal? fee_money { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_date { set; get; }
        /// <summary>
        /// 业主编号
        /// </summary>
        public string owner_id { set; get; }
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string ownerName { set; get; }
        /// <summary>
        /// 房间编号
        /// </summary>
        public string room_name { set; get; }
        /// <summary>
        /// 费用科目名称
        /// </summary>
        public string feeitem_name { set; get; }
        /// <summary>
        /// 费用科目编号
        /// </summary>
        public string feeitem_id { set; get; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? building_dim { set; get; }
    }
}
