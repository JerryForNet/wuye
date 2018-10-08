using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-04-27 16:55
    /// 描 述：设备类型
    /// </summary>
    public class wy_device_typeMap : EntityTypeConfiguration<wy_device_typeEntity>
    {
        public wy_device_typeMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_device_type");
            //主键
            this.HasKey(t => t.typecode);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
