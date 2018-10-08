using HuRongClub.Application.Entity.TenementManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.FinanceManage.ViewModel
{
    /// <summary>
    /// 多表视图实体--发票管理
    /// </summary>
    [NotMapped]
    public class FeeticketModel : FeeticketEntity
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string deptname { get; set; }
    }
}