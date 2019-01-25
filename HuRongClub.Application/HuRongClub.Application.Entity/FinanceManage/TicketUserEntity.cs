namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2019-01-20 20:54
    /// 描 述：TicketUser
    /// </summary>
    public class TicketUserEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// khdm
        /// </summary>
        /// <returns></returns>
        public string khdm { get; set; }

        /// <summary>
        /// khmc
        /// </summary>
        /// <returns></returns>
        public string khmc { get; set; }

        /// <summary>
        /// khsh
        /// </summary>
        /// <returns></returns>
        public string khsh { get; set; }

        /// <summary>
        /// khdz
        /// </summary>
        /// <returns></returns>
        public string khdz { get; set; }

        /// <summary>
        /// khkhyhzh
        /// </summary>
        /// <returns></returns>
        public string khkhyhzh { get; set; }

        /// <summary>
        /// vdef1
        /// </summary>
        /// <returns></returns>
        public string vdef1 { get; set; }

        /// <summary>
        /// vdef2
        /// </summary>
        /// <returns></returns>
        public string vdef2 { get; set; }

        /// <summary>
        /// vdef3
        /// </summary>
        /// <returns></returns>
        public string vdef3 { get; set; }

        /// <summary>
        /// vdef4
        /// </summary>
        /// <returns></returns>
        public string vdef4 { get; set; }

        #endregion

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            // 主健只能为 uniqueidentifier 或 int
        }

        #endregion
    }
}