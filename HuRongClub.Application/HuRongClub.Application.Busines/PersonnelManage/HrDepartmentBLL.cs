using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Application.Service.PersonnelManage;
using HuRongClub.Cache.Factory;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.PersonnelManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 10:41
    /// �� ����������Ϣ
    /// </summary>
    public class HrDepartmentBLL
    {
        private HrDepartmentIService service = new HrDepartmentService();

        /// <summary>
        /// ����key
        /// </summary>
        public string cacheKey = "HrDepartmentCache";

        #region ��ȡ����

        /// <summary>
        /// �б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HrDepartmentEntity> GetList()
        {
            return service.GetList();
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<HrDepartmentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<HrDepartmentEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public HrDepartmentEntity GetEntity(int keyValue)
        {
            int keyValues = Convert.ToInt32(keyValue);
            return service.GetEntity(keyValues);
        }

        #endregion ��ȡ����

        #region �ύ����

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                
                CacheFactory.Cache().RemoveCache(cacheKey);
                CacheFactory.Cache().RemoveCache("DepartmentCache");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, HrDepartmentEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
                CacheFactory.Cache().RemoveCache(cacheKey);
                CacheFactory.Cache().RemoveCache("DepartmentCache");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion �ύ����
    }
}