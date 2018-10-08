using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;
using HuRongClub.Application.Entity.TenementManage.ViewModel;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    /// �� ����������
    /// �� �ڣ�2017-04-01 10:09
    /// �� ������ҵ����
    /// </summary>
    public class PropertyBLL
    {
        private PropertyIService service = new PropertyService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<PropertyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<PropertyEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public PropertyEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }


        /// <summary>
        /// ��ȡ��ҵ�����б�
        /// </summary>
        /// <param name="property_ids">property_id ����</param>
        /// <returns></returns>
        public IEnumerable<PropertyEntity> GetListBySel(string property_ids)
        {
            return service.GetListBySel(property_ids);
        }
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OutInModel> GetInList()
        {
            return service.GetInList();
        }
        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OutInModel> GetOutList()
        {
            return service.GetOutList();
        }


        #endregion

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
        public void SaveForm(string keyValue, PropertyEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
