using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-04-28 21:58
    /// 描 述：wy_Device_maintence
    /// </summary>
    public class DeviceMaintenceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// classid
        /// </summary>
        /// <returns></returns>
        public string classid { get; set; }
        /// <summary>
        /// p_number
        /// </summary>
        /// <returns></returns>
        public string p_number { get; set; }
        /// <summary>
        /// maintencename
        /// </summary>
        /// <returns></returns>
        public string maintencename { get; set; }
        /// <summary>
        /// maintence
        /// </summary>
        /// <returns></returns>
        public string maintence { get; set; }
        /// <summary>
        /// sid
        /// </summary>
        /// <returns></returns>
        public string sid { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.classid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.classid = keyValue;
                                            }
        #endregion
    }
}