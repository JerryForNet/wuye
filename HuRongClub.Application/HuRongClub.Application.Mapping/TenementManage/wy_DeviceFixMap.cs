using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-12 18:36
    /// 描 述：wy_DeviceFix
    /// </summary>
    public class wy_DeviceFixMap : EntityTypeConfiguration<wy_DeviceFixEntity>
    {
        public wy_DeviceFixMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_DeviceFix");
            //主键
            this.HasKey(t => t.DeviceFixID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
