using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuRongClub.Application.IService.PersonnelManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 09:43
    /// �� ����Ա������
    /// </summary>
    public interface EmployinfoIService
    {
        #region ��ȡ����

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<EmployinfoEntity> GetList();

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<EmployinfoListEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// �û����б�(ALL)
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployinfoEntity> GetAllList();

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        EmployinfoEntity GetEntity(int keyValue);

        /// <summary>
        /// ��ȡ��Ϣ--����
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetListInfo(string queryJson);

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="deptid">���ű��</param>
        /// <returns></returns>
        IEnumerable<EmployinfoEntity> GetList(int deptid);

        #endregion ��ȡ����

        #region �ύ����

        /// <summary>
        /// �޸��û�״̬
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="State">״̬��1-������0-����</param>
        void UpdateState(string keyValue, Int16 State, DateTime? fireoutdate);

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
        void SaveForm(string keyValue, EmployinfoEntity entity);

        /// <summary>
        /// ��ͬ����
        /// </summary>
        /// <param name="keyValue">����Ա�����</param>
        /// <param name="firedate">����ʱ��</param>
        void ExpireFrom(string keyValue, DateTime? firedate);

        #endregion �ύ����
    }
}