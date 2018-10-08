using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：姜良福
    /// 日 期：2017-04-01 10:09
    /// 描 述：产业档案
    /// </summary>
    public class PropertyMap : EntityTypeConfiguration<PropertyEntity>
    {
        public PropertyMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_property");
            //主键
            this.HasKey(t => t.property_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
