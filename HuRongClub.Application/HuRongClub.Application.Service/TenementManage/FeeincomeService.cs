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
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:36
    /// 描 述：费用应收表
    /// </summary>
    public class FeeincomeService : RepositoryFactory<FeeincomeEntity>, FeeincomeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeincomeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeincomeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeincomeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取列表主键集
        /// </summary>
        /// <param name="keyValues">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeincomeEntity> GetLists(string keyValues)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM wy_feeincome WHERE income_id IN ({0})", keyValues);
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="owner_id">业主编号</param>
        /// <param name="property_id">物业名称</param>
        /// <param name="type">1业主已缴费 2业主欠缴费 3单元已缴费 4单元欠缴费 5减免费用</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeincomeListEntity> GetPageList(Pagination pagination, string owner_id, string property_id, int type)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from(select wf.income_id,wf.owner_id,wf.fee_month,wf.fee_year,wf.feeitem_id,wf.rentcontract_id,wf.room_id,wf.fee_income,wf.fee_already  ");
            strSql.Append(",ISNULL((wp.property_name + '/' +wb.building_name + '/' +wr.room_name + '/' + wo.owner_name),wt.customername)  as property_name,wfm.feeitem_name  ");
            strSql.Append(",cast((cast(wf.fee_year as varchar(4)) + '-' + cast(wf.fee_month as varchar(2)) + '-01') as datetime)  as fee_years   ");
            strSql.Append("FROM wy_feeincome wf  ");
            strSql.Append("LEFT JOIN wy_property wp ON wf.property_id=wp.property_id  ");
            strSql.Append("LEFT JOIN wy_building wb ON wf.building_id=wb.building_id  ");
            strSql.Append("LEFT JOIN wy_room wr ON wf.room_id=wr.room_id  ");
            strSql.Append("LEFT JOIN wy_owner wo ON wf.owner_id=wo.owner_id  ");
            strSql.Append("LEFT JOIN wy_feeitem wfm ON wf.feeitem_id=wfm.feeitem_id  ");
            strSql.Append("LEFT JOIN wy_rentcontract wt ON wf.rentcontract_id=wt.contractid  ");
            strSql.Append("WHERE  wf.property_id=@property_id   ");
            if (type == 1)
            {
                strSql.Append(" AND wf.owner_id=@owner_id");
                strSql.Append(" AND isnull(cast(wf.fee_already as float),0) >= isnull(cast(wf.fee_income as float),0)  ");
            }
            else if (type == 2)
            {
                strSql.Append(" AND wf.owner_id=@owner_id");
                strSql.Append(" AND isnull(cast(wf.fee_already as float),0) < isnull(cast(wf.fee_income as float),0)  ");
            }
            else if (type == 3)
            {
                strSql.Append(" AND wf.room_id=@owner_id");
                strSql.Append(" AND isnull(cast(wf.fee_already as float),0) >= isnull(cast(wf.fee_income as float),0)  ");
            }
            else if (type == 4)
            {
                strSql.Append(" AND wf.room_id=@owner_id");
                strSql.Append(" AND isnull(cast(wf.fee_already as float),0) < isnull(cast(wf.fee_income as float),0)  ");
            }
            else if (type == 5)
            {
                strSql.AppendFormat(" AND wf.income_id  IN ({0})", owner_id);
                strSql.Append(" AND isnull(cast(wf.fee_already as float),0) < isnull(cast(wf.fee_income as float),0)  ");
            }
            else if (type == 6)
            {
                strSql.AppendFormat(" AND wf.income_id  IN ({0})", owner_id);
            }
            strSql.AppendFormat(" )x1");
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@owner_id",owner_id),
                    DbParameters.CreateDbParameter("@property_id",property_id)
                };

            RepositoryFactory<FeeincomeListEntity> repository = new RepositoryFactory<FeeincomeListEntity>();
            if (pagination != null)
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter, pagination);//.OrderBy(t=>t.fee_year).ThenBy(t=>t.fee_month);
            }
            else
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter);//.OrderBy(t => t.fee_year).ThenBy(t => t.fee_month);
            }
        }

        /// <summary>
        /// 获取租户缴费情况
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        public IEnumerable<FeeincomeListEntity> GetPageLists(Pagination pagination, string contractid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM( SELECT income_id,fee_month,fee_year,wf.feeitem_id,wf.rentcontract_id,wf.room_id,wf.fee_income,wf.fee_already
                        ,wr.customername AS property_name,wm.feeitem_name
                        ,cast((cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2)) + '-01') as datetime)  as fee_years
                        FROM wy_feeincome wf
                        LEFT JOIN wy_rentcontract wr ON wf.rentcontract_id=wr.contractid
                        LEFT JOIN wy_feeitem wm ON wf.feeitem_id=wm.feeitem_id
                        WHERE wf.rentcontract_id=@contractid
                        AND  isnull(cast(fee_income as float),0) > isnull(cast(fee_already as float),0))X1");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@contractid",contractid)
                };

            RepositoryFactory<FeeincomeListEntity> repository = new RepositoryFactory<FeeincomeListEntity>();
            if (pagination != null)
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter, pagination);//.OrderBy(t => t.fee_year).ThenBy(t => t.fee_month);
            }
            else
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter);//.OrderBy(t => t.fee_year).ThenBy(t => t.fee_month);
            }
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="feeitem_id">费用科目ID</param>
        /// <param name="room_id">房间编号/租赁合同编号</param>
        /// <param name="fee_year">应收日期年</param>
        /// <param name="fee_month">应收日期月</param>
        /// <param name="type">1业主 2租户</param>
        /// <returns></returns>
        public IEnumerable<FeeincomeEntity> GetList(string feeitem_id, string room_id, int fee_year, int fee_month, int type)
        {
            var expression = LinqExtensions.True<FeeincomeEntity>();

            expression = expression.And(t => t.feeitem_id == feeitem_id);
            expression = expression.And(t => t.fee_year == fee_year);
            expression = expression.And(t => t.fee_month == fee_month);
            if (type == 1)
            {
                if (room_id.IndexOf(",") == -1)
                {
                    expression = expression.And(t => t.room_id == room_id);
                }
                else
                {
                    string[] room_ids = room_id.Split(',');
                    expression = expression.And(t => room_ids.Contains(t.room_id));
                }
            }
            else
            {
                expression = expression.And(t => t.rentcontract_id == room_id);
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        public string GetMaxID(int pos)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(right(income_id,8))+1 from wy_feeincome");
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
        /// 实收/应收查询列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="Mode">1实收 2 应收</param>
        /// <returns></returns>
        public IEnumerable<ActualEntity> GetActualList(Pagination pagination, string property_id, string queryJson, int Mode)
        {
            var queryParam = queryJson.ToJObject();
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();
            int type = 1;
            if (!queryParam["type"].IsEmpty())
            {
                type = queryParam["type"].ToInt();
            }

            if (type == 2)
            {
                // ,(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))) as YearMouth,notes
                strSql.Append(@"select (select top 1 customername from wy_rentcontract where contractid=wy_feeincome.rentcontract_id) as owner_name,(select feeitem_name from wy_feeitem where feeitem_id=wy_feeincome.feeitem_id) as feeitem_name,income_id ,fee_income,rentcontract_id,pay_enddate,fee_date,owner_id
                        ,(LEFT(CONVERT(VARCHAR(10), pay_enddate, 120),7)) as YearMouth,notes
                        FROM wy_feeincome
                        WHERE wy_feeincome.property_id=@property_id ");
                if (Mode == 1)
                {
                    strSql.Append(" and fee_date is not null  ");
                }
                if (!queryParam["rentcontract_id"].IsEmpty())
                {
                    strSql.Append(" AND rentcontract_id=@rentcontract_id  ");
                    string rentcontract_id = queryParam["rentcontract_id"].ToString();

                    parameter.Add(DbParameters.CreateDbParameter("@rentcontract_id", rentcontract_id));
                }
            }
            else
            {
                strSql.Append(@"select owner_name=case when rentcontract_id is null or rentcontract_id='' then (select top 1 building_name from wy_building where building_id=wy_feeincome.building_id) + '/' + (select top 1 room_name from wy_room where room_id=wy_feeincome.room_id) + '(' +(select owner_name from wy_owner where owner_id=wy_feeincome.owner_id) + ')' else (select top 1 customername from wy_rentcontract where contractid=wy_feeincome.rentcontract_id) END
                    ,(select feeitem_name from wy_feeitem where feeitem_id=wy_feeincome.feeitem_id) as feeitem_name
                    ,income_id ,fee_income,rentcontract_id,pay_enddate,fee_date,owner_id
                    ,(LEFT(CONVERT(VARCHAR(10), pay_enddate, 120),7)) as YearMouth,notes
                    FROM wy_feeincome
                    WHERE wy_feeincome.property_id=@property_id   ");

                if (Mode == 1)
                {
                    strSql.Append("  AND fee_date is not null  ");
                }
                if (!queryParam["building_id"].IsEmpty())
                {
                    strSql.Append(" AND building_id=@building_id  ");
                    string building_id = queryParam["building_id"].ToString();

                    parameter.Add(DbParameters.CreateDbParameter("@building_id", building_id));
                }
                if (!queryParam["owner_id"].IsEmpty())
                {
                    strSql.Append(" AND owner_id=@owner_id  ");
                    string owner_id = queryParam["owner_id"].ToString();

                    parameter.Add(DbParameters.CreateDbParameter("@owner_id", owner_id));
                }
            }

            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));

            if (!queryParam["feeitem_id"].IsEmpty())
            {
                strSql.Append(" AND feeitem_id=@feeitem_id  ");
                string feeitem_id = queryParam["feeitem_id"].ToString();

                parameter.Add(DbParameters.CreateDbParameter("@feeitem_id", feeitem_id));
            }
            if (!queryParam["state"].IsEmpty() && !queryParam["end"].IsEmpty())
            {
                strSql.Append("  AND fee_date between @state and @end   ");
                string state = queryParam["state"].ToString();
                string end = queryParam["end"].ToString();

                parameter.Add(DbParameters.CreateDbParameter("@state", state));
                parameter.Add(DbParameters.CreateDbParameter("@end", end));
            }

            if (!queryParam["stateY"].IsEmpty() && !queryParam["endY"].IsEmpty())
            {
                strSql.Append("   AND CONVERT(VARCHAR(10),pay_enddate,120) between @stateY and @endY    ");
                string stateY = queryParam["stateY"].ToString();
                string endY = queryParam["endY"].ToString();

                parameter.Add(DbParameters.CreateDbParameter("@stateY", stateY));
                parameter.Add(DbParameters.CreateDbParameter("@endY", endY));
            }

            RepositoryFactory<ActualEntity> repository = new RepositoryFactory<ActualEntity>();

            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination).OrderByDescending(t => t.fee_date);
        }

        /// <summary>
        /// 应收查询不分页
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetActualList(Pagination pagination,string property_id, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                string building_id = queryParam["building_id"] == null ? string.Empty : queryParam["building_id"].ToString();
                string owner_id = queryParam["owner_id"] == null ? string.Empty : queryParam["owner_id"].ToString();
                string feeitem_id = queryParam["feeitem_id"] == null ? string.Empty : queryParam["feeitem_id"].ToString();
                string stateY = queryParam["stateY"] == null ? string.Empty : queryParam["stateY"].ToString();
                string endY = queryParam["endY"] == null ? string.Empty : queryParam["endY"].ToString();
                string rentcontract_id = queryParam["rentcontract_id"] == null ? string.Empty : queryParam["rentcontract_id"].ToString();

                StringBuilder sqls = new StringBuilder();
                sqls.Append(@"SELECT
                                CASE ISNULL(building_id,'')
		                        WHEN '' THEN ( SELECT TOP 1 customername FROM      wy_rentcontract WHERE     contractid = wy_feeincome.rentcontract_id )
		                        ELSE
                                    ( ( SELECT TOP 1
                                                building_name
                                        FROM    wy_building
                                        WHERE   building_id = wy_feeincome.building_id
                                      ) + '/' + ( SELECT TOP 1
                                                            room_name
                                                  FROM      wy_room
                                                  WHERE     room_id = wy_feeincome.room_id
                                                ) + '(' + ( SELECT  owner_name
                                                            FROM    wy_owner
                                                            WHERE   owner_id = wy_feeincome.owner_id
                                                          ) + ')' ) END
                                AS owner_name ,
                                ( SELECT    feeitem_name
                                  FROM      wy_feeitem
                                  WHERE     feeitem_id = wy_feeincome.feeitem_id
                                ) AS feeitem ,
                                income_id ,
                                fee_income ,
                                rentcontract_id ,
                                pay_enddate ,
                                fee_date ,
                                owner_id ,
                                ( CAST(fee_year AS VARCHAR(4)) + '-' + CAST(fee_month AS VARCHAR(2)) ) AS incomedate
                        FROM    wy_feeincome WITH(NOLOCK)
                        WHERE   wy_feeincome.property_id = '" + property_id + "'");

                if (!string.IsNullOrEmpty(building_id))
                {
                    sqls.Append(" and building_id='" + building_id + "'");
                }
                if (!string.IsNullOrEmpty(owner_id))
                {
                    sqls.Append(" and  owner_id='" + owner_id + "'");
                }
                if (!string.IsNullOrEmpty(feeitem_id))
                {
                    sqls.Append(" and feeitem_id='" + feeitem_id + "'");
                }
                if (!string.IsNullOrEmpty(rentcontract_id))
                {
                    sqls.Append(" and rentcontract_id = '" + rentcontract_id + "'");
                }

                string strwhere = sqls.ToString();

                strwhere = checkdate(strwhere, stateY, endY);

                if (pagination != null && !string.IsNullOrEmpty(pagination.sidx))
                {
                    strwhere = string.Format("SELECT * FROM ({0}) t ORDER BY {1} {2}", strwhere, pagination.sidx, pagination.sord);
                }
                else
                {
                    strwhere += " order by fee_date,income_id desc";
                }

                return new RepositoryFactory().BaseRepository().FindTable(strwhere);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 实收查询不分页
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetReceiveList(Pagination pagination, string property_id, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                string building_id = queryParam["building_id"] == null ? string.Empty : queryParam["building_id"].ToString();
                string owner_id = queryParam["owner_id"] == null ? string.Empty : queryParam["owner_id"].ToString();
                string feeitem_id = queryParam["feeitem_id"] == null ? string.Empty : queryParam["feeitem_id"].ToString();
                string state = queryParam["state"] == null ? string.Empty : queryParam["state"].ToString();
                string end = queryParam["end"] == null ? string.Empty : queryParam["end"].ToString();
                string stateY = queryParam["stateY"] == null ? string.Empty : queryParam["stateY"].ToString();
                string endY = queryParam["endY"] == null ? string.Empty : queryParam["endY"].ToString();
                string rentcontract_id = queryParam["rentcontract_id"] == null ? string.Empty : queryParam["rentcontract_id"].ToString();

                StringBuilder sqls = new StringBuilder()
               .AppendFormat(@"SELECT  owner_name = CASE WHEN rentcontract_id IS NULL OR rentcontract_id = ''
                                                            THEN ( SELECT TOP 1
                                                                        building_name
                                                                    FROM   wy_building
                                                                    WHERE  building_id = wy_feeincome.building_id
                                                                ) + '/'
                                                                + ( SELECT TOP 1
                                                                            room_name
                                                                    FROM     wy_room
                                                                    WHERE    room_id = wy_feeincome.room_id
                                                                    ) + '('
                                                                + ( SELECT   owner_name
                                                                    FROM     wy_owner
                                                                    WHERE    owner_id = wy_feeincome.owner_id
                                                                    ) + ')'
                                                            ELSE ( SELECT TOP 1
                                                                        customername
                                                                    FROM   wy_rentcontract
                                                                    WHERE  contractid = wy_feeincome.rentcontract_id
                                                                )
                                                        END ,
                                        ( SELECT    feeitem_name
                                            FROM      wy_feeitem
                                            WHERE     feeitem_id = wy_feeincome.feeitem_id
                                        ) AS feeitem ,
                                        income_id ,
                                        fee_income ,
                                        rentcontract_id ,
                                        pay_enddate ,
                                        fee_date ,
                                        owner_id ,
                                        ( CAST(fee_year AS VARCHAR(4)) + '-' + CAST(fee_month AS VARCHAR(2)) ) AS incomedate
                                FROM    wy_feeincome WITH ( NOLOCK )
                                WHERE   wy_feeincome.property_id = '{0}'
                                        AND fee_date IS NOT NULL", property_id);

                if (!string.IsNullOrEmpty(building_id))
                {
                    sqls.AppendFormat(" and building_id='{0}'", building_id);
                }
                if (!string.IsNullOrEmpty(owner_id))
                {
                    sqls.AppendFormat(" and  owner_id='{0}'", owner_id);
                }
                if (!string.IsNullOrEmpty(feeitem_id))
                {
                    sqls.AppendFormat(" and feeitem_id='{0}'", feeitem_id);
                }
                if (!string.IsNullOrEmpty(rentcontract_id))
                {
                    sqls.AppendFormat(" and rentcontract_id = '{0}'", rentcontract_id);
                }

                string strwhere = sqls.ToString();

                strwhere = receive_checkdate(strwhere, state, end);
                strwhere = checkFeeyear(strwhere, stateY, endY);

                if (pagination != null && !string.IsNullOrEmpty(pagination.sidx))
                {
                    strwhere = string.Format("SELECT * FROM ({0}) t ORDER BY {1} {2}", strwhere, pagination.sidx, pagination.sord);
                }
                else
                {
                    strwhere += " order by fee_date,income_id desc";
                }

                return new RepositoryFactory().BaseRepository().FindTable(strwhere);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string receive_checkdate(string strwhere, string state, string end)
        {
            string str = strwhere;
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(end))
            {
                str = str + " and fee_date between '" + state + "' and '" + end + "'";
                return str;
            }
            else if (!string.IsNullOrEmpty(state))
            {
                str = str + " and fee_date >= '" + state + "'";
                return str;
            }
            else if (!string.IsNullOrEmpty(end))
            {
                str = str + " and fee_date<= '" + end + "'";
                return str;
            }
            return str;
        }

        private string checkFeeyear(string strwhere, string stateY, string endY)
        {
            string str = strwhere;
            if (!string.IsNullOrEmpty(stateY) && !string.IsNullOrEmpty(endY))
            {
                str = str + " and (cast(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))+'-1' as datetime)) between '" + stateY + "' and '" + endY + "'";
                return str;
            }
            else if (!string.IsNullOrEmpty(stateY))
            {
                str = str + " and (cast(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))+'-1' as datetime)) >= '" + stateY + "'";
                return str;
            }
            else if (!string.IsNullOrEmpty(endY))
            {
                str = str + " and (cast(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))+'-1' as datetime)) <= '" + endY + "'";
                return str;
            }
            return str;
        }

        /// <summary>
        /// 日期格式
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="stateY"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        private string checkdate(string strwhere, string stateY, string endY)
        {
            string str = strwhere;
            if (!string.IsNullOrEmpty(stateY) && !string.IsNullOrEmpty(endY))
            {
                str = str + " and (cast(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))+'-1' as datetime)) between '" + stateY + "' and '" + endY + "'";
                return str;
            }
            else if (!string.IsNullOrEmpty(stateY))
            {
                str = str + " and (cast(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))+'-1' as datetime)) >= '" + stateY + "'";
                return str;
            }
            else if (!string.IsNullOrEmpty(endY))
            {
                str = str + " and (cast(cast(fee_year as varchar(4)) + '-' + cast(fee_month as varchar(2))+'-1' as datetime)) <= '" + endY + "'";
                return str;
            }

            return str;
        }

        /// <summary>
        /// 是否欠费
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="owner_id"></param>
        /// <param name="Type">1业主 2合同</param>
        /// <returns></returns>
        public int IsQianFei(string room_id, string owner_id, int Type)
        {
            var strSql = new StringBuilder();
            if (Type == 1)
            {
                strSql.Append(@"select COUNT(income_id) from wy_feeincome where room_id=@room_id and owner_id=@owner_id and fee_income-fee_already>0 ");
            }
            else
            {
                strSql.Append(@"select COUNT(income_id) from wy_feeincome where property_id=@room_id and rentcontract_id=@owner_id and fee_income-fee_already>0 ");
            }
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@room_id",room_id),
                    DbParameters.CreateDbParameter("@owner_id",owner_id)
                };

            object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
            if (obj != null)
            {
                return obj.ToInt();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取小台帐
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<AccountTZEntity> GetAccountList(string property_id, string queryJson)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  wf.* ,
                                    wb.building_name ,
                                    wr.room_name ,
                                    wo.owner_name
                            FROM    wy_feeincome wf
                                    LEFT JOIN wy_building wb ON wb.building_id = wf.building_id
                                    LEFT JOIN wy_room wr ON wr.room_id = wf.room_id
                                    LEFT JOIN wy_owner wo ON wo.owner_id = wf.owner_id
                            WHERE   wf.property_id = @property_id ");

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));

            var queryParam = queryJson.ToJObject();
            //年份
            if (!queryParam["year"].IsEmpty())
            {
                int year = queryParam["year"].ToInt();
                strSql.AppendLine("  AND wf.fee_year=@fee_year ");
                parameter.Add(DbParameters.CreateDbParameter("@fee_year", year));
            }
            //查询楼栋
            if (!queryParam["bid"].IsEmpty())
            {
                string building_id = queryParam["bid"].ToString();
                strSql.AppendLine(" AND wf.building_id=@building_id ");
                parameter.Add(DbParameters.CreateDbParameter("@building_id", building_id));
            }
            //楼栋单元
            if (!queryParam["rid"].IsEmpty())
            {
                string room_id = queryParam["rid"].ToString();
                strSql.AppendLine(" AND wf.room_id=@room_id ");
                parameter.Add(DbParameters.CreateDbParameter("@room_id", room_id));
            }
            //费用科目
            if (!queryParam["fid"].IsEmpty())
            {
                string feeitem_id = queryParam["fid"].ToString();
                strSql.AppendLine(" AND wf.feeitem_id=@feeitem_id ");
                parameter.Add(DbParameters.CreateDbParameter("@feeitem_id", feeitem_id));
            }
            strSql.AppendLine("ORDER BY wr.room_id ASC");

            RepositoryFactory<AccountTZEntity> repository = new RepositoryFactory<AccountTZEntity>();
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="list">日志实体集合</param>
        public void RemoveForm(string keyValue, List<FeechangelogEntity> list)
        {
            FeechangelogService dal = new FeechangelogService();

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            if (keyValue.IndexOf(',') == -1)
            {
                #region 单个删除

                try
                {
                    //主表
                    db.Delete<FeeincomeEntity>(keyValue);
                    foreach (FeechangelogEntity item in list)
                    {
                        db.Insert(item);
                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw;
                }

                #endregion
            }
            else
            {
                #region 批量删除

                try
                {
                    string[] keyValues = keyValue.Split(',');
                    foreach (string item in keyValues)
                    {
                        //主表
                        db.Delete<FeeincomeEntity>(item);
                    }
                    foreach (FeechangelogEntity item in list)
                    {
                        db.Insert(item);
                    }

                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }

                #endregion
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FeeincomeEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //entity.Create();
                entity.income_id = entity.property_id + GetMaxID(8);
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 调整
        /// </summary>
        /// <param name="list">主键值</param>
        /// <param name="list_f">日志实体对象</param>
        /// <param name="list_c">费用减免记录实体对象</param>
        /// <returns></returns>
        public void SavesForm(List<FeeincomeEntity> list, List<FeechangelogEntity> list_f, List<FeeincomeCutEntity> list_c)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                foreach (FeeincomeEntity item in list)
                {
                    db.Update(item);
                }
                foreach (FeechangelogEntity item in list_f)
                {
                    db.Insert(item);
                }
                foreach (FeeincomeCutEntity item in list_c)
                {
                    db.Insert(item);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 生成费用
        /// </summary>
        /// <param name="roomid">单元编号</param>
        /// <param name="enddate">时间</param>
        /// <param name="type">1业主 2租户</param>
        public void Generate(string roomid, DateTime enddate, int type)
        {
            if (type == 1)
            {
                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@roomid",roomid),
                    DbParameters.CreateDbParameter("@enddate",enddate)
                };
                this.BaseRepository().ExecuteByProc("CreateRoomIncome", parameter);
            }
            else if (type == 2)
            {
                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@contractid",roomid),
                    DbParameters.CreateDbParameter("@enddate",enddate)
                };
                this.BaseRepository().ExecuteByProc("CreateContractIncome", parameter);
            }
        }

        private static readonly object locker = new object();

        /// <summary>
        /// 缴费确定
        /// </summary>
        /// <param name="list">费用应收实体对象</param>
        /// <param name="ent_ft">发票领用实体对象</param>
        /// <param name="list_fe">费用实收实体对象</param>
        /// <param name="list_fK">收费核销实体对象</param>
        public void FixedCost(List<FeeincomeEntity> list, FeeticketEntity ent_ft, List<FeereceiveEntity> list_fe, List<FeecheckEntity> list_fK)
        {
            lock (locker)
            {
                FeeticketEntity ticket = new RepositoryFactory().BaseRepository().FindEntity<FeeticketEntity>(w => w.ticket_id == ent_ft.ticket_id);
                if (ticket != null && ticket.ticket_status == 0)
                {
                    IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

                    try
                    {
                        foreach (FeeincomeEntity item in list)
                        {
                            db.Update(item);
                        }

                        db.Update(ent_ft);

                        foreach (FeereceiveEntity item in list_fe)
                        {
                            db.Insert(item);
                        }

                        foreach (FeecheckEntity item in list_fK)
                        {
                            db.Insert(item);
                        }

                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 导入费用
        /// </summary>
        /// <param name="list"></param>
        public void FeeManage(List<FeeincomeEntity> list)
        {
            if (list == null || list.Count == 0) return;

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();

            try
            {
                List<FeeincomeEntity> insert = new List<FeeincomeEntity>();
                List<FeeincomeEntity> update = new List<FeeincomeEntity>();

                // 获取集合
                string sql = "SELECT * FROM dbo.wy_feeincome WHERE fee_year = @fee_year AND fee_month = @fee_month AND property_id = @property_id";

                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@fee_year",list[0].fee_year),
                    DbParameters.CreateDbParameter("@fee_month",list[0].fee_month),
                    DbParameters.CreateDbParameter("@property_id",list[0].property_id)
                };

                var all = this.BaseRepository().FindList(sql, parameter);
                if (all != null)
                {
                    foreach (FeeincomeEntity item in list)
                    {
                        FeeincomeEntity c = new FeeincomeEntity();
                        if (!string.IsNullOrEmpty(item.rentcontract_id))
                        {
                            c = all.Where(w => w.feeitem_id == item.feeitem_id && w.rentcontract_id == item.rentcontract_id).FirstOrDefault();
                        }
                        else
                        {
                            c = all.Where(w => w.feeitem_id == item.feeitem_id && w.owner_id == item.owner_id && w.room_id == item.room_id).FirstOrDefault();
                        }
                        
                        if (c != null)
                        {
                            item.income_id = c.income_id;
                            update.Add(item);
                        }
                        else
                        {
                            insert.Add(item);
                        }
                    }
                }

                // 循环判断是否存在
                int MaxID = GetMaxID(0).ToInt();
                foreach (FeeincomeEntity item in insert)
                {
                    item.income_id = item.property_id + MaxID.ToString();
                    db.Insert(item);

                    MaxID++;
                }

                foreach (var item in update)
                {
                    db.Update(item);
                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        #endregion
    }
}