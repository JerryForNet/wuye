using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.PersonnelManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 10:41
    /// �� ����������Ϣ
    /// </summary>
    public interface HrDepartmentIService
    {
        #region ��ȡ����

        /// <summary>
        /// �б�
        /// </summary>
        /// <returns></returns>
        IEnumerable<HrDepartmentEntity> GetList();

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<HrDepartmentEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<HrDepartmentEntity> GetList(string queryJson);

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        HrDepartmentEntity GetEntity(int keyValues);

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
        void SaveForm(string keyValue, HrDepartmentEntity entity);

        #endregion �ύ����
    }
}