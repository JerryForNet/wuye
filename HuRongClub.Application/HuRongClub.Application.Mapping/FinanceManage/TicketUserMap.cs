using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2019-01-20 20:54
    /// 描 述：TicketUser
    /// </summary>
    public class TicketUserMap : EntityTypeConfiguration<TicketUserEntity>
    {
        public TicketUserMap()
        {
            #region 表、主键

            //表
            this.ToTable("pt_kpkhxx_old");
            //主键
            this.HasKey(t => t.khdm);

            #endregion
        }
    }
}