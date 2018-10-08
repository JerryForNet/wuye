using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.Service.RepostryManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.RepostryManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 13:28
    /// �� ������Ʒ���
    /// </summary>
    public class GoodstypeBLL
    {
        private GoodstypeIService service = new GoodstypeService();

        /// <summary>
        /// ����key
        /// </summary>
        public string cacheKey = "GoodstypeCache";

        #region ��ȡ����

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<GoodstypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<GoodstypeEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        public IEnumerable<GoodstypeEntity> GetList()
        {
            return service.GetList();
        }

        public IEnumerable<GoodstypeEntity> GetListJson(string fparentcode)
        {
            return service.GetListJson(fparentcode);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public GoodstypeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        public void SaveForm(string keyValue, GoodstypeEntity entity)
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

        #endregion �ύ����
    }
}