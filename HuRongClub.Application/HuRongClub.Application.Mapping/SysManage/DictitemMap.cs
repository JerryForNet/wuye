using HuRongClub.Application.Entity.SysManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.SysManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 20:55
    /// 描 述：字典详情
    /// </summary>
    public class DictitemMap : EntityTypeConfiguration<DictitemEntity>
    {
        public DictitemMap()
        {
            #region 表、主键
            //表
            this.ToTable("sys_dictitem");
            //主键
            this.HasKey(t => t.itemid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
