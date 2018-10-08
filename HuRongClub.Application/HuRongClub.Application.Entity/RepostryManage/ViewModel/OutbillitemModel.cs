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
    public class OutbillitemModel : OutbillEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string deptname { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 班组名
        /// </summary>
        public string itemname { get; set; }
    }
}