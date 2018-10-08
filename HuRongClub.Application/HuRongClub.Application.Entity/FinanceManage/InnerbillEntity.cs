using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-20 10:13
    /// 描 述：支出费用列表
    /// </summary>
    public class InnerbillEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// billid
        /// </summary>
        /// <returns></returns>
        public string billid { get; set; }

        /// <summary>
        /// deptid
        /// </summary>
        /// <returns></returns>
        public int? deptid { get; set; }

        /// <summary>
        /// ticketnumber
        /// </summary>
        /// <returns></returns>
        public string ticketnumber { get; set; }

        /// <summary>
        /// notes
        /// </summary>
        /// <returns></returns>
        public string notes { get; set; }

        /// <summary>
        /// operater
        /// </summary>
        /// <returns></returns>
        public string operater { get; set; }

        /// <summary>
        /// inputtime
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }

        /// <summary>
        /// moneydate
        /// </summary>
        /// <returns></returns>
        public DateTime? moneydate { get; set; }

        /// <summary>
        /// ifpay
        /// </summary>
        /// <returns></returns>
        public string ifpay { get; set; }

        #endregion 实体成员

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.billid = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.billid = keyValue;
        }

        #endregion 扩展操作
    }
}