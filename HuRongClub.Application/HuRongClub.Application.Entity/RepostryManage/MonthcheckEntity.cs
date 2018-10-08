using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-27 14:25
    /// 描 述：月结账
    /// </summary>
    public class MonthcheckEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// ftypecode
        /// </summary>
        /// <returns></returns>
        public string ftypecode { get; set; }

        /// <summary>
        /// fyear
        /// </summary>
        /// <returns></returns>
        public Int16 fyear { get; set; }

        /// <summary>
        /// fmonth
        /// </summary>
        /// <returns></returns>
        public Int16 fmonth { get; set; }

        /// <summary>
        /// fbeginmoney
        /// </summary>
        /// <returns></returns>
        public Decimal fbeginmoney { get; set; }

        /// <summary>
        /// fbegindate
        /// </summary>
        /// <returns></returns>
        public DateTime? fbegindate { get; set; }

        /// <summary>
        /// fenddate
        /// </summary>
        /// <returns></returns>
        public DateTime? fenddate { get; set; }

        /// <summary>
        /// finmoney
        /// </summary>
        /// <returns></returns>
        public Decimal finmoney { get; set; }

        /// <summary>
        /// foutmoney
        /// </summary>
        /// <returns></returns>
        public Decimal foutmoney { get; set; }

        /// <summary>
        /// fendmoney
        /// </summary>
        /// <returns></returns>
        public Decimal fendmoney { get; set; }

        #endregion 实体成员

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
            // 主健只能为 uniqueidentifier 或 int
        }

        #endregion 扩展操作
    }
}