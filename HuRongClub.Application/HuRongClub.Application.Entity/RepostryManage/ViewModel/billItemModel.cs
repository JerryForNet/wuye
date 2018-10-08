using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    public class billItemModel
    {
        /// <summary>
        /// 材料编号
        /// </summary>
        public string fgoodsid { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int fnumber { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal fmoney { get; set; }

        /// <summary>
        /// 名称规格
        /// </summary>
        public string fname { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string funit { get; set; }
    }
}