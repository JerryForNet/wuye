using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2019-01-17 09:31
    /// 描 述：PtInvoice
    /// </summary>
    public class PtInvoiceMap : EntityTypeConfiguration<PtInvoiceEntity>
    {
        public PtInvoiceMap()
        {
            #region 表、主键
            //表
            this.ToTable("pt_invoice");
            //主键
            this.HasKey(t => t.ticket_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
