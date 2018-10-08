using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:36
    /// 描 述：Feeincome
    /// </summary>
    public class FeeincomeMap : EntityTypeConfiguration<FeeincomeEntity>
    {
        public FeeincomeMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_feeincome");
            //主键
            this.HasKey(t => t.income_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
