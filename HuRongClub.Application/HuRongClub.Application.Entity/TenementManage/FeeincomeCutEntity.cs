using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-24 14:37
    /// 描 述：FeeincomeCut
    /// </summary>
    public class FeeincomeCutEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// itemid
        /// </summary>
        /// <returns></returns>
        public int? itemid { get; set; }
        /// <summary>
        /// property_id
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// room_id
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        /// <summary>
        /// owner_id
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// contract_id
        /// </summary>
        /// <returns></returns>
        public string contract_id { get; set; }
        /// <summary>
        /// feeitem_id
        /// </summary>
        /// <returns></returns>
        public string feeitem_id { get; set; }
        /// <summary>
        /// fee_year
        /// </summary>
        /// <returns></returns>
        public Int16? fee_year { get; set; }
        /// <summary>
        /// fee_month
        /// </summary>
        /// <returns></returns>
        public Int16? fee_month { get; set; }
        /// <summary>
        /// fee_cutmoney
        /// </summary>
        /// <returns></returns>
        public double? fee_cutmoney { get; set; }
        /// <summary>
        /// inputtime
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// inputempid
        /// </summary>
        /// <returns></returns>
        public string inputempid { get; set; }
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