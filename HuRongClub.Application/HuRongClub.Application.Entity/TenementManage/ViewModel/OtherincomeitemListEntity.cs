using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 其他费用应收明细
    /// </summary>

    [NotMapped]
    public class OtherincomeitemListEntity: OtherincomeEntity
    {
        /// <summary>
        /// 发票编号
        /// </summary>
        public string ticket_code { set; get; }
    }
}
