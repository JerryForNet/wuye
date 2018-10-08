using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-12 18:36
    /// 描 述：wy_DeviceFix
    /// </summary>
    public class wy_DeviceFixEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// DeviceFixID
        /// </summary>
        /// <returns></returns>
        public string DeviceFixID { get; set; }
        /// <summary>
        /// devicenumber
        /// </summary>
        /// <returns></returns>
        public string devicenumber { get; set; }
        /// <summary>
        /// FixDate
        /// </summary>
        /// <returns></returns>
        public DateTime? FixDate { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// Fixer
        /// </summary>
        /// <returns></returns>
        public string Fixer { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.DeviceFixID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.DeviceFixID = keyValue;
                                            }
        #endregion
    }
}