using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 19:31
    /// 描 述：员工职位变动
    /// </summary>
    public class EmployexchangeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// itemid
        /// </summary>
        /// <returns></returns>
        public int? itemid { get; set; }
        /// <summary>
        /// empid
        /// </summary>
        /// <returns></returns>
        public int? empid { get; set; }
        /// <summary>
        /// fromdept
        /// </summary>
        /// <returns></returns>
        public string fromdept { get; set; }
        /// <summary>
        /// fromclass
        /// </summary>
        /// <returns></returns>
        public string fromclass { get; set; }
        /// <summary>
        /// todept
        /// </summary>
        /// <returns></returns>
        public string todept { get; set; }
        /// <summary>
        /// toclass
        /// </summary>
        /// <returns></returns>
        public string toclass { get; set; }
        /// <summary>
        /// sdate
        /// </summary>
        /// <returns></returns>
        public DateTime? sdate { get; set; }
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
            int _id = 0;
            int.TryParse(keyValue, out _id);
            this.itemid = _id;
                                            }
        #endregion
    }
}