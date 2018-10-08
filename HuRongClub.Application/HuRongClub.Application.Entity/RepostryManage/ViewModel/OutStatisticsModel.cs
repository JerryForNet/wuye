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
    public class OutStatisticsModel : OutbillitemEntity
    {
        /// <summary>
        /// 领用时间
        /// </summary>
        public string foutdate { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string fdeptid { get; set; }

        /// <summary>
        /// 名称规格
        /// </summary>
        public string fname { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string funit { get; set; }

        /// <summary>
        /// 领用部门
        /// </summary>
        public string fdeptname { get; set; }
    }
}