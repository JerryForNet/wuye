using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-07 17:17
    /// 描 述：Building
    /// </summary>
    public class BuildingMap : EntityTypeConfiguration<BuildingEntity>
    {
        public BuildingMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_building");
            //主键
            this.HasKey(t => t.building_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
