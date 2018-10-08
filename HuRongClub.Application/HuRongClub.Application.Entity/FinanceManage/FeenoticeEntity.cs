using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-08 16:21
    /// 描 述：进账认领
    /// </summary>
    public class FeenoticeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns></returns>
        public string NoticeID { get; set; }
        /// <summary>
        /// 账单编号
        /// </summary>
        /// <returns></returns>
        public string accountcode { get; set; }
        /// <summary>
        /// 账单单位
        /// </summary>
        /// <returns></returns>
        public string accountcompany { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        /// <returns></returns>
        public DateTime? accountdate { get; set; }
        /// <summary>
        /// 账单金额
        /// </summary>
        /// <returns></returns>
        public decimal? account { get; set; }
        /// <summary>
        /// 账单备注
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }
        /// <summary>
        /// 创建人编号
        /// </summary>
        /// <returns></returns>
        public string CreatorId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        public string CreatorName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 认领用户ID
        /// </summary>
        /// <returns></returns>
        public string checkuserid { get; set; }
        /// <summary>
        /// 认领日期
        /// </summary>
        /// <returns></returns>
        public DateTime? checkdate { get; set; }
        /// <summary>
        /// 认领物业
        /// </summary>
        /// <returns></returns>
        public string checkproperty { get; set; }
        /// <summary>
        /// 认领备注
        /// </summary>
        /// <returns></returns>
        public string checkremark { get; set; }
        /// <summary>
        /// 对方帐号
        /// </summary>
        /// <returns></returns>
        public string accounts { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        /// <returns></returns>
        public string purpose { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.NoticeID = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.NoticeID = keyValue;
                                            }
        #endregion
    }
}