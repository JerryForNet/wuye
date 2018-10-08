using HuRongClub.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 6.1
    
    
    /// 日 期：2015.11.27
    /// 描 述：授权功能
    /// </summary>
    public class AuthorizeMap : EntityTypeConfiguration<AuthorizeEntity>
    {
        public AuthorizeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Authorize");
            //主键
            this.HasKey(t => t.AuthorizeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
