using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// 版 本
    
    /// 创 建：王菲
    /// 日 期：2017-04-01 10:41
    /// 描 述：部门信息
    /// </summary>
    public class DepartmentMap : EntityTypeConfiguration<HrDepartmentEntity>
    {
        public DepartmentMap()
        {
            #region 表、主键
            //表
            this.ToTable("hr_department");
            //主键
            this.HasKey(t => t.deptid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
