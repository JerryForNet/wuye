using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 10:39
    /// 描 述：费用实收表
    /// </summary>
    public class FeereceiveEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 实收编号
        /// </summary>
        /// <returns></returns>
        public string receive_id { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// 实收日期
        /// </summary>
        /// <returns></returns>
        public DateTime? receive_date { get; set; }
        /// <summary>
        /// 发票编号
        /// </summary>
        /// <returns></returns>
        public string ticket_id { get; set; }
        /// <summary>
        /// 业主编号
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// 租赁合同编号
        /// </summary>
        /// <returns></returns>
        public string rentcontract_id { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        /// <returns></returns>
        public decimal? fee_money { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <returns></returns>
        public string userid { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        /// <returns></returns>
        public string pay_mode { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        /// <summary>
        /// 是否打印发票
        /// </summary>
        /// <returns></returns>
        public string isprint { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        /// <returns></returns>
        public string printname { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.receive_id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.receive_id = keyValue;
                                            }
        #endregion
    }
}