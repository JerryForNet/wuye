using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 17:11
    /// 描 述：费用科目表
    /// </summary>
    public class FeeitemMap : EntityTypeConfiguration<FeeitemEntity>
    {
        public FeeitemMap()
        {
            #region 表、主键

            //表
            this.ToTable("wy_feeitem");
            //主键
            this.HasKey(t => t.feeitem_id);

            #endregion 表、主键
        }
    }
}