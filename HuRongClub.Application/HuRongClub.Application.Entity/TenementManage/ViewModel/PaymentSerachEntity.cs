using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{

    /// <summary>
    /// 减免查询实体
    /// </summary>
    public class PaymentSerachEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string itemid { get; set; }

        /// <summary>
        /// 租赁合同编号
        /// </summary>
        public string contract_id { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        public string room_name { get; set; }

        /// <summary>
        /// 调整前金额
        /// </summary>
        public decimal? source_money { get; set; }

        /// <summary>
        /// 调整后金额
        /// </summary>
        public decimal? new_money { get; set; }

        /// <summary>
        /// 业主
        /// </summary>
        public string bname { get; set; }

        /// <summary>
        /// 费用科目ID
        /// </summary>
        public string feename { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime operatetime { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        public string operatername { get; set; }
        /// <summary>
        /// 应收日期
        /// </summary>
        public DateTime income_date { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo { get; set; }

    }
}
