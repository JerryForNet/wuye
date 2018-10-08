using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:07
    /// 描 述：Owner_fee
    /// </summary>
    public class Owner_feeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// owner_feeid
        /// </summary>
        /// <returns></returns>
        public string owner_feeid { get; set; }
        /// <summary>
        /// owner_id
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// fee_code
        /// </summary>
        /// <returns></returns>
        public string fee_code { get; set; }
        /// <summary>
        /// fee_money
        /// </summary>
        /// <returns></returns>
        public decimal? fee_money { get; set; }
        /// <summary>
        /// fee_rule
        /// </summary>
        /// <returns></returns>
        public string fee_rule { get; set; }
        /// <summary>
        /// start_date
        /// </summary>
        /// <returns></returns>
        public DateTime? start_date { get; set; }
        /// <summary>
        /// room_id
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.owner_feeid = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.owner_feeid = keyValue;
                                            }
        #endregion
    }
}