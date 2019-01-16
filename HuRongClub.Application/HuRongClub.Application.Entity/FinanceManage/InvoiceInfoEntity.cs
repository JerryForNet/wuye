using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：Jerry.Li
    /// 日 期：2019-01-16 21:36
    /// 描 述：InvoiceInfo
    /// </summary>
    public class InvoiceInfoEntity : BaseEntity
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