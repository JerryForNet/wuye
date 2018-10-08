using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 15:26
    /// 描 述：供应商管理
    /// </summary>
    public class ProviderMap : EntityTypeConfiguration<ProviderEntity>
    {
        public ProviderMap()
        {
            #region 表、主键

            //表
            this.ToTable("tb_wh_provider");
            //主键
            this.HasKey(t => t.fproviderid);

            #endregion 表、主键
        }
    }
}