using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    
    /// �� ������������Ա
    /// �� �ڣ�2017-04-01 10:57
    /// �� ����Owner
    /// </summary>
    public interface OwnerIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<OwnerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<OwnerEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        OwnerEntity GetEntity(string keyValue);
        /// <summary>
        /// ��Ԫҵ����Ϣ
        /// </summary>
        /// <param name="id">������</param>
        /// <param name="property_id">��ҵ���</param>
        /// <param name="type">1��room_id ��ѯ 2 ��owner_id ��ѯ</param>
        /// <returns></returns>
        IEnumerable<OwnerModelEntity> GetInfo(string id, string property_id, int type);
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="property_id">��ҵ���</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <param name="pagination">��ҳ</param>
        /// <returns></returns>
        IEnumerable<OwnerListEntity> GetInfoList(string property_id,string queryJson, Pagination pagination);
        /// <summary>
        /// ҵ������
        /// </summary>
        /// <param name="building_id"></param>
        /// <returns></returns>
        IEnumerable<OwnerEntity> GetListBySel(string building_id);
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
        string SaveForm(string keyValue, OwnerEntity entity);
        /// <summary>
        /// �޸�ҵ������
        /// </summary>
        /// <param name="owner_id">ҵ�����</param>
        /// <param name="owner_name">ҵ������</param>
        void UpdateOwnerName(string owner_id, string owner_name);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="list"></param>
        void BatchFrom(List<OwnerEntity> list);
        #endregion
    }
}
