using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 19:31
    /// 描 述：员工职位变动
    /// </summary>
    public class EmployexchangeMap : EntityTypeConfiguration<EmployexchangeEntity>
    {
        public EmployexchangeMap()
        {
            #region 表、主键
            //表
            this.ToTable("hr_employexchange");
            //主键
            this.HasKey(t => t.itemid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
