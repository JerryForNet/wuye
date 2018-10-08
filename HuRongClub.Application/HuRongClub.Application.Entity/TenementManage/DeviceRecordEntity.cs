using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 14:30
    /// 描 述：DeviceRecord
    /// </summary>
    public class DeviceRecordEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// frecordid
        /// </summary>
        /// <returns></returns>
        public string frecordid { get; set; }
        /// <summary>
        /// fdate
        /// </summary>
        /// <returns></returns>
        public DateTime? fdate { get; set; }
        /// <summary>
        /// fman
        /// </summary>
        /// <returns></returns>
        public string fman { get; set; }
        /// <summary>
        /// fclass
        /// </summary>
        /// <returns></returns>
        public string fclass { get; set; }
        /// <summary>
        /// frunstatus
        /// </summary>
        /// <returns></returns>
        public string frunstatus { get; set; }
        /// <summary>
        /// finfo
        /// </summary>
        /// <returns></returns>
        public string finfo { get; set; }
        /// <summary>
        /// fplanyear
        /// </summary>
        /// <returns></returns>
        public int? fplanyear { get; set; }
        /// <summary>
        /// fplanmonth
        /// </summary>
        /// <returns></returns>
        public Int16? fplanmonth { get; set; }
        /// <summary>
        /// fpartnumber
        /// </summary>
        /// <returns></returns>
        public string fpartnumber { get; set; }
        /// <summary>
        /// fstatusid
        /// </summary>
        /// <returns></returns>
        public Int16? fstatusid { get; set; }
        /// <summary>
        /// propertyid
        /// </summary>
        /// <returns></returns>
        public string propertyid { get; set; }
        /// <summary>
        /// fnumber
        /// </summary>
        /// <returns></returns>
        public int? fnumber { get; set; }
        /// <summary>
        /// fplanid
        /// </summary>
        /// <returns></returns>
        public string fplanid { get; set; }
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
            this.frecordid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.frecordid = keyValue;
                                            }
        #endregion
    }
}