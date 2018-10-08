using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-21 16:56
    /// 描 述：Accounts_Permissions
    /// </summary>
    public class Accounts_PermissionsEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// PermissionID
        /// </summary>
        /// <returns></returns>
        public int? PermissionID { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// CategoryID
        /// </summary>
        /// <returns></returns>
        public int? CategoryID { get; set; }
        /// <summary>
        /// permissionKey
        /// </summary>
        /// <returns></returns>
        public string permissionKey { get; set; }
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