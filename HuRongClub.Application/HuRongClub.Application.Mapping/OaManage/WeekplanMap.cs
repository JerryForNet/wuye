using HuRongClub.Application.Entity.OaManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-26 16:05
    /// 描 述：工作周记
    /// </summary>
    public class WeekplanMap : EntityTypeConfiguration<WeekplanEntity>
    {
        public WeekplanMap()
        {
            #region 表、主键

            //表
            this.ToTable("oa_weekplan");
            //主键
            this.HasKey(t => t.id);

            #endregion 表、主键
        }
    }
}