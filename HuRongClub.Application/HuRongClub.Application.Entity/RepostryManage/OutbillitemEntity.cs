using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 11:47
    /// 描 述：领用单对应物品信息
    /// </summary>
    public class OutbillitemEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 出库序号
        /// </summary>
        /// <returns></returns>
        public string fitemid { get; set; }

        /// <summary>
        /// 物品ID
        /// </summary>
        /// <returns></returns>
        public string fgoodsid { get; set; }

        /// <summary>
        /// 领用数量
        /// </summary>
        /// <returns></returns>
        public Double? fnumber { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        /// <returns></returns>
        public Decimal? fprice { get; set; }

        /// <summary>
        /// 领用价值
        /// </summary>
        /// <returns></returns>
        public Decimal? fmoney { get; set; }

        /// <summary>
        /// 出库单编号
        /// </summary>
        /// <returns></returns>
        public string foutbillid { get; set; }

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