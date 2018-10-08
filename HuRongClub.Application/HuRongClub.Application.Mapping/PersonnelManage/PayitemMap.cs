using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:06
    /// 描 述：Payitem
    /// </summary>
    public class PayitemMap : EntityTypeConfiguration<PayitemEntity>
    {
        public PayitemMap()
        {
            #region 表、主键
            //表
            this.ToTable("hr_payitem");
            //主键
            this.HasKey(t => t.itemcode);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
