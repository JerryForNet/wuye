using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// �� ��
    
    /// �� ��������
    /// �� �ڣ�2017-04-01 10:41
    /// �� ����������Ϣ
    /// </summary>
    public class DepartmentMap : EntityTypeConfiguration<HrDepartmentEntity>
    {
        public DepartmentMap()
        {
            #region ������
            //��
            this.ToTable("hr_department");
            //����
            this.HasKey(t => t.deptid);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
