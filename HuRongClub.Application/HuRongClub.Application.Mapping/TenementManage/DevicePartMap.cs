using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:58
    /// 描 述：DevicePart
    /// </summary>
    public class DevicePartMap : EntityTypeConfiguration<DevicePartEntity>
    {
        public DevicePartMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_device_part");
            //主键
            this.HasKey(t => t.p_number);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
