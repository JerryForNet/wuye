using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:19
    /// 描 述：Feechangelog
    /// </summary>
    public class FeechangelogEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns></returns>
        public string itemid { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        /// <summary>
        /// 租赁合同编号
        /// </summary>
        /// <returns></returns>
        public string contract_id { get; set; }
        /// <summary>
        /// 业主编号
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// 调整前金额
        /// </summary>
        /// <returns></returns>
        public double? source_money { get; set; }
        /// <summary>
        /// 调整后金额
        /// </summary>
        /// <returns></returns>
        public double? new_money { get; set; }
        /// <summary>
        /// 费用科目ID
        /// </summary>
        /// <returns></returns>
        public string feeitem_id { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        /// <returns></returns>
        public DateTime? operatetime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string operatername { get; set; }
        /// <summary>
        /// 应收日期
        /// </summary>
        /// <returns></returns>
        public DateTime? income_date { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }
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