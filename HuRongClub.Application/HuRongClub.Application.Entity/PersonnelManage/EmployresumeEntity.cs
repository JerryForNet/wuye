using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 16:33
    /// 描 述：员工履历信息
    /// </summary>
    public class EmployresumeEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// itemid
        /// </summary>
        /// <returns></returns>
        public int? itemid { get; set; }

        /// <summary>
        /// duration
        /// </summary>
        /// <returns></returns>
        public string duration { get; set; }

        /// <summary>
        /// workcompany
        /// </summary>
        /// <returns></returns>
        public string workcompany { get; set; }

        /// <summary>
        /// job
        /// </summary>
        /// <returns></returns>
        public string job { get; set; }

        /// <summary>
        /// empid
        /// </summary>
        /// <returns></returns>
        public int? empid { get; set; }

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
            int _itemid = 0;
            int.TryParse(keyValue, out _itemid);
            this.itemid = _itemid;
        }

        #endregion 扩展操作
    }
}