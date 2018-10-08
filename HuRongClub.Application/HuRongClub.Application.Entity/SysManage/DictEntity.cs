using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.SysManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 20:29
    /// 描 述：物业系统字典
    /// </summary>
    public class DictEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// dictid
        /// </summary>
        /// <returns></returns>
        public int? dictid { get; set; }
        /// <summary>
        /// dictname
        /// </summary>
        /// <returns></returns>
        public string dictname { get; set; }
        /// <summary>
        /// dictgroup
        /// </summary>
        /// <returns></returns>
        public string dictgroup { get; set; }
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
            this.dictid = _id;
                                            }
        #endregion
    }
}