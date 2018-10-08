using System.ComponentModel.DataAnnotations.Schema;


namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 租赁合同收费标准 扩展实体
    /// </summary>
    [NotMapped]
    public class RentfeeitemListEntity:RentfeeitemEntity
    {
        /// <summary>
        /// 费用科目名称
        /// </summary>
        public string feeitem_name { set; get; }
    }
}
