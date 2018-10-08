using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.RepostryManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 13:28
    /// �� ������Ʒ���
    /// </summary>
    public interface GoodstypeIService
    {
        #region ��ȡ����

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<GoodstypeEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<GoodstypeEntity> GetList(string queryJson);

        IEnumerable<GoodstypeEntity> GetList();

        IEnumerable<GoodstypeEntity> GetListJson(string fparentcode);

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        GoodstypeEntity GetEntity(string keyValue);

        #endregion ��ȡ����

        #region �ύ����

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, GoodstypeEntity entity);

        #endregion �ύ����
    }
}