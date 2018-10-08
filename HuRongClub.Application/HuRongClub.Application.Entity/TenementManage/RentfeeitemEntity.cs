using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-21 09:28
    /// 描 述：Rentfeeitem
    /// </summary>
    public class RentfeeitemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns></returns>
        public string itemid { get; set; }
        /// <summary>
        /// 租赁合同ID
        /// </summary>
        /// <returns></returns>
        public string contractid { get; set; }
        /// <summary>
        /// 费用科目编号
        /// </summary>
        /// <returns></returns>
        public string feeitemid { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        /// <returns></returns>
        public string currency { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        public decimal? feemoney { get; set; }
        /// <summary>
        /// 规则
        /// </summary>
        /// <returns></returns>
        public string feerule { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.itemid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.itemid = keyValue;
                                            }
        #endregion
    }
}