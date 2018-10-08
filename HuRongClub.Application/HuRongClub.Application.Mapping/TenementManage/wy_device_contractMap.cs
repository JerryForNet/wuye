using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：卢以君
    /// 日 期：2017-05-12 18:34
    /// 描 述：wy_device_contract
    /// </summary>
    public class wy_device_contractMap : EntityTypeConfiguration<wy_device_contractEntity>
    {
        public wy_device_contractMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_device_contract");
            //主键
            this.HasKey(t => t.pkeyid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
