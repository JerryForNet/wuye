using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本
    
    /// 创 建：超级管理员
    /// 日 期：2017-04-01 10:57
    /// 描 述：Owner
    /// </summary>
    public class OwnerMap : EntityTypeConfiguration<OwnerEntity>
    {
        public OwnerMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_owner");
            //主键
            this.HasKey(t => t.owner_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
