using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-02 13:04
    /// 描 述：wy_device_plan
    /// </summary>
    public class wy_device_planMap : EntityTypeConfiguration<wy_device_planEntity>
    {
        public wy_device_planMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_device_plan");
            //主键
            this.HasKey(t => t.planid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
