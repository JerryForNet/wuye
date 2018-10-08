using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:40
    /// 描 述：Payroll
    /// </summary>
    public class PayrollEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PayrollId
        /// </summary>
        /// <returns></returns>
        public int? PayrollId { get; set; }
        /// <summary>
        /// period
        /// </summary>
        /// <returns></returns>
        public string period { get; set; }
        /// <summary>
        /// status
        /// </summary>
        /// <returns></returns>
        public int? status { get; set; }
        /// <summary>
        /// employnum
        /// </summary>
        /// <returns></returns>
        public int? employnum { get; set; }
        /// <summary>
        /// Totalamount
        /// </summary>
        /// <returns></returns>
        public decimal? Totalamount { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>
        /// <returns></returns>
        public int? CreatorId { get; set; }
        /// <summary>
        /// CreatorName
        /// </summary>
        /// <returns></returns>
        public string CreatorName { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// UpdatorId
        /// </summary>
        /// <returns></returns>
        public int? UpdatorId { get; set; }
        /// <summary>
        /// UpdatorName
        /// </summary>
        /// <returns></returns>
        public string UpdatorName { get; set; }
        /// <summary>
        /// UpdateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? UpdateDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            int _id = 0;
            int.TryParse(keyValue, out _id);
            this.PayrollId = _id;
                                            }
        #endregion
    }
}