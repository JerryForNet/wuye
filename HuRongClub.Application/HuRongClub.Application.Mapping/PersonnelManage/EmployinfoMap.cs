using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// 版 本
    
    /// 创 建：王菲
    /// 日 期：2017-04-01 09:43
    /// 描 述：员工档案
    /// </summary>
    public class EmployinfoMap : EntityTypeConfiguration<EmployinfoEntity>
    {
        public EmployinfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("hr_employinfo");
            //主键
            this.HasKey(t => t.empid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
