using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 费用应收表扩展
    /// </summary>
    [NotMapped]
    public class FeeincomeListEntity:FeeincomeEntity
    {
        /// <summary>
        /// 物业组合名称
        /// </summary>
        public string property_name { set; get; }
        /// <summary>
        /// 费用科目
        /// </summary>
        public string feeitem_name { set; get; }
        /// <summary>
        /// 缴费年份
        /// </summary>
        public DateTime fee_years { set; get; }
    }
}
