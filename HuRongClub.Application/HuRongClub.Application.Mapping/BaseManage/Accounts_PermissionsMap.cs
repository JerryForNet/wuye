using HuRongClub.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-21 16:56
    /// 描 述：Accounts_Permissions
    /// </summary>
    public class Accounts_PermissionsMap : EntityTypeConfiguration<Accounts_PermissionsEntity>
    {
        public Accounts_PermissionsMap()
        {
            #region 表、主键
            //表
            this.ToTable("Accounts_Permissions");
            //主键
            this.HasKey(t => t.ItemDetailId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
