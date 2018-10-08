using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:01
    /// 描 述：Energy
    /// </summary>
    public class EnergyEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public int? FEnergyID { get; set; }

        /// <summary>
        /// FPropID
        /// </summary>
        /// <returns></returns>
        public string FPropID { get; set; }

        /// <summary>
        /// 消耗日期
        /// </summary>
        /// <returns></returns>
        public DateTime? FDate { get; set; }

        /// <summary>
        /// 用电数
        /// </summary>
        /// <returns></returns>
        public Single? FEletricity { get; set; }

        /// <summary>
        /// 用电数金额
        /// </summary>
        /// <returns></returns>
        public decimal? FEmoney { get; set; }

        /// <summary>
        /// 用水数
        /// </summary>
        /// <returns></returns>
        public Single? FWater { get; set; }

        /// <summary>
        /// 用水数金额
        /// </summary>
        /// <returns></returns>
        public decimal? FWmoney { get; set; }
        /// <summary>
        /// 用油数
        /// </summary>
        /// <returns></returns>
        public Single? FOil { get; set; }

        /// <summary>
        /// 用油数金额
        /// </summary>
        /// <returns></returns>
        public decimal? FOmoney { get; set; }
        /// <summary>
        /// 用气数
        /// </summary>
        /// <returns></returns>
        public Single? FGas { get; set; }

        /// <summary>
        /// 用气数金额
        /// </summary>
        /// <returns></returns>
        public decimal? FGmoney { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        public int? Fuserid { get; set; }

        /// <summary>
        /// 更新时间
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
            int key = Convert.ToInt32(keyValue);
            this.FEnergyID = key;
        }

        #endregion 扩展操作
    }
}