using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 员工档案列表MODEL
    /// </summary>
    [NotMapped]
    public class EmployinfoListEntity:EmployinfoEntity
    {
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string zzmm { set; get; }
        /// <summary>
        /// 学历
        /// </summary>
        public string xl { set; get; }
        /// <summary>
        /// 职称
        /// </summary>
        public string zc { set; get; }
        /// <summary>
        /// 技术等级
        /// </summary>
        public string jsdj { set; get; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string gw { set; get; }
        /// <summary>
        /// 职务
        /// </summary>
        public string zw { set; get; }
        /// <summary>
        /// 职级
        /// </summary>
        public string zj { set; get; }
        /// <summary>
        /// 用工来源
        /// </summary>
        public string ygly { set; get; }
        /// <summary>
        /// 部门
        /// </summary>
        public string deptname { set; get; }
    }
}
