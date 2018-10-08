using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-02 13:04
    /// 描 述：wy_device_plan
    /// </summary>
    public class wy_device_planEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// planid
        /// </summary>
        /// <returns></returns>
        public string planid { get; set; }
        /// <summary>
        /// p_number
        /// </summary>
        /// <returns></returns>
        public string p_number { get; set; }
        /// <summary>
        /// fyear
        /// </summary>
        /// <returns></returns>
        public int? fyear { get; set; }
        /// <summary>
        /// fmonth
        /// </summary>
        /// <returns></returns>
        public int? fmonth { get; set; }
        /// <summary>
        /// classid
        /// </summary>
        /// <returns></returns>
        public string classid { get; set; }
        /// <summary>
        /// finfo
        /// </summary>
        /// <returns></returns>
        public string finfo { get; set; }
        /// <summary>
        /// fstatusid
        /// </summary>
        /// <returns></returns>
        public int? fstatusid { get; set; }
        /// <summary>
        /// fgroupid
        /// </summary>
        /// <returns></returns>
        public int? fgroupid { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.planid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.planid = keyValue;
                                            }
        #endregion
    }
}