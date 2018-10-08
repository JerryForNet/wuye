using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-24 19:21
    /// 描 述：收费核销表
    /// </summary>
    public class FeecheckEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 核销序号
        /// </summary>
        /// <returns></returns>
        public string check_id { get; set; }
        /// <summary>
        /// 实收编号
        /// </summary>
        /// <returns></returns>
        public string receive_id { get; set; }
        /// <summary>
        /// 应收编号
        /// </summary>
        /// <returns></returns>
        public string income_id { get; set; }
        /// <summary>
        /// 核销金额
        /// </summary>
        /// <returns></returns>
        public decimal? check_money { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.check_id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.check_id = keyValue;
                                            }
        #endregion
    }
}