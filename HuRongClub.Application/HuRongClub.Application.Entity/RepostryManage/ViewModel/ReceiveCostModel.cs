using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 领用费用
    /// </summary>
    public class ReceiveCostModel
    {
        /// <summary>
        /// 费用
        /// </summary>
        public decimal fmoney { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int fdeptid { get; set; }
        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime foutdate { get; set; }
        /// <summary>
        /// 商品类别编号
        /// </summary>
        public string fgoodsid { get; set; }
    }
}
