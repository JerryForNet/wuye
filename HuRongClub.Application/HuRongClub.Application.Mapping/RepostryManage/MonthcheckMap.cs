using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-27 14:25
    /// 描 述：月结账
    /// </summary>
    public class MonthcheckMap : EntityTypeConfiguration<MonthcheckEntity>
    {
        public MonthcheckMap()
        {
            #region 表、主键

            //表
            this.ToTable("tb_wh_monthcheck");
            //主键
            this.HasKey(t => t.ftypecode);

            #endregion 表、主键
        }
    }
}