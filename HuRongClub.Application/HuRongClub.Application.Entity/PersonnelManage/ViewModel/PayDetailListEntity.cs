using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    [NotMapped]
   public class PayDetailListEntity : PaydetailEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string empname { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptname { get; set; }

        /// <summary>
        /// 薪资项
        /// </summary>
        public string dispName { get; set; }


        /// <summary>
        /// 薪资
        /// </summary>
        public decimal amount { get; set; }
    }
}
