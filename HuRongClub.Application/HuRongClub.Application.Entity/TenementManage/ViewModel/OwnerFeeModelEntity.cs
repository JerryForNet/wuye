using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 业主费用扩展实体
    /// </summary>
    [NotMapped]
    public class OwnerFeeModelEntity:Owner_feeEntity
    {
        /// <summary>
        /// 业主姓名
        /// </summary>
        public string owner_name { set; get; }

        /// <summary>
        /// 费用类型
        /// </summary>
        public string feeitem_name { set; get; }
    }
}
