using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-20 10:13
    /// 描 述：支出费用列表
    /// </summary>
    public class InnerbillMap : EntityTypeConfiguration<InnerbillEntity>
    {
        public InnerbillMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_innerbill");
            //主键
            this.HasKey(t => t.billid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
