using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-03 14:52
    /// 描 述：保修材料表
    /// </summary>
    public class FixmaterialMap : EntityTypeConfiguration<FixmaterialEntity>
    {
        public FixmaterialMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_fix_material");
            //主键
            this.HasKey(t => t.pkeyid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
