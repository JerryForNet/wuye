using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-26 13:42
    /// 描 述：入库汇总
    /// </summary>
    public class InbillItemEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 明细ID
        /// </summary>
        /// <returns></returns>
        public string fitemid { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        /// <returns></returns>
        public string fproviderid { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        /// <returns></returns>
        public Double fnumber { get; set; }

        /// <summary>
        /// 采购价格
        /// </summary>
        /// <returns></returns>
        public Decimal fprice { get; set; }

        /// <summary>
        /// 采购总价
        /// </summary>
        /// <returns></returns>
        public Decimal fmoney { get; set; }

        /// <summary>
        /// 物品ID
        /// </summary>
        /// <returns></returns>
        public string fgoodsid { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        /// <returns></returns>
        public string finbillid { get; set; }

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
            this.fitemid = keyValue;
        }

        #endregion 扩展操作
    }
}