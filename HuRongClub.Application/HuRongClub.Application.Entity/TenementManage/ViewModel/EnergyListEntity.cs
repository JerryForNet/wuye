using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 能源消耗
    /// </summary>
    [NotMapped]
    public class EnergyListEntity: EnergyEntity
    {
        /// <summary>
        /// 小计
        /// </summary>
        public decimal Subtotal { set; get; }
    }
}
