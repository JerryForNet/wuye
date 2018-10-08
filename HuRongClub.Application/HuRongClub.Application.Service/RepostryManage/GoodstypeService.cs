using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// �� �� 6.1

    /// �� ��������
    /// �� �ڣ�2017-04-01 13:28
    /// �� ������Ʒ���
    /// </summary>
    public class GoodstypeService : RepositoryFactory, GoodstypeIService
    {
        #region ��ȡ����

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<GoodstypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<GoodstypeEntity> repository = new RepositoryFactory<GoodstypeEntity>();
            return repository.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<GoodstypeEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<GoodstypeEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();

                #region ��������ѯ

                switch (condition)
                {
                    case "ftypename":    //����
                        expression = expression.And(t => t.ftypename.Contains(keyword));
                        break;

                    case "ftypecode":    //����
                        expression = expression.And(t => t.ftypecode.Contains(keyword));
                        break;

                    default:
                        break;
                }

                #endregion ��������ѯ
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<GoodstypeEntity> GetList()
        {
            RepositoryFactory<GoodstypeEntity> repository = new RepositoryFactory<GoodstypeEntity>();
            return repository.BaseRepository().IQueryable().ToList();
        }

        public IEnumerable<GoodstypeEntity> GetListJson(string fparentcode)
        {
            return this.BaseRepository().FindList<GoodstypeEntity>("select ftypecode,ftypename,frootid from tb_wh_goodstype where fparentcode='" + fparentcode + "'");
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public GoodstypeEntity GetEntity(string keyValue)
        {
            RepositoryFactory<GoodstypeEntity> repository = new RepositoryFactory<GoodstypeEntity>();
            return repository.BaseRepository().FindEntity(keyValue);
        }

        #endregion ��ȡ����

        #region �ύ����

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, GoodstypeEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        #endregion �ύ����
    }
}