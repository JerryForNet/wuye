using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:08
    /// 描 述：发票领用表
    /// </summary>
    public class FeeticketEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 发票编号
        /// </summary>
        /// <returns></returns>
        public string ticket_id { get; set; }

        /// <summary>
        /// 发票类别
        /// </summary>
        /// <returns></returns>
        public string ticket_type { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        /// <returns></returns>
        public string ticket_code { get; set; }

        /// <summary>
        /// 发票状态 0未使用 1已使用 2已归档 10作废
        /// </summary>
        /// <returns></returns>
        public Int16? ticket_status { get; set; }

        /// <summary>
        /// 领用部门ID
        /// </summary>
        /// <returns></returns>
        public string dept_id { get; set; }

        /// <summary>
        /// 领用人
        /// </summary>
        /// <returns></returns>
        public string signname { get; set; }

        /// <summary>
        /// 领用日期
        /// </summary>
        /// <returns></returns>
        public DateTime? signdate { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        /// <returns></returns>
        public DateTime? lasttime { get; set; }

        /// <summary>
        /// 最后操作人
        /// </summary>
        /// <returns></returns>
        public string lastoperate { get; set; }

        #endregion 实体成员

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
            this.ticket_id = keyValue;
        }

        #endregion 扩展操作
    }
}