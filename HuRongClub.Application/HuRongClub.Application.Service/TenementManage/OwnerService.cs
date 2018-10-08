using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// �� �� 6.1
    
    /// �� ������������Ա
    /// �� �ڣ�2017-04-01 10:57
    /// �� ����Owner
    /// </summary>
    public class OwnerService : RepositoryFactory, OwnerIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<OwnerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<OwnerEntity>(pagination);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<OwnerEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable<OwnerEntity>().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public OwnerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<OwnerEntity>(keyValue);
        }
        /// <summary>
        /// ��Ԫҵ����Ϣ
        /// </summary>
        /// <param name="id">���</param>
        /// <param name="property_id">��ҵ���</param>
        /// <param name="type">1��room_id ��ѯ 2 ��owner_id ��ѯ</param>
        /// <returns></returns>
        public IEnumerable<OwnerModelEntity> GetInfo(string id, string property_id,int type)
        {
            RepositoryFactory<OwnerModelEntity> repository = new RepositoryFactory<OwnerModelEntity>();
            var StrSql = new StringBuilder();
            StrSql.Append("SELECT ow.*,pr.property_name,(bg.building_name+'/'+r.room_name+' ��')AS room_names,r.building_dim,r.room_dim ");
            StrSql.Append("FROM wy_owner ow ");
            StrSql.Append("INNER JOIN dbo.wy_property pr ON ow.property_id=pr.property_id  ");
            StrSql.Append("INNER JOIN wy_room r ON ow.room_id=r.room_id ");
            StrSql.Append("LEFT JOIN wy_building bg ON r.building_id=bg.building_id ");
            StrSql.Append("WHERE  ow.property_id=@property_id ");

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));
            if (type == 1)
            {
                StrSql.Append(" AND ow.room_id=@room_id "); 
                parameter.Add(DbParameters.CreateDbParameter("@room_id", id));
            }
            else if (type == 2)
            {
                StrSql.Append(" AND ow.owner_id=@owner_id ");
                parameter.Add(DbParameters.CreateDbParameter("@owner_id", id));
            }            

            return repository.BaseRepository().FindList(StrSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="property_id">��ҵ���</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <param name="pagination">��ҳ</param>
        /// <returns></returns>
        public IEnumerable<OwnerListEntity> GetInfoList(string property_id,string queryJson, Pagination pagination)
        {
            RepositoryFactory<OwnerListEntity> repository = new RepositoryFactory<OwnerListEntity>();
            var StrSql = new StringBuilder();
            StrSql.Append("SELECT  ow.owner_id,ow.owner_name,ow.owner_tel,ow.owner_cardno,ow.room_id,ow.in_date,ow.out_date,ow.sign_userid,ow.sign_date,r.is_rent,r.building_dim, r.room_name,r.is_owner,(d.building_name + '-' + r.room_name ) AS unitroom ");
            StrSql.Append("FROM wy_owner ow ");
            StrSql.Append("LEFT JOIN wy_room r ON ow.room_id = r.room_id ");
            StrSql.Append("LEFT JOIN wy_building d ON r.building_id = d.building_id ");
            StrSql.Append("WHERE ow.property_id=@property_id ");

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));

            var queryParam = queryJson.ToJObject();
            if (!queryParam["building_id"].IsEmpty())
            {
                string building_id = queryParam["building_id"].ToString();
                StrSql.Append(" AND r.building_id=@building_id ");
                parameter.Add(DbParameters.CreateDbParameter("@building_id", building_id));
            }
            if (!queryParam["room_id"].IsEmpty())
            {
                string room_id = queryParam["room_id"].ToString();
                StrSql.Append(" AND ow.room_id=@room_id");
                parameter.Add(DbParameters.CreateDbParameter("@room_id", room_id));
            }
            if (!queryParam["owner_name"].IsEmpty())
            {
                string owner_name = queryParam["owner_name"].ToString();
                StrSql.Append(" AND ow.owner_name like @owner_name ");
                parameter.Add(DbParameters.CreateDbParameter("@owner_name", "%"+ owner_name + "%"));
            }

            if (!queryParam["StartDate"].IsEmpty()&& !queryParam["EndDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                string EndDate = queryParam["EndDate"].ToString();
                StrSql.Append(" AND ow.in_date BETWEEN @StartDate AND @EndDate ");

                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }

            return repository.BaseRepository().FindList(StrSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="pos">λ�� ����ǰ�油0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(owner_id,8))+1 from wy_owner ");
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
        /// <summary>
        /// ҵ������
        /// </summary>
        /// <param name="building_id"></param>
        /// <returns></returns>
        public IEnumerable<OwnerEntity> GetListBySel(string building_id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT wo.owner_id,(wr.room_name+'/'+owner_name)owner_name 
                        FROM dbo.wy_owner wo
                        LEFT JOIN dbo.wy_room wr ON wo.room_id=wr.room_id
                        WHERE wr.is_owner=1 AND wr.building_id=@building_id  ORDER BY wr.room_name ASC  ");

            DbParameter[] param = {
                DbParameters.CreateDbParameter("@building_id", building_id)
            };

            return this.BaseRepository().FindList<OwnerEntity>(strSql.ToString(), param);
        }
        #endregion

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
        public string SaveForm(string keyValue, OwnerEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                
                return this.BaseRepository().Update(entity).ToString();
            }
            else
            {
                //entity.Create();
                entity.owner_id = entity.property_id + GetMaxID(8);
                this.BaseRepository().Insert(entity);
                
                RepositoryFactory<RoomEntity> repository = new RepositoryFactory<RoomEntity>();
                RoomEntity rom = new RoomEntity();
                rom.is_owner = 1;
                rom.owner_id = entity.owner_id;
                rom.room_id = entity.room_id;
                repository.BaseRepository().Update(rom);

                return entity.owner_id;
            }
        }
        /// <summary>
        /// �޸�ҵ������
        /// </summary>
        /// <param name="owner_id">ҵ�����</param>
        /// <param name="owner_name">ҵ������</param>
        public void UpdateOwnerName(string owner_id, string owner_name)
        {
            var StrSql = new StringBuilder();
            StrSql.Append("UPDATE wy_owner SET owner_name=@owner_name WHERE owner_id=@owner_id ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@owner_id",owner_id),
                    DbParameters.CreateDbParameter("@owner_name",owner_name)
                };

            this.BaseRepository().FindList<OwnerEntity>(StrSql.ToString(), parameter);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="list"></param>
        public void BatchFrom(List<OwnerEntity> list)
        {
            if (list != null && list.Count > 0)
            {
                IRepository db = this.BaseRepository().BeginTrans();
                try
                {
                    int maxid = GetMaxID(0).ToInt();
                    foreach (OwnerEntity item in list)
                    {
                        item.owner_id = item.property_id + Utils.SupplementZero(maxid.ToString(), 8);                        
                        db.Insert(item);

                        RoomEntity rom = new RoomEntity();
                        rom.is_owner = 1;
                        rom.owner_id = item.owner_id;
                        rom.room_id = item.room_id;
                        db.Update(rom);

                        maxid++;
                    }
                    db.Commit();
                }
                catch (System.Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        #endregion
    }
}
