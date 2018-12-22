using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：李俊
    /// 日 期：2018-12-22 21:13
    /// 描 述：财务账开关
    /// </summary>
    public class FeeCloseEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// property_id
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// fyear
        /// </summary>
        /// <returns></returns>
        public Int16? fyear { get; set; }
        /// <summary>
        /// fmonth
        /// </summary>
        /// <returns></returns>
        public Int16? fmonth { get; set; }
        /// <summary>
        /// fstatus
        /// </summary>
        /// <returns></returns>
        public string fstatus { get; set; }
        /// <summary>
        /// fuser
        /// </summary>
        /// <returns></returns>
        public string fuser { get; set; }
        /// <summary>
        /// flasttime
        /// </summary>
        /// <returns></returns>
        public DateTime? flasttime { get; set; }
        #endregion

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
        #endregion
    }
}