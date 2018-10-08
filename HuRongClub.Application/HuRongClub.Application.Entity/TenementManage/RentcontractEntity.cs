using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 09:28
    /// 描 述：Rentcontract
    /// </summary>
    public class RentcontractEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 租赁合同ID
        /// </summary>
        /// <returns></returns>
        public string contractid { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string customername { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string phone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        /// <returns></returns>
        public string fax { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string linkman { get; set; }
        /// <summary>
        /// 物业编号
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// 合同状态 0新建 1有效 2终止
        /// </summary>
        /// <returns></returns>
        public Int16? status { get; set; }
        /// <summary>
        /// 合同开始日期
        /// </summary>
        /// <returns></returns>
        public DateTime? expire_begin { get; set; }
        /// <summary>
        /// 合同结束日期
        /// </summary>
        /// <returns></returns>
        public DateTime? expire_end { get; set; }
        /// <summary>
        /// 签约日期
        /// </summary>
        /// <returns></returns>
        public DateTime? signdate { get; set; }
        /// <summary>
        /// 租赁面积
        /// </summary>
        /// <returns></returns>
        public Double? rentarea { get; set; }
        /// <summary>
        /// 合同信息
        /// </summary>
        /// <returns></returns>
        public string contractinfo { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        /// <returns></returns>
        public string attfile { get; set; }
        /// <summary>
        /// 合同编码
        /// </summary>
        /// <returns></returns>
        public string contractcode { get; set; }
        /// <summary>
        /// 租赁单元房间号
        /// </summary>
        /// <returns></returns>
        public string rentcell { get; set; }
        /// <summary>
        /// 输入时间
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }
        /// <summary>
        /// 输入人
        /// </summary>
        /// <returns></returns>
        public string inputuser { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.contractid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.contractid = keyValue;
                                            }
        #endregion
    }
}