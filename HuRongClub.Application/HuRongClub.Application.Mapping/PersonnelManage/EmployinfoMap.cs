using HuRongClub.Application.Entity.PersonnelManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.PersonnelManage
{
    /// <summary>
    /// �� ��
    
    /// �� ��������
    /// �� �ڣ�2017-04-01 09:43
    /// �� ����Ա������
    /// </summary>
    public class EmployinfoMap : EntityTypeConfiguration<EmployinfoEntity>
    {
        public EmployinfoMap()
        {
            #region ������
            //��
            this.ToTable("hr_employinfo");
            //����
            this.HasKey(t => t.empid);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
