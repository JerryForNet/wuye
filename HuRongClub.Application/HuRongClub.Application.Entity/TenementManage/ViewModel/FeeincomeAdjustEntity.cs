using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 费用调整JSON实体
    /// </summary>
    [NotMapped]
    public class FeeincomeAdjustEntity:FeeincomeEntity
    {
        /// <summary>
        /// 调整后费用
        /// </summary>
        public decimal? price { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { set; get; }
        /// <summary>
        /// 是否打印发票
        /// </summary>
        public string isprint { set; get; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string printname { set; get; }
        /// <summary>
        /// 实收日期
        /// </summary>
        public DateTime? receive_date { set; get; }
        /// <summary>
        /// 发票编号
        /// </summary>
        public string ticket_id { set; get; }
    }
}
