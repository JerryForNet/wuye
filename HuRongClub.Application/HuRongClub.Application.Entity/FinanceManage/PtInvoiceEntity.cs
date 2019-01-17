using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2019-01-17 09:31
    /// 描 述：PtInvoice
    /// </summary>
    public class PtInvoiceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ticket_id
        /// </summary>
        /// <returns></returns>
        public string ticket_id { get; set; }
        /// <summary>
        /// inv_num
        /// </summary>
        /// <returns></returns>
        public string inv_num { get; set; }
        /// <summary>
        /// inputtime
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// inv_date
        /// </summary>
        /// <returns></returns>
        public DateTime? inv_date { get; set; }
        /// <summary>
        /// inv_name
        /// </summary>
        /// <returns></returns>
        public string inv_name { get; set; }
        /// <summary>
        /// inv_money
        /// </summary>
        /// <returns></returns>
        public double? inv_money { get; set; }
        /// <summary>
        /// inv_notes
        /// </summary>
        /// <returns></returns>
        public string inv_notes { get; set; }
        /// <summary>
        /// khdm
        /// </summary>
        /// <returns></returns>
        public string khdm { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ticket_id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ticket_id = keyValue;
                                            }
        #endregion
    }
}