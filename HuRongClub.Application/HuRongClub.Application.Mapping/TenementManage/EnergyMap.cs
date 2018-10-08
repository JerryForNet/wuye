using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:01
    /// 描 述：Energy
    /// </summary>
    public class EnergyMap : EntityTypeConfiguration<EnergyEntity>
    {
        public EnergyMap()
        {
            #region 表、主键
            //表
            this.ToTable("tb_energy");
            //主键
            this.HasKey(t => t.FEnergyID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
