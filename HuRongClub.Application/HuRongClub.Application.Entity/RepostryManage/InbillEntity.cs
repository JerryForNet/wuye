using HuRongClub.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    public class InbillEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 入库单编号
        /// </summary>
        /// <returns></returns>
        [Column("FINBILLID")]
        public string finbillid { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        /// <returns></returns>
        [Column("FINDATE")]
        public DateTime findate { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        /// <returns></returns>
        [Column("FUSERID")]
        public int? fuserid { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        /// <returns></returns>
        [Column("FINPUTDATE")]
        public DateTime finputdate { get; set; }

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
            this.finbillid = keyValue;
        }

        #endregion 扩展操作
    }
}