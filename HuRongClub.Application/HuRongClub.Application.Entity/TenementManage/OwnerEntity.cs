using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.TenementManage
{
    /// <summary>
    /// 版 本    
    /// 创 建：超级管理员
    /// 日 期：2017-04-01 10:57
    /// 描 述：Owner
    /// </summary>
    public class OwnerEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// owner_id
        /// </summary>
        /// <returns></returns>
        public string owner_id { get; set; }
        /// <summary>
        /// property_id
        /// </summary>
        /// <returns></returns>
        public string property_id { get; set; }
        /// <summary>
        /// sign_userid
        /// </summary>
        /// <returns></returns>
        public string sign_userid { get; set; }
        /// <summary>
        /// sign_date
        /// </summary>
        /// <returns></returns>
        public DateTime? sign_date { get; set; }
        /// <summary>
        /// room_id
        /// </summary>
        /// <returns></returns>
        public string room_id { get; set; }
        /// <summary>
        /// in_date
        /// </summary>
        /// <returns></returns>
        public DateTime? in_date { get; set; }
        /// <summary>
        /// out_date
        /// </summary>
        /// <returns></returns>
        public DateTime? out_date { get; set; }
        /// <summary>
        /// remark
        /// </summary>
        /// <returns></returns>
        public string remark { get; set; }
        /// <summary>
        /// link1_name
        /// </summary>
        /// <returns></returns>
        public string link1_name { get; set; }
        /// <summary>
        /// link1_tel
        /// </summary>
        /// <returns></returns>
        public string link1_tel { get; set; }
        /// <summary>
        /// link1_mark
        /// </summary>
        /// <returns></returns>
        public string link1_mark { get; set; }
        /// <summary>
        /// link2_name
        /// </summary>
        /// <returns></returns>
        public string link2_name { get; set; }
        /// <summary>
        /// link2_mark
        /// </summary>
        /// <returns></returns>
        public string link2_mark { get; set; }
        /// <summary>
        /// link2_tel
        /// </summary>
        /// <returns></returns>
        public string link2_tel { get; set; }
        /// <summary>
        /// owner_name
        /// </summary>
        /// <returns></returns>
        public string owner_name { get; set; }
        /// <summary>
        /// owner_tel
        /// </summary>
        /// <returns></returns>
        public string owner_tel { get; set; }
        /// <summary>
        /// owner_cardtype
        /// </summary>
        /// <returns></returns>
        public string owner_cardtype { get; set; }
        /// <summary>
        /// owner_cardno
        /// </summary>
        /// <returns></returns>
        public string owner_cardno { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.owner_id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.owner_id = keyValue;
                                            }
        #endregion
    }
}