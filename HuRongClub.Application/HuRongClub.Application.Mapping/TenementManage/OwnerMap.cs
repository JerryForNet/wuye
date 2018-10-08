using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// �� ��
    
    /// �� ������������Ա
    /// �� �ڣ�2017-04-01 10:57
    /// �� ����Owner
    /// </summary>
    public class OwnerMap : EntityTypeConfiguration<OwnerEntity>
    {
        public OwnerMap()
        {
            #region ������
            //��
            this.ToTable("wy_owner");
            //����
            this.HasKey(t => t.owner_id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
