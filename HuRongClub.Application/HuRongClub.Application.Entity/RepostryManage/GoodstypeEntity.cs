using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 13:28
    /// 描 述：物品类别
    /// </summary>
    public class GoodstypeEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// ftypecode
        /// </summary>
        /// <returns></returns>
        public string ftypecode { get; set; }

        /// <summary>
        /// fparentcode
        /// </summary>
        /// <returns></returns>
        public string fparentcode { get; set; }

        /// <summary>
        /// frootid
        /// </summary>
        /// <returns></returns>
        public string frootid { get; set; }

        /// <summary>
        /// flayer
        /// </summary>
        /// <returns></returns>
        public int? flayer { get; set; }

        /// <summary>
        /// ftypename
        /// </summary>
        /// <returns></returns>
        public string ftypename { get; set; }

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
            this.ftypecode = keyValue;
        }

        #endregion 扩展操作
    }
}