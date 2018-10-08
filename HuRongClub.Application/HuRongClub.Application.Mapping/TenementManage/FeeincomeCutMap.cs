using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-24 14:37
    /// 描 述：FeeincomeCut
    /// </summary>
    public class FeeincomeCutMap : EntityTypeConfiguration<FeeincomeCutEntity>
    {
        public FeeincomeCutMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_feeincome_cut");
            //主键
            this.HasKey(t => t.itemid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
