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
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:07
    /// 描 述：Owner_fee
    /// </summary>
    public class Owner_feeService : RepositoryFactory<Owner_feeEntity>, Owner_feeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Owner_feeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Owner_feeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Owner_feeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="owner_id">业主编号</param>
        /// <param name="room_id">房屋编号</param>
        /// <returns></returns>
        public IEnumerable<OwnerFeeModelEntity> GetList(string owner_id, string room_id)
        {
            RepositoryFactory<OwnerFeeModelEntity> repository = new RepositoryFactory<OwnerFeeModelEntity>();
            var StrSql = new StringBuilder();
            StrSql.Append("SELECT F.*,ow.owner_name,i.feeitem_name  ");
            StrSql.Append("FROM wy_owner_fee F ");
            StrSql.Append("LEFT join wy_feeitem  I on F.fee_code =  I.feeitem_id  ");
            StrSql.Append("LEFT JOIN wy_owner ow ON f.owner_id=ow.owner_id ");
            StrSql.Append("WHERE F.owner_id=@owner_id and F.room_id=@room_id ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@owner_id",owner_id),
                    DbParameters.CreateDbParameter("@room_id",room_id)
                };

            return repository.BaseRepository().FindList(StrSql.ToString(), parameter).OrderByDescending(t => t.start_date);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(owner_feeid,8))+1 FROM wy_owner_fee ");
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
        /// 获取用户编号
        /// </summary>
        /// <param name="pagination">排序</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<OwnerFeeIndexEntity> GetList(Pagination pagination, string queryJson, string property_id)
        {
            RepositoryFactory<OwnerFeeIndexEntity> repository = new RepositoryFactory<OwnerFeeIndexEntity>();
            var StrSql = new StringBuilder();
            StrSql.Append(@"SELECT  *
                            FROM    ( SELECT    f.owner_feeid ,
                                                f.fee_money ,
                                                f.start_date ,
                                                r.owner_id ,
                                                ISNULL(o.owner_name, '--') AS ownerName ,
                                                ( wb.building_name + '/' + r.room_name ) room_name ,
                                                i.feeitem_name ,
                                                i.feeitem_id ,
                                                r.building_dim,
											    wb.building_id,
											    r.room_id
                                      FROM      wy_owner_fee f
                                                LEFT JOIN wy_room r ON f.owner_id = r.owner_id
                                                LEFT JOIN wy_feeitem i ON f.fee_code = i.feeitem_id
                                                LEFT JOIN wy_owner o ON r.owner_id = o.owner_id
                                                LEFT JOIN dbo.wy_building wb ON r.building_id = wb.building_id
                                      WHERE     o.property_id = @property_id");

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));

            var queryParam = queryJson.ToJObject();

            if (!queryParam["building_id"].IsEmpty())
            {
                string building_id = queryParam["building_id"].ToString();
                StrSql.Append(" AND r.building_id=@building_id  ");
                parameter.Add(DbParameters.CreateDbParameter("@building_id", building_id));
            }
            if (!queryParam["feeitem_id"].IsEmpty())
            {
                string feeitem_id = queryParam["feeitem_id"].ToString();
                StrSql.Append(" AND i.feeitem_id=@feeitem_id  ");
                parameter.Add(DbParameters.CreateDbParameter("@feeitem_id", feeitem_id));
            }
            if (!queryParam["owner_name"].IsEmpty())
            {
                string owner_name = queryParam["owner_name"].ToString();
                StrSql.Append(" AND isnull(o.owner_name,'--')=@owner_name  ");
                parameter.Add(DbParameters.CreateDbParameter("@owner_name", owner_name));
            }
            StrSql.Append(")x1 ");

            return repository.BaseRepository().FindList(StrSql.ToString(), parameter.ToArray(), pagination);
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, string property_id, Owner_feeEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity).ToString();
                return entity.owner_feeid;
            }
            else
            {
                entity.owner_feeid = property_id + GetMaxID(8);
                this.BaseRepository().Insert(entity);

                return entity.owner_feeid;
            }
        }

        #endregion
    }
}