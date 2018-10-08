using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:13
    /// 描 述：FixReport
    /// </summary>
    public class FixReportEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// FixReportID
        /// </summary>
        /// <returns></returns>
        public string FixReportID { get; set; }
        /// <summary>
        /// fixNumber_No
        /// </summary>
        /// <returns></returns>
        public string fixNumber_No { get; set; }
        /// <summary>
        /// propertyid
        /// </summary>
        /// <returns></returns>
        public string propertyid { get; set; }
        /// <summary>
        /// ReportDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ReportDate { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// ReportMan
        /// </summary>
        /// <returns></returns>
        public string ReportMan { get; set; }
        /// <summary>
        /// fixtype
        /// </summary>
        /// <returns></returns>
        public Int16? fixtype { get; set; }
        /// <summary>
        /// manfee
        /// </summary>
        /// <returns></returns>
        public decimal? manfee { get; set; }
        /// <summary>
        /// materialfee
        /// </summary>
        /// <returns></returns>
        public decimal? materialfee { get; set; }
        /// <summary>
        /// feetype
        /// </summary>
        /// <returns></returns>
        public Int16? feetype { get; set; }
        /// <summary>
        /// fixgroup
        /// </summary>
        /// <returns></returns>
        public string fixgroup { get; set; }
        /// <summary>
        /// fixman
        /// </summary>
        /// <returns></returns>
        public string fixman { get; set; }
        /// <summary>
        /// senddate
        /// </summary>
        /// <returns></returns>
        public DateTime? senddate { get; set; }
        /// <summary>
        /// backdate
        /// </summary>
        /// <returns></returns>
        public DateTime? backdate { get; set; }
        /// <summary>
        /// fixinfo
        /// </summary>
        /// <returns></returns>
        public string fixinfo { get; set; }
        /// <summary>
        /// back_info
        /// </summary>
        /// <returns></returns>
        public string back_info { get; set; }
        /// <summary>
        /// back_qu
        /// </summary>
        /// <returns></returns>
        public string back_qu { get; set; }
        /// <summary>
        /// back_ser
        /// </summary>
        /// <returns></returns>
        public string back_ser { get; set; }
        /// <summary>
        /// backinfo
        /// </summary>
        /// <returns></returns>
        public string backinfo { get; set; }
        /// <summary>
        /// feedbackdate
        /// </summary>
        /// <returns></returns>
        public DateTime? feedbackdate { get; set; }
        /// <summary>
        /// feedbackman
        /// </summary>
        /// <returns></returns>
        public string feedbackman { get; set; }
        /// <summary>
        /// feedbackinfo
        /// </summary>
        /// <returns></returns>
        public string feedbackinfo { get; set; }
        /// <summary>
        /// checkdate
        /// </summary>
        /// <returns></returns>
        public DateTime? checkdate { get; set; }
        /// <summary>
        /// checkcharge
        /// </summary>
        /// <returns></returns>
        public string checkcharge { get; set; }
        /// <summary>
        /// checkinfo
        /// </summary>
        /// <returns></returns>
        public string checkinfo { get; set; }
        /// <summary>
        /// userid
        /// </summary>
        /// <returns></returns>
        public int? userid { get; set; }
        /// <summary>
        /// inputdate
        /// </summary>
        /// <returns></returns>
        public DateTime? inputdate { get; set; }
        /// <summary>
        /// phone
        /// </summary>
        /// <returns></returns>
        public string phone { get; set; }
        /// <summary>
        /// receivedate
        /// </summary>
        /// <returns></returns>
        public DateTime? receivedate { get; set; }
        /// <summary>
        /// owner_id
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// customertype
        /// </summary>
        /// <returns></returns>
        public Int16? customertype { get; set; }
        /// <summary>
        /// customername
        /// </summary>
        /// <returns></returns>
        public string customername { get; set; }

        /// <summary>
        /// 楼栋
        /// </summary>
        public string building_id { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.FixReportID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.FixReportID = keyValue;
                                            }
        #endregion
    }
}