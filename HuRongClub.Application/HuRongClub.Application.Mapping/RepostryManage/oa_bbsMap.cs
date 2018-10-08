using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-01 18:57
    /// 描 述：oa_bbs
    /// </summary>
    public class oa_bbsMap : EntityTypeConfiguration<oa_bbsEntity>
    {
        public oa_bbsMap()
        {
            #region 表、主键
            //表
            this.ToTable("oa_bbs");
            //主键
            this.HasKey(t => t.bbsid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
