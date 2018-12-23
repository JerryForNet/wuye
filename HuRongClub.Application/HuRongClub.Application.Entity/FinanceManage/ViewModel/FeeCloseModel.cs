using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.FinanceManage.ViewModel
{
    /// <summary>
    /// 账单关闭实体
    /// </summary>
    [NotMapped]
    public class FeeCloseModel : FeeCloseEntity
    {
        /// <summary>
        /// 物业名称
        /// </summary>
        public string propertyName { get; set; }
    }
}