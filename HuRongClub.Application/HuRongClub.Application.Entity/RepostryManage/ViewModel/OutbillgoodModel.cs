using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.RepostryManage.ViewModel
{
    /// <summary>
    /// 页面model
    /// </summary>
    [NotMapped]
    public class OutbillgoodModel : OutbillitemEntity
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string fname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string funit { get; set; }
    }
}