using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 09:43
    /// 描 述：Otherincome
    /// </summary>
    public class OtherincomeMap : EntityTypeConfiguration<OtherincomeEntity>
    {
        public OtherincomeMap()
        {
            #region 表、主键
            //表
            this.ToTable("wy_otherincome");
            //主键
            this.HasKey(t => t.incomeid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
