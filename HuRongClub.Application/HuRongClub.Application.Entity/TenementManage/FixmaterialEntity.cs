using System;
using System.Collections;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-03 14:52
    /// 描 述：保修维修材料表
    /// </summary>
    public class FixmaterialEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public string pkeyid { get; set; }
        /// <summary>
        /// 保修维修主表编号
        /// </summary>
        /// <returns></returns>
        public string fixreportid { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        /// <returns></returns>
        public string fname { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public int? fnumber { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        public decimal? fprice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        /// <returns></returns>
        public decimal? fmoney { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.pkeyid = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.pkeyid = keyValue;
        }
        #endregion
    }
}
