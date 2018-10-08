using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 18:43
    /// 描 述：员工家庭信息
    /// </summary>
    public class EmployfamilyMap : EntityTypeConfiguration<EmployfamilyEntity>
    {
        public EmployfamilyMap()
        {
            #region 表、主键
            //表
            this.ToTable("hr_employfamily");
            //主键
            this.HasKey(t => t.memberid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
