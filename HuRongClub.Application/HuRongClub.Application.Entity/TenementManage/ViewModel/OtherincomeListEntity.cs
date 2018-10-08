using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 缴费查询
    /// </summary>
    public class OtherincomeListEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string receive_id { set; get; }
        /// <summary>
        /// 类型0 费用应收 1其他应收费
        /// </summary>
        public string feetype { set; get; }
        /// <summary>
        /// 业主/客户
        /// </summary>
        public string customer { set; get; }
        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime? receive_date { set; get; }
        /// <summary>
        /// 收费人
        /// </summary>
        public string userid { set; get; }
        /// <summary>
        /// 收费金额
        /// </summary>
        public decimal? fee_money { set; get; }
        /// <summary>
        /// 票据编号
        /// </summary>
        public string ticket_id { set; get; }
        /// <summary>
        /// 票据号码
        /// </summary>
        public string ticket_code { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string notes { set; get; }
    }
}
