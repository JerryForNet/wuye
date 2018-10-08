using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:08
    /// 描 述：Feeticket
    /// </summary>
    public class FeeticketMap : EntityTypeConfiguration<FeeticketEntity>
    {
        public FeeticketMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_feeticket");
            //主键
            this.HasKey(t => t.ticket_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
