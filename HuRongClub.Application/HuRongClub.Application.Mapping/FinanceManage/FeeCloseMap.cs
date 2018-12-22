using HuRongClub.Application.Entity.FinanceManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：李俊
    /// 日 期：2018-12-22 21:13
    /// 描 述：财务账开关
    /// </summary>
    public class FeeCloseMap : EntityTypeConfiguration<FeeCloseEntity>
    {
        public FeeCloseMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_feeclose");
            //主键
            this.HasKey(t => t.property_id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
