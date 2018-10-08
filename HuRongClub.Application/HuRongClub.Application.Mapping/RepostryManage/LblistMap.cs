using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-06-20 10:32
    /// 描 述：Lblist
    /// </summary>
    public class LblistMap : EntityTypeConfiguration<LblistEntity>
    {
        public LblistMap()
        {
            #region 表、主键
            //表
            this.ToTable("wh_lblist");
            //主键
            this.HasKey(t => t.lid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
