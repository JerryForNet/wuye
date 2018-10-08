using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:58
    /// 描 述：DevicePart
    /// </summary>
    public class DevicePartEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// p_number
        /// </summary>
        /// <returns></returns>
        public string p_number { get; set; }
        /// <summary>
        /// p_name
        /// </summary>
        /// <returns></returns>
        public string p_name { get; set; }
        /// <summary>
        /// p_standard
        /// </summary>
        /// <returns></returns>
        public string p_standard { get; set; }
        /// <summary>
        /// p_place
        /// </summary>
        /// <returns></returns>
        public string p_place { get; set; }
        /// <summary>
        /// d_devicenumber
        /// </summary>
        /// <returns></returns>
        public string d_devicenumber { get; set; }
        /// <summary>
        /// p_groupid
        /// </summary>
        /// <returns></returns>
        public int? p_groupid { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.p_number = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.p_number = keyValue;
                                            }
        #endregion
    }
}