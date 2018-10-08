using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-04-28 21:58
    /// 描 述：wy_Device_maintence
    /// </summary>
    public class DeviceMaintenceMap : EntityTypeConfiguration<DeviceMaintenceEntity>
    {
        public DeviceMaintenceMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_Device_maintence");
            //主键
            this.HasKey(t => t.classid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
