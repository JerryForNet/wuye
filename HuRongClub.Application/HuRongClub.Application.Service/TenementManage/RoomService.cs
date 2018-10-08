using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：Jerry.Li
    /// 日 期：2017-04-10 15:27
    /// 描 述：Room
    /// </summary>
    public class RoomService : RepositoryFactory<RoomEntity>, RoomIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RoomEntity> GetPageList(Pagination pagination, string queryJson)
        {
            //var expression = LinqExtensions.True<RoomEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            string building_id = queryParam["building_id"].ToString();
            int floor_number = queryParam["floor_number"].ToInt();

            var strSql = new StringBuilder();
            strSql.Append("SELECT room_id,room_name,building_id,owner_id,floor_number,room_number,building_dim,room_dim,isnull(is_rent,0) as is_rent,isnull(is_owner,0) as is_owner,d.itemname AS room_model ");
            strSql.Append("FROM wy_room r ");
            strSql.Append("LEFT JOIN (SELECT * FROM sys_dictitem WHERE dictid=2) d ON r.room_model=d.itemid ");
            strSql.AppendFormat("WHERE building_id='{0}' and floor_number={1} ", building_id, floor_number);

            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RoomEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoomEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(room_id,4))+1 FROM wy_room");
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
        /// 获取房屋类型下拉列表
        /// </summary>
        /// <param name="dictid">主表ID</param>
        /// <returns></returns>
        public IEnumerable<Entity.SysManage.DictitemEntity> GetListBySel(int dictid)
        {
            RepositoryFactory<Entity.SysManage.DictitemEntity> repository = new RepositoryFactory<Entity.SysManage.DictitemEntity>();
            var expression = LinqExtensions.True<Entity.SysManage.DictitemEntity>();
            if (dictid != 0)
            {
                expression = expression.And(t => t.dictid == dictid);
            }
            return repository.BaseRepository().IQueryable(expression).OrderBy(t => t.itemid).ToList();
        }

        /// <summary>
        /// 获取房屋 room_number
        /// </summary>
        /// <param name="building_id">所属楼栋编号</param>
        /// <param name="floor_number">所属楼层</param>
        /// <returns></returns>
        public int GetMaxRoom_number(string building_id, int floor_number)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT ISNULL(MAX(room_number),0)+1 FROM dbo.wy_room ");
            strSql.AppendFormat("WHERE building_id='{0}' AND floor_number={1}", building_id, floor_number);
            object obj = this.BaseRepository().FindObject(strSql.ToString());
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return obj.ToInt();
            }
        }

        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="room_id">房间编号</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<RoomModelEntity> GetInfo(string room_id, string property_id)
        {
            RepositoryFactory<RoomModelEntity> repository = new RepositoryFactory<RoomModelEntity>();
            var StrSql = new StringBuilder();
            StrSql.Append("SELECT r.*,bg.building_name,ow.owner_name ");
            StrSql.Append("FROM wy_room r ");
            StrSql.Append("LEFT JOIN wy_building bg ON r.building_id=bg.building_id ");
            StrSql.Append("LEFT JOIN wy_owner ow ON r.owner_id=ow.owner_id ");
            StrSql.Append("WHERE r.room_id=@room_id AND r.property_id=@property_id ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@room_id",room_id),
                    DbParameters.CreateDbParameter("@property_id",property_id)
                };

            return repository.BaseRepository().FindList(StrSql.ToString(), parameter);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="building_id">所属楼栋编号</param>
        /// <param name="Type">1 全部 2空房间</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RoomEntity> GetTreeList(string building_id, int Type)
        {
            var expression = LinqExtensions.True<RoomEntity>();
            expression = expression.And(t => t.building_id == building_id);
            if (Type == 2)
            {
                expression = expression.And(t => (t.is_owner == 0 || t.is_owner == null));
            }
            else if (Type == 3)
            {
                expression = expression.And(t => t.is_owner == 1);
            }

            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.room_name);
        }

        /// <summary>
        /// 租赁单元
        /// </summary>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        public IEnumerable<RoomModelEntity> GetListByRent(string contractid)
        {
            RentcontractService rent = new RentcontractService();

            string room_ids = rent.GetRentcell(contractid);

            RepositoryFactory<RoomModelEntity> repository = new RepositoryFactory<RoomModelEntity>();
            string StrSql = string.Format(@"SELECT  * ,
                                                    ( SELECT TOP 1
                                                                building_name
                                                      FROM      wy_building
                                                      WHERE     building_id = wy_room.building_id
                                                    ) + '/' + room_name AS building_name ,
                                                    '' AS owner_name
                                            FROM    wy_room
                                            WHERE   room_id IN ({0})
                                            ORDER BY room_name ASC", room_ids == "" ? "''" : room_ids);

            // 下方写法不利于阅读 update:2017/10/12 Jerry.Li
            //var StrSql = new StringBuilder();
            //StrSql.Append("select *,(select top 1 building_name from wy_building where building_id=wy_room.building_id)+'/'+room_name as building_name,''AS owner_name  ");
            //StrSql.AppendFormat("FROM wy_room where room_id in ({0}) ", room_ids == "" ? "''" : room_ids);

            return repository.BaseRepository().FindList(StrSql.ToString()).OrderBy(t => t.building_id);
        }

        /// <summary>
        /// 获取批量费用导出数据
        /// </summary>
        /// <param name="Type">1 业主 2 租户</param>
        /// <param name="id">查询id</param>
        /// <returns></returns>
        public DataTable GetListByFee(int Type, string id)
        {
            var StrSql = new StringBuilder();
            if (Type == 1)
            {
                StrSql.Append(@"SELECT wy_room.owner_id AS oid,wy_room.room_id AS rid,wy_room.room_name,wy_owner.owner_name AS oname,wy_room.building_dim AS dim ,'' as fee_income
                        FROM wy_room
                        LEFT join wy_owner on wy_owner.owner_id=wy_room.owner_id
                        WHERE wy_room.building_id=@id  order by wy_room.floor_number, wy_room.room_name ");
            }
            else
            {
                StrSql.Append(@"select '' as oid,contractid  AS rid,(select room_name from wy_room where room_id=substring(rentcell,1,10)) as room_name,customername AS oname,rentarea AS dim,'' as fee_income
                            FROM wy_rentcontract
                            WHERE status=1 and property_id=@id
                            ORDER by customername ");
            }

            DbParameter[] parameter ={
                DbParameters.CreateDbParameter("@id",id)
            };

            RepositoryFactory<FeeManageEntity> repository = new RepositoryFactory<FeeManageEntity>();
            return repository.BaseRepository().FindTable(StrSql.ToString(), parameter);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">owner_id</param>
        /// <returns></returns>
        public RoomEntity GetEntitys(string keyValue)
        {
            var expression = LinqExtensions.True<RoomEntity>();
            expression = expression.And(t => t.owner_id == keyValue);
            //expression = expression.And(t => t.is_owner == 1);

            return this.BaseRepository().FindEntity(expression);
        }

        /// <summary>
        /// 获取空房间信息（用于下载批量导出数据）
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="building_id">归属楼栋</param>
        /// <returns></returns>
        public DataTable GetRoomInfo(string property_id, string building_id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT r.room_id,b.building_name,r.room_name,r.building_dim,r.room_dim,r.room_number,'' AS owner_name
                            ,'' AS owner_tel,'身份证' AS owner_cardtype,'' AS owner_cardno,'' AS in_date,'' AS sign_date,'' AS link1_name,'' AS link1_tel,'' AS link1_mark,'' AS link2_name,'' AS link2_tel,'' AS link2_mark,'' AS remark
                            FROM wy_Room r
                            INNER JOIN dbo.wy_building b ON r.building_id=b.building_id
                            WHERE (is_owner=0 OR is_owner IS NULL)
                            AND r.property_id=@property_id
                            AND r.building_id=@building_id
                            ORDER BY r.room_name ASC,r.room_number ASC");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@property_id",property_id),
                    DbParameters.CreateDbParameter("@building_id",building_id)
                };

            RepositoryFactory<FeeManageEntity> repository = new RepositoryFactory<FeeManageEntity>();
            return repository.BaseRepository().FindTable(strSql.ToString(), parameter);
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
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RoomEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                //this.BaseRepository().Update(entity);

                string strSql = @"UPDATE  wy_room
                                SET     floor_number = @floor_number ,
                                        room_name = @room_name ,
                                        building_dim = @building_dim ,
                                        room_model = @room_model
                                WHERE   room_id = @room_id ";

                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@floor_number",entity.floor_number),
                    DbParameters.CreateDbParameter("@room_name",entity.room_name),
                    DbParameters.CreateDbParameter("@building_dim",entity.building_dim),
                    DbParameters.CreateDbParameter("@room_model",string.IsNullOrEmpty(entity.room_model)?string.Empty:entity.room_model),
                    DbParameters.CreateDbParameter("@room_id",entity.room_id)
                };

                this.BaseRepository().ExecuteBySql(strSql, parameter);
            }
            else
            {
                //entity.Create();
                entity.room_id = entity.property_id + GetMaxID(4);
                entity.room_number = GetMaxRoom_number(entity.building_id, entity.floor_number.ToInt());

                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 修改表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(RoomEntity entity)
        {
            entity.Modify(entity.room_id);
            this.BaseRepository().Update(entity);
        }

        /// <summary>
        /// 出户操作
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="owner_id"></param>
        public void SaveOutFrom(string room_id, string owner_id)
        {
            RoomEntity rom = new RoomEntity();
            rom.is_owner = 0;
            rom.owner_id = null;
            rom.room_id = room_id;
            this.BaseRepository().Update(rom);

            RepositoryFactory<OwnerEntity> repository = new RepositoryFactory<OwnerEntity>();
            OwnerEntity own = new OwnerEntity();
            own.owner_id = owner_id;
            own.out_date = DateTime.Now;
            repository.BaseRepository().Update(own);
        }

        #endregion

        /// <summary>
        /// 更新房间状态
        /// </summary>
        /// <param name="roomids"></param>
        /// <param name="status"></param>
        public void UpdateRent(string roomids, int status)
        {
            if (!string.IsNullOrEmpty(roomids))
            {
                string[] room_ids = roomids.Split(',');
                for (int i = 0; i < room_ids.Length; i++)
                {
                    string strSql = string.Format("UPDATE dbo.wy_room SET is_rent = {0} WHERE room_id IN ({1})", status, roomids);

                    this.BaseRepository().ExecuteBySql(strSql);
                }
            }
        }
    }
}