using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using HuRongClub.Application.Entity.TenementManage.ViewModel;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    /// �� ����������
    /// �� �ڣ�2017-04-01 10:09
    /// �� ������ҵ����
    /// </summary>
    public interface PropertyIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<PropertyEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<PropertyEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        PropertyEntity GetEntity(string keyValue);
        /// <summary>
        /// ��ȡ��ҵ�����б�
        /// </summary>
        /// <param name="property_ids">property_id ����</param>
        /// <returns></returns>
        IEnumerable<PropertyEntity> GetListBySel(string property_ids);
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns></returns>
        IEnumerable<OutInModel> GetInList();
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        IEnumerable<OutInModel> GetOutList();

        #endregion

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
        void SaveForm(string keyValue, PropertyEntity entity);
        #endregion
    }
}
