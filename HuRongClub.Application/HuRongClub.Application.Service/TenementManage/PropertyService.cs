using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Application.IService.TenementManage;
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

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    /// �� ����������
    /// �� �ڣ�2017-04-01 10:09
    /// �� ������ҵ����
    /// </summary>
    public class PropertyService : RepositoryFactory<PropertyEntity>, PropertyIService
    {
        #region ��ȡ����

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<PropertyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var expression = LinqExtensions.True<PropertyEntity>();
            if (!queryParam["owner_name"].IsEmpty())
            {
                string para = queryParam["owner_name"].ToString();
                expression = expression.And(t => t.property_name.Contains(para));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<PropertyEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.property_id).ToList();
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public PropertyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// ��ȡ��ҵ�����б�
        /// </summary>
        /// <param name="property_ids">property_id ����</param>
        /// <returns></returns>
        public IEnumerable<PropertyEntity> GetListBySel(string property_ids)
        {
            var expression = LinqExtensions.True<PropertyEntity>();
            if (!string.IsNullOrEmpty(property_ids))
            {
                if (property_ids != "System")
                {
                    string[] pids = property_ids.Split(',');
                    expression = expression.And(t => pids.Contains(t.property_id));
                }
            }
            else
            {
                expression = expression.And(t => t.property_id == " ");
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.property_id).ToList();
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OutInModel> GetInList()
        {
            RepositoryFactory<OutInModel> repository = new RepositoryFactory<OutInModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT SUM(tab.fmoney) AS amount,tab.years,tab.months FROM ( ");
            strSql.Append(@" SELECT fmoney=sum(a.fmoney),YEAR(b.findate) AS years,MONTH(b.findate) AS months ");
            strSql.Append(@" FROM tb_wh_inbill_item a ");
            strSql.Append(@" LEFT join tb_wh_inbill b on a.finbillid = b.finbillid where b.findate IS NOT NULL ");
            strSql.Append(@" AND (YEAR(b.findate)=YEAR(DATEADD(MONTH,-13,GETDATE())) AND MONTH(b.findate)>=MONTH(DATEADD(MONTH,-13,GETDATE()))) ");
            strSql.Append(@" OR (YEAR(b.findate)=YEAR(GETDATE()) AND MONTH(b.findate)<=MONTH(DATEADD(MONTH,-1,GETDATE()))) ");
            strSql.Append(@" GROUP by b.findate)tab ");
            strSql.Append(@" GROUP BY tab.years,tab.months ");
            strSql.Append(@" ORDER BY tab.years ");
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OutInModel> GetOutList()
        {
            RepositoryFactory<OutInModel> repository = new RepositoryFactory<OutInModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT SUM(tab.fmoney) AS amount,tab.years,tab.months FROM ( ");
            strSql.Append(@" SELECT fmoney=sum(a.fmoney),YEAR(b.foutdate) AS years,MONTH(b.foutdate) AS months ");
            strSql.Append(@" FROM tb_wh_outbill_item a ");
            strSql.Append(@" LEFT join tb_wh_outbill b on a.foutbillid = b.foutbillid ");
            strSql.Append(@" WHERE b.foutdate IS NOT NULL ");
            strSql.Append(@" AND (YEAR(b.foutdate)=YEAR(DATEADD(MONTH,-13,GETDATE())) AND MONTH(b.foutdate)>=MONTH(DATEADD(MONTH,-13,GETDATE()))) ");
            strSql.Append(@" OR (YEAR(b.foutdate)=YEAR(GETDATE()) AND MONTH(b.foutdate)<=MONTH(DATEADD(MONTH,-1,GETDATE()))) ");
            strSql.Append(@" GROUP BY  b.foutdate)tab ");
            strSql.Append(@" GROUP BY tab.years,tab.months ");
            strSql.Append(@" ORDER BY tab.years ");
            return repository.BaseRepository().FindList(strSql.ToString());
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
        public void SaveForm(string keyValue, PropertyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                entity.property_id = GetMaxID(4);
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="pos">λ�� ����ǰ�油0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(property_id,4))+1 FROM wy_property");
            string str = "1";
            object obj = this.BaseRepository().FindObject(strSql.ToString());
            if (obj != null)
            {
                str = obj.ToString();
            }
            if (str.Length < pos)
            {
                int leng = str.Length;
                for (int i = 0; i < (pos - leng); i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }

        #endregion �ύ����
    }
}