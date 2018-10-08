using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 18:43
    /// 描 述：员工家庭信息
    /// </summary>
    public class EmployfamilyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// memberid
        /// </summary>
        /// <returns></returns>
        public int? memberid { get; set; }
        /// <summary>
        /// membername
        /// </summary>
        /// <returns></returns>
        public string membername { get; set; }
        /// <summary>
        /// epithet
        /// </summary>
        /// <returns></returns>
        public string epithet { get; set; }
        /// <summary>
        /// sex
        /// </summary>
        /// <returns></returns>
        public string sex { get; set; }
        /// <summary>
        /// birthdate
        /// </summary>
        /// <returns></returns>
        public DateTime? birthdate { get; set; }
        /// <summary>
        /// company
        /// </summary>
        /// <returns></returns>
        public string company { get; set; }
        /// <summary>
        /// empid
        /// </summary>
        /// <returns></returns>
        public int? empid { get; set; }
        /// <summary>
        /// job
        /// </summary>
        /// <returns></returns>
        public string job { get; set; }
        /// <summary>
        /// zzmm
        /// </summary>
        /// <returns></returns>
        public string zzmm { get; set; }
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
            int _id = 0;
            int.TryParse(keyValue, out _id);
            this.memberid = _id;
                                            }
        #endregion
    }
}