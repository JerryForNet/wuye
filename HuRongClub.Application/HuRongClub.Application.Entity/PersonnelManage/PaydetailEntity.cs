using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-05-02 18:41
    /// 描 述：Paydetail
    /// </summary>
    public class PaydetailEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// payrollid
        /// </summary>
        /// <returns></returns>
        public int? payrollid { get; set; }
        /// <summary>
        /// empid
        /// </summary>
        /// <returns></returns>
        public int? empid { get; set; }
        /// <summary>
        /// itemcode
        /// </summary>
        /// <returns></returns>
        public string itemcode { get; set; }
        /// <summary>
        /// amount
        /// </summary>
        /// <returns></returns>
        public decimal? amount { get; set; }
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
        /// <summary>
        /// vdef10
        /// </summary>
        /// <returns></returns>
        public int? vdef10 { get; set; }
        /// <summary>
        /// vdef11
        /// </summary>
        /// <returns></returns>
        public int? vdef11 { get; set; }
        /// <summary>
        /// vdef20
        /// </summary>
        /// <returns></returns>
        public Single? vdef20 { get; set; }
        /// <summary>
        /// vdef21
        /// </summary>
        /// <returns></returns>
        public Single? vdef21 { get; set; }
        /// <summary>
        /// vdef30
        /// </summary>
        /// <returns></returns>
        public string vdef30 { get; set; }
        /// <summary>
        /// vdef31
        /// </summary>
        /// <returns></returns>
        public string vdef31 { get; set; }
        /// <summary>
        /// vdef32
        /// </summary>
        /// <returns></returns>
        public string vdef32 { get; set; }
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
            this.Id = _id;
                                            }
        #endregion
    }
}