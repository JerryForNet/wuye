using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 16:33
    /// 描 述：员工履历信息
    /// </summary>
    public class EmployresumeMap : EntityTypeConfiguration<EmployresumeEntity>
    {
        public EmployresumeMap()
        {
            #region 表、主键
            //表
            this.ToTable("hr_employresume");
            //主键
            this.HasKey(t => t.itemid);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
