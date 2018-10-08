namespace HuRongClub.Application.Entity.TenementManage.ViewModel
{
    /// <summary>
    /// 合同列表
    /// </summary>
    public class RentcontractTree
    {
        /// <summary>
        /// 合同id
        /// </summary>
        public string contractid { get; set; }

        /// <summary>
        /// 客户名称（拼装）
        /// </summary>
        public string customername { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        public string unitroom { get; set; }

        /// <summary>
        /// 合同单号
        /// </summary>
        public string contractcode { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string shortname { get; set; }
    }
}