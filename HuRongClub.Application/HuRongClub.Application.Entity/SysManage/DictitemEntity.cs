using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.SysManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 20:55
    /// 描 述：字典详情
    /// </summary>
    public class DictitemEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// itemid
        /// </summary>
        /// <returns></returns>
        public string itemid { get; set; }

        /// <summary>
        /// itemname
        /// </summary>
        /// <returns></returns>
        public string itemname { get; set; }

        /// <summary>
        /// dictid
        /// </summary>
        /// <returns></returns>
        public int? dictid { get; set; }

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
            this.itemid = keyValue;
        }

        #endregion 扩展操作
    }
}