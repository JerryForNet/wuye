using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-28 22:19
    /// 描 述：其他费用应收明细
    /// </summary>
    public class OtherincomeitemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 明细序号
        /// </summary>
        /// <returns></returns>
        public string incomeitem_id { get; set; }
        /// <summary>
        /// 其他应收编号
        /// </summary>
        /// <returns></returns>
        public string incomeid { get; set; }
        /// <summary>
        /// 费用科目编号
        /// </summary>
        /// <returns></returns>
        public string feeitem_id { get; set; }
        /// <summary>
        /// 费用说明
        /// </summary>
        /// <returns></returns>
        public string subject { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        public double? fee_income { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.incomeitem_id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.incomeitem_id = keyValue;
        }
        #endregion
    }
}
