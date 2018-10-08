using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 15:16
    /// 描 述：物品info信息
    /// </summary>
    public class GoodsinfoEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 物品ID
        /// </summary>
        /// <returns></returns>
        public string fgoodsid { get; set; }

        /// <summary>
        /// 最小库存数
        /// </summary>
        /// <returns></returns>
        public Double fmincount { get; set; }

        /// <summary>
        /// 库存价格
        /// </summary>
        /// <returns></returns>
        public Decimal fprice { get; set; }

        /// <summary>
        /// 总价值
        /// </summary>
        /// <returns></returns>
        public Decimal fmoney { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        /// <returns></returns>
        public string fplace { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        /// <returns></returns>
        public int? fuserid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? finputdate { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        public string funit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public Double fcount { get; set; }

        /// <summary>
        /// 最大库存
        /// </summary>
        /// <returns></returns>
        public Double fmaxcount { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string fname { get; set; }

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
            this.fgoodsid = keyValue;
        }

        #endregion 扩展操作
    }
}