using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 17:11
    /// 描 述：费用科目表
    /// </summary>
    public class FeeitemEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// feeitem_id
        /// </summary>
        /// <returns></returns>
        public string feeitem_id { get; set; }

        /// <summary>
        /// feeitem_name
        /// </summary>
        /// <returns></returns>
        public string feeitem_name { get; set; }

        /// <summary>
        /// group_id
        /// </summary>
        /// <returns></returns>
        public string group_id { get; set; }

        /// <summary>
        /// allowreply
        /// </summary>
        /// <returns></returns>
        public bool? allowreply { get; set; }

        /// <summary>
        /// feedispname
        /// </summary>
        /// <returns></returns>
        public string feedispname { get; set; }

        /// <summary>
        /// taxrate
        /// </summary>
        /// <returns></returns>
        public string taxrate { get; set; }

        /// <summary>
        /// taxtype
        /// </summary>
        /// <returns></returns>
        public string taxtype { get; set; }

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
            this.feeitem_id = keyValue;
        }

        #endregion 扩展操作
    }
}