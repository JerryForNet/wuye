using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:07
    /// 描 述：Owner_fee
    /// </summary>
    public class Owner_feeMap : EntityTypeConfiguration<Owner_feeEntity>
    {
        public Owner_feeMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_owner_fee");
            //主键
            this.HasKey(t => t.owner_feeid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
