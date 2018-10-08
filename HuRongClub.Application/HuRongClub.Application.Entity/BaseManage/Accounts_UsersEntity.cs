using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-13 16:55
    /// 描 述：Accounts_Users
    /// </summary>
    public class Accounts_UsersEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// UserID
        /// </summary>
        /// <returns></returns>
        public int? UserID { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }
        /// <summary>
        /// TrueName
        /// </summary>
        /// <returns></returns>
        public string TrueName { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        /// <returns></returns>
        public string Sex { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        /// <returns></returns>
        public string Phone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// EmployeeID
        /// </summary>
        /// <returns></returns>
        public int? EmployeeID { get; set; }
        /// <summary>
        /// DepartmentID
        /// </summary>
        /// <returns></returns>
        public string DepartmentID { get; set; }
        /// <summary>
        /// Activity
        /// </summary>
        /// <returns></returns>
        public bool? Activity { get; set; }
        /// <summary>
        /// UserType
        /// </summary>
        /// <returns></returns>
        public string UserType { get; set; }
        /// <summary>
        /// Style
        /// </summary>
        /// <returns></returns>
        public int? Style { get; set; }
        /// <summary>
        /// smscode
        /// </summary>
        /// <returns></returns>
        public string smscode { get; set; }
        /// <summary>
        /// user_property
        /// </summary>
        /// <returns></returns>
        public string user_property { get; set; }
        /// <summary>
        /// roleid
        /// </summary>
        /// <returns></returns>
        public int? roleid { get; set; }
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
            this.UserID = _id;
                                            }
        #endregion
    }
}