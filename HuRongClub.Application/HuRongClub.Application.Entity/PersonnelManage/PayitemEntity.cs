using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:06
    /// 描 述：Payitem
    /// </summary>
    public class PayitemEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// itemcode
        /// </summary>
        /// <returns></returns>
        public string itemcode { get; set; }
        /// <summary>
        /// dispName
        /// </summary>
        /// <returns></returns>
        public string dispName { get; set; }
        /// <summary>
        /// disable
        /// </summary>
        /// <returns></returns>
        public string disable { get; set; }
        /// <summary>
        /// groupcode
        /// </summary>
        /// <returns></returns>
        public string groupcode { get; set; }
        /// <summary>
        /// CreatorId
        /// </summary>
        /// <returns></returns>
        public int? CreatorId { get; set; }
        /// <summary>
        /// CreatorName
        /// </summary>
        /// <returns></returns>
        public string CreatorName { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// OrderNo
        /// </summary>
        /// <returns></returns>
        public int? OrderNo { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
           // this.itemcode = "";// Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.itemcode = keyValue;
                                            }
        #endregion
    }
}