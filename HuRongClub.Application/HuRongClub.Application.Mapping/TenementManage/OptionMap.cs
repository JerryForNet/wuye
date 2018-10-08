using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-01 16:42
    /// 描 述：Option
    /// </summary>
    public class OptionMap : EntityTypeConfiguration<OptionEntity>
    {
        public OptionMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_option");
            //主键
            this.HasKey(t => t.property_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
