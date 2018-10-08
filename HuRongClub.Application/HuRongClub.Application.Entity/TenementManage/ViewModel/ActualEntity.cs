using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 实收查询
    /// </summary>
    [NotMapped]
    public class ActualEntity:FeeincomeEntity
    {
        /// <summary>
        /// 业主/客户
        /// </summary>
        public string owner_name { set; get; }
        /// <summary>
        /// 费用科目
        /// </summary>
        public string feeitem { set; get; }
        /// <summary>
        /// 计费年月
        /// </summary>
        public string incomedate { set; get; }
    }
}
