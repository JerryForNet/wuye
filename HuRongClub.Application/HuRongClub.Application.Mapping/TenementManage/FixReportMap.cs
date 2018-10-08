using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:13
    /// 描 述：FixReport
    /// </summary>
    public class FixReportMap : EntityTypeConfiguration<FixReportEntity>
    {
        public FixReportMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_FixReport");
            //主键
            this.HasKey(t => t.FixReportID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
