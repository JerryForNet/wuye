using HuRongClub.Application.Entity.RepostryManage;
using System.Data.Entity.ModelConfiguration;

namespace HuRongClub.Application.Mapping.RepostryManage
{
    /// <summary>
    /// �� ��
    
    /// �� ��������
    /// �� �ڣ�2017-04-01 13:28
    /// �� ������Ʒ���
    /// </summary>
    public class GoodstypeMap : EntityTypeConfiguration<GoodstypeEntity>
    {
        public GoodstypeMap()
        {
            #region ������
            //��
            this.ToTable("tb_wh_goodstype");
            //����
            this.HasKey(t => t.ftypecode);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
