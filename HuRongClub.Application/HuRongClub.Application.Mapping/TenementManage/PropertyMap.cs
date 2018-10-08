using HuRongClub.Application.Entity.TenementManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    /// �� ����������
    /// �� �ڣ�2017-04-01 10:09
    /// �� ������ҵ����
    /// </summary>
    public class PropertyMap : EntityTypeConfiguration<PropertyEntity>
    {
        public PropertyMap()
        {
            #region ������
            //��
            this.ToTable("wy_property");
            //����
            this.HasKey(t => t.property_id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
