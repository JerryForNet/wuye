using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:47
    /// 描 述：Device
    /// </summary>
    public class DeviceMap : EntityTypeConfiguration<DeviceEntity>
    {
        public DeviceMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_device");
            //主键
            this.HasKey(t => t.d_number);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
