using HuRongClub.Application.Entity.SysManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.SysManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 20:29
    /// 描 述：物业系统字典
    /// </summary>
    public class DictMap : EntityTypeConfiguration<DictEntity>
    {
        public DictMap()
        {
            #region 表、主键
            //表
            this.ToTable("sys_dict");
            //主键
            this.HasKey(t => t.dictid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
