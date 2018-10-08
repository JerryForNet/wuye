using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-24 19:21
    /// 描 述：Feecheck
    /// </summary>
    public class FeecheckMap : EntityTypeConfiguration<FeecheckEntity>
    {
        public FeecheckMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_feecheck");
            //主键
            this.HasKey(t => t.check_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
