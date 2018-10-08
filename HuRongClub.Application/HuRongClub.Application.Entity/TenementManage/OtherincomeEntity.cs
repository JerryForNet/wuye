using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 09:43
    /// 描 述：Otherincome
    /// </summary>
    public class OtherincomeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 应收ID
        /// </summary>
        /// <returns></returns>
        public string incomeid { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        /// <returns></returns>
        public string customer { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// 应收时间
        /// </summary>
        /// <returns></returns>
        public DateTime? feedate { get; set; }
        /// <summary>
        /// 费用金额
        /// </summary>
        /// <returns></returns>
        public double? feemoney { get; set; }
        /// <summary>
        /// 费用币种
        /// </summary>
        /// <returns></returns>
        public string currency { get; set; }
        /// <summary>
        /// 发票编号
        /// </summary>
        /// <returns></returns>
        public string ticketid { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        /// <returns></returns>
        public string operateuser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// 是否打印发票
        /// </summary>
        /// <returns></returns>
        public string isprint { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.incomeid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.incomeid = keyValue;
                                            }
        #endregion
    }
}