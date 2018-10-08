using System;
using System.ComponentModel.DataAnnotations.Schema;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    public class tb_wh_inbill_itemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 明细ID
        /// </summary>
        /// <returns></returns>
        [Column("FITEMID")]
        public string fitemid { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        /// <returns></returns>
        [Column("FPROVIDERID")]
        public string fproviderid { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        /// <returns></returns>
        [Column("FNUMBER")]
        public string fnumber { get; set; }
        /// <summary>
        /// 采购价格
        /// </summary>
        /// <returns></returns>
        [Column("FPRICE")]
        public string fprice { get; set; }
        /// <summary>
        /// 采购总价
        /// </summary>
        /// <returns></returns>
        [Column("FMONEY")]
        public string fmoney { get; set; }
        /// <summary>
        /// 物品ID
        /// </summary>
        /// <returns></returns>
        [Column("FGOODSID")]
        public string fgoodsid { get; set; }
        /// <summary>
        /// 入库单号
        /// </summary>
        /// <returns></returns>
        [Column("FINBILLID")]
        public string finbillid { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.fitemid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.fitemid = keyValue;
                                            }
        #endregion
    }
}