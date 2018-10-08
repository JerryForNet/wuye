using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-08 16:21
    /// 描 述：进账认领
    /// </summary>
    public class FeenoticeMap : EntityTypeConfiguration<FeenoticeEntity>
    {
        public FeenoticeMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_feenotice");
            //主键
            this.HasKey(t => t.NoticeID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
