using HuRongClub.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 6.1
    
    
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public class ModuleButtonMap : EntityTypeConfiguration<ModuleButtonEntity>
    {
        public ModuleButtonMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_ModuleButton");
            //主键
            this.HasKey(t => t.ModuleButtonId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
