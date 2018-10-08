using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-24 18:47
    /// 描 述：物品领用
    /// </summary>
    public class OutbillEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// foutbillid
        /// </summary>
        /// <returns></returns>
        public string foutbillid { get; set; }

        /// <summary>
        /// fdeptid
        /// </summary>
        /// <returns></returns>
        public int? fdeptid { get; set; }

        /// <summary>
        /// foutdate
        /// </summary>
        /// <returns></returns>
        public DateTime? foutdate { get; set; }

        /// <summary>
        /// fman
        /// </summary>
        /// <returns></returns>
        public string fman { get; set; }

        /// <summary>
        /// fuserid
        /// </summary>
        /// <returns></returns>
        public int? fuserid { get; set; }

        /// <summary>
        /// finputdate
        /// </summary>
        /// <returns></returns>
        public DateTime? finputdate { get; set; }

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
            // 主健只能为 uniqueidentifier 或 int
            this.foutbillid = keyValue;
        }

        #endregion 扩展操作
    }
}