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
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 09:28
    /// 描 述：Rentcontract
    /// </summary>
    public class RentcontractService : RepositoryFactory<RentcontractEntity>, RentcontractIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="property_id">区域编号</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RentcontractEntity> GetPageList(Pagination pagination, string queryJson, string property_id)
        {
            var expression = LinqExtensions.True<RentcontractEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["status"].IsEmpty())
            {
                Int16 status = Convert.ToInt16(queryParam["status"]);
                expression = expression.And(t => t.status == status);
            }
            if (!queryParam["customername"].IsEmpty())
            {
                string customername = queryParam["customername"].ToString();
                expression = expression.And(t => t.customername.Contains(customername));
            }
            if (!queryParam["contractcode"].IsEmpty())
            {
                string contractcode = queryParam["contractcode"].ToString();
                expression = expression.And(t => t.contractcode.Contains(contractcode));
            }
            if (!queryParam["expire_begin"].IsEmpty() && !queryParam["expire_end"].IsEmpty())
            {
                DateTime expire_begin = queryParam["expire_begin"].ToDate();
                DateTime expire_end = queryParam["expire_end"].ToDate();
                expression = expression.And(t => DateTime.Compare((DateTime)t.expire_begin, expire_begin) <= 0 && DateTime.Compare((DateTime)t.expire_end, expire_end) >= 0);
            }

            expression = expression.And(t => t.property_id == property_id);
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RentcontractEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RentcontractEntity GetEntity(string keyValue)
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
            strSql.Append(@"select max(right(contractid,6))+1 FROM wy_rentcontract");
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
        /// 获取租赁单元房间号
        /// </summary>
        /// <param name="contractid">编号</param>
        /// <returns></returns>
        public string GetRentcell(string contractid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT ISNULL(rentcell,'') FROM wy_rentcontract WHERE contractid=@contractid ");
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@contractid",contractid) };

            object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 判断租赁单元房间号是否存在
        /// </summary>
        /// <param name="contractid">编号</param>
        /// <param name="rentcell">租赁单元房间号</param>
        /// <returns></returns>
        public bool IsRentcell(string contractid, string rentcell)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat(@"SELECT COUNT(1) FROM wy_rentcontract WHERE rentcell LIKE '%{0}%'  AND CONVERT(VARCHAR(10),expire_end,120) >= CONVERT(VARCHAR(10),GETDATE(),120)  AND status = 1", rentcell);
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@contractid",contractid) };

            object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
            if (obj != null)
            {
                if (obj.ToString() == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<RentcontractTree> GetListBySel(string property_id, int status)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT wr.contractid,(ISNULL(rm.room_name,'')+'('+wr.customername+')') AS customername,(b.building_name + '-' + rm.room_name ) AS unitroom,wr.contractcode,wr.customername AS shortname
                            FROM wy_rentcontract wr
                            LEFT JOIN wy_room rm ON left(wr.rentcell,10)=rm.room_id
                            LEFT JOIN wy_building b ON rm.building_id = b.building_id
                            WHERE rentcell<>'' AND wr.property_id=@property_id ");

            if (status != 0)
            {
                strSql.Append(" AND status = " + status);
            }

            DbParameter[] parameter ={
                DbParameters.CreateDbParameter("@property_id",property_id)
            };

            return new RepositoryFactory<RentcontractTree>().BaseRepository().FindList(strSql.ToString(), parameter).OrderBy(t => t.customername);
            //return this.BaseRepository().FindList(strSql.ToString(), parameter).OrderBy(t => t.customername);
        }

        /// <summary>
        /// 获取前五条到期合同
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RentcontractEntity> GetExpireList(string property_id)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT TOP 5 *
                FROM wy_rentcontract
                WHERE property_id=@property_id AND CONVERT(VARCHAR(10),expire_end,120) BETWEEN CONVERT(VARCHAR(10),GETDATE(),120) AND CONVERT(VARCHAR(10),DATEADD(month,2,GETDATE()),120)
                ORDER BY expire_end ASC");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@property_id",property_id)
                };

            return this.BaseRepository().FindList(strSql.ToString(), parameter);
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
        /// 删除房间号
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="room_id"></param>
        public void RemoveDYForm(string keyValue, string room_id, string rentcell)
        {
            string strRentcell = GetRentcell(keyValue);
            if (!string.IsNullOrEmpty(strRentcell))
            {
                string[] strRentcells = strRentcell.Split(',');
                string str = "";
                foreach (string item in strRentcells)
                {
                    if (item != room_id)
                    {
                        str += item + ",";
                    }
                }
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Substring(0, str.Length - 1);
                }

                rentcell = rentcell.Replace("," + room_id, "");

                var StrSql = new StringBuilder();
                StrSql.Append("update wy_rentcontract set  ");
                StrSql.Append(" rentcell=@rentcell,");
                if (string.IsNullOrEmpty(rentcell))
                {
                    StrSql.Append("rentarea=0  ");
                }
                else {
                    StrSql.AppendFormat("rentarea=CASE WHEN ((SELECT SUM(building_dim) FROM wy_room WHERE room_id IN ({0}))) <= 0 THEN 0 ELSE  (SELECT SUM(building_dim) FROM wy_room WHERE room_id IN ({0})) END ", rentcell);
                }
                
                StrSql.Append("WHERE contractid=@contractid ");

                DbParameter[] parameter ={
                DbParameters.CreateDbParameter("@rentcell",str),
                DbParameters.CreateDbParameter("@room_id",room_id),
                DbParameters.CreateDbParameter("@contractid",keyValue) };

                this.BaseRepository().FindList(StrSql.ToString(), parameter);
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RentcontractEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.status = 0;
                entity.contractid = entity.property_id + GetMaxID(6);
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="status">状态</param>
        public void UpdateStatus(string keyValue, int status)
        {
            var StrSql = new StringBuilder();
            StrSql.Append("UPDATE wy_rentcontract SET status=" + status + " where contractid=@contractid ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@contractid",keyValue) };

            this.BaseRepository().FindList(StrSql.ToString(), parameter);
        }

        /// <summary>
        /// 租凭单元
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="room_id">房屋编号</param>
        /// <param name="IsTrue">是否添加 1未 2 是</param>
        /// <param name="rentcell">关联房号</param>
        public void UpdateRentcell(string keyValue, string room_id, int IsTrue, string rentcell)
        {
            var StrSql = new StringBuilder();
            StrSql.Append("update wy_rentcontract set  ");
            if (IsTrue == 1)
            {
                StrSql.Append(" rentcell=@room_id,");
            }
            else
            {
                StrSql.Append(" rentcell = CASE WHEN (rentcell IS NULL OR rentcell = '') THEN @room_id ELSE rentcell + ',' + @room_id END , ");

                if (string.IsNullOrEmpty(rentcell))
                {
                    rentcell = room_id;
                }
                else
                {
                    rentcell += "," + room_id;
                }
            }

            if (string.IsNullOrEmpty(rentcell))
            {
                StrSql.Append("rentarea=0  ");
            }
            else
            {
                if (rentcell.IndexOf(',') == 0)
                {
                    rentcell = rentcell.Substring(1);
                }
                StrSql.AppendFormat("rentarea=CASE WHEN ((SELECT SUM(building_dim) FROM wy_room WHERE room_id IN ({0}))) <= 0 THEN 0 ELSE  (SELECT SUM(building_dim) FROM wy_room WHERE room_id IN ({0})) END ", rentcell);
            }

            StrSql.Append("WHERE contractid=@contractid ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@room_id",room_id),
                    DbParameters.CreateDbParameter("@contractid",keyValue) };

            this.BaseRepository().FindList(StrSql.ToString(), parameter);
        }

        #endregion

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        public bool SaveContractIncomeForm(string jsonParam)
        {
            try
            {
                var jsonObj = jsonParam.ToJObject();

                DbParameter[] parameter2 ={
                    DbParameters.CreateDbParameter("@propertyid",jsonObj["propertyID"].ToString()),
                    DbParameters.CreateDbParameter("@dtyear",jsonObj["sel_year"].ToString()),
                    DbParameters.CreateDbParameter("@dtmonth",jsonObj["sel_month"].ToString())
                };

                this.BaseRepository().ExecuteByProc("CreateContractIncomeCheck", parameter2);

                string safeSql = string.Empty;
                OptionIService optionService = new OptionService();
                OptionEntity option = optionService.GetEntity(jsonObj["propertyID"].ToString());
                if (option != null)
                {
                    safeSql = "update wy_option set option_exchange=@optionValue,option_point=@optionPoint where property_id=@propertyID";
                }
                else
                {
                    safeSql = "insert into wy_option(property_id,option_exchange,option_point) values (@propertyID,@optionValue,@optionPoint)";
                }

                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@propertyID",jsonObj["propertyID"].ToString()),
                    DbParameters.CreateDbParameter("@optionValue",jsonObj["option_exchange"].ToString()),
                    DbParameters.CreateDbParameter("@optionPoint",jsonObj["option_point"].ToString())
                };

                return this.BaseRepository().ExecuteBySql(safeSql, parameter) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}