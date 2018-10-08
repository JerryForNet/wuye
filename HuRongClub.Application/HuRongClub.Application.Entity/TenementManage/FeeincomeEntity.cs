using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:36
    /// 描 述：费用应收表
    /// </summary>
    public class FeeincomeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 应收序号
        /// </summary>
        /// <returns></returns>
        public string income_id { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
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
        /// 费用科目ID
        /// </summary>
        /// <returns></returns>
        public string feeitem_id { get; set; }
        /// <summary>
        /// 应收日期年
        /// </summary>
        /// <returns></returns>
        public Int16? fee_year { get; set; }
        /// <summary>
        /// 应收日期月
        /// </summary>
        /// <returns></returns>
        public Int16? fee_month { get; set; }
        /// <summary>
        /// 计费开始日期
        /// </summary>
        /// <returns></returns>
        public DateTime? start_date { get; set; }
        /// <summary>
        /// 计费结束日期
        /// </summary>
        /// <returns></returns>
        public DateTime? end_date { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        public decimal? fee_income { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        public decimal? fee_already { get; set; }
        /// <summary>
        /// 收费日期
        /// </summary>
        /// <returns></returns>
        public DateTime? fee_date { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <returns></returns>
        public string userid { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        /// <summary>
        /// oldid
        /// </summary>
        /// <returns></returns>
        public string oldid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string notes { get; set; }
        /// <summary>
        /// 楼栋编号
        /// </summary>
        /// <returns></returns>
        public string building_id { get; set; }
        /// <summary>
        /// 付款截止日期
        /// </summary>
        /// <returns></returns>
        public DateTime? pay_enddate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.income_id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.income_id = keyValue;
                                            }
        #endregion
    }
}