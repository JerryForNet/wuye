using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Entity.SysManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2018-09-19 14:31
    /// 描 述：Accounts_Users
    /// </summary>
    public class Accounts_UsersMap : EntityTypeConfiguration<Accounts_UsersEntity>
    {
        public Accounts_UsersMap()
        {
            #region 表、主键
            //表
            this.ToTable("Accounts_Users");
            //主键
            this.HasKey(t => t.UserID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
