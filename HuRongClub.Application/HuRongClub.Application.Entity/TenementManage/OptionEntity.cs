using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-01 16:42
    /// 描 述：Option
    /// </summary>
    public class OptionEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// property_id
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// option_exchange
        /// </summary>
        /// <returns></returns>
        public decimal? option_exchange { get; set; }
        /// <summary>
        /// option_point
        /// </summary>
        /// <returns></returns>
        public int? option_point { get; set; }
        #endregion

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
                                            }
        #endregion
    }
}