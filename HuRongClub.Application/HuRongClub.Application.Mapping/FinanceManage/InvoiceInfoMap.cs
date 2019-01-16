using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：Jerry.Li
    /// 日 期：2019-01-16 21:36
    /// 描 述：InvoiceInfo
    /// </summary>
    public class InvoiceInfoMap : EntityTypeConfiguration<InvoiceInfoEntity>
    {
        public InvoiceInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("pt_kpkhxx");
            //主键
            this.HasKey(t => t.khdm);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
