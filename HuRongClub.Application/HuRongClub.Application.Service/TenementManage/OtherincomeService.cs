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
    /// 日 期：2017-04-06 09:43
    /// 描 述：Otherincome
    /// </summary>
    public class OtherincomeService : RepositoryFactory<OtherincomeEntity>, OtherincomeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OtherincomeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OtherincomeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OtherincomeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OtherincomeEntity GetEntityByTicket(string keyValue)
        {
            return this.BaseRepository().FindEntity(w => w.ticketid == keyValue);
        }

        /// <summary>
        /// 获取 缴费查询 列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<OtherincomeListEntity> GetList(Pagination pagination, string property_id, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            #region 老系统逻辑 Update:2017/9/28 Jerry.Li

            string fullowner = "(select top 1 building_name from wy_building where building_id=(select top 1 building_id from wy_room where room_id=wy_feereceive.room_id)) + '/' + (select top 1 room_name from wy_room where room_id=wy_feereceive.room_id) + '(' +(select owner_name from wy_owner where owner_id=wy_feereceive.owner_id) + ')' ";
            string rentcustomer = "(select top 1 customername from wy_rentcontract where contractid=wy_feereceive.rentcontract_id)";
            string sql_rec = "select receive_id AS id,'0' as feetype,'customer'=case when rentcontract_id is null or rentcontract_id='' then " + fullowner + " else " + rentcustomer + " end, receive_date,userid,fee_money,ticket_id,(select ticket_code from wy_feeticket where ticket_id=wy_feereceive.ticket_id) as ticket_code,'' notes from wy_feereceive where property_id='" + property_id + "'";

            string sql_other = "select incomeid AS id,'1' as feetype,customer,feedate as receive_date,operateuser as userid,feemoney as feemoney,ticketid as ticket_id,(select ticket_code from wy_feeticket where ticket_id=a.ticketid) as ticket_code,(SELECT [subject]+',' FROM wy_otherincomeitem WHERE incomeid = a.incomeid AND [subject] != ''  FOR XML PATH('')) AS notes from wy_otherincome a where property_id='" + property_id + "'";

            if (!queryParam["feeitem_id"].IsEmpty())
            {
                string feeitem_id = queryParam["feeitem_id"].ToString();
                sql_rec = sql_rec + " and wy_feereceive.receive_id in (select distinct receive_id from wy_feecheck inner join wy_feeincome on wy_feecheck.income_id=wy_feeincome.income_id where wy_feeincome.feeitem_id='" + feeitem_id + "')";
                sql_other = sql_other + " and incomeid in(select distinct incomeid from wy_otherincomeitem where feeitem_id='" + feeitem_id + "')";
            }

            if (!queryParam["userid"].IsEmpty())
            {
                string userid = queryParam["userid"].ToString();
                sql_rec = sql_rec + " and wy_feereceive.userid='" + userid + "'";
                sql_other = sql_other + " and operateuser='" + userid + "'";
            }

            if (!queryParam["ticket_id"].IsEmpty())
            {
                string ticket_id = queryParam["ticket_id"].ToString();
                if (ticket_id != "0")
                {
                    sql_rec = sql_rec + " and  ticket_id='" + ticket_id + "'";
                    sql_other = sql_other + " and  ticketid='" + ticket_id + "'";
                }
            }

            if (!queryParam["state"].IsEmpty())
            {
                string state = queryParam["state"].ToString();
                sql_rec = sql_rec + " and receive_date>='" + state + "'";
                sql_other = sql_other + " and feedate>='" + state + "'";
            }

            if (!queryParam["end"].IsEmpty())
            {
                string end = queryParam["end"].ToString();
                sql_rec = sql_rec + " and receive_date<='" + end + "'";
                sql_other = sql_other + " and feedate<='" + end + "'";
            }

            if (!queryParam["contractid"].IsEmpty())
            {
                string contractid = queryParam["contractid"].ToString();
                sql_rec = sql_rec + " and rentcontract_id='" + contractid + "'";
            }

            if (!queryParam["owner_id"].IsEmpty())
            {
                string owner_id = queryParam["owner_id"].ToString();
                sql_rec = sql_rec + " and owner_id='" + owner_id + "'";
            }

            string sqls = "";
            if (queryParam["owner_id"].IsEmpty() && queryParam["contractid"].IsEmpty())
            {
                sqls = sql_rec + " union " + sql_other;
                sqls = string.Format("SELECT * FROM ({0}) t ORDER BY {1} {2}", sqls, pagination.sidx, pagination.sord);
            }
            else
            {
                sqls = string.Format("{0} ORDER BY {1} {2}", sql_rec, pagination.sidx, pagination.sord);
            }

            #endregion

            RepositoryFactory<OtherincomeListEntity> repository = new RepositoryFactory<OtherincomeListEntity>();

            return repository.BaseRepository().FindList(sqls);
        }

        private string checkdate(string strwhere, string sdate,string enddate)
        {
            string str = strwhere;
            if (sdate != "" && enddate != "")
            {
                str = str + " and wy_feereceive.receive_date between '" + sdate + "' and '" + enddate + "'";
                return str;
            }
            else if (sdate != "")
            {
                str = str + " and wy_feereceive.receive_date >= '" + sdate + "'";
                return str;
            }
            else if (enddate != "")
            {
                str = str + " and wy_feereceive.receive_date <= '" + enddate + "'";
                return str;
            }
            return str;
        }

        private string checkotherdate(string strwhere, string sdate, string enddate)
        {
            string str = strwhere;
            if (sdate != "" && enddate != "")
            {
                str = str + " and feedate between '" + sdate + "' and '" + enddate + "'";
                return str;
            }
            else if (sdate != "")
            {
                str = str + " and feedate >= '" + sdate + "'";
                return str;
            }
            else if (enddate != "")
            {
                str = str + " and feedate <= '" + enddate + "'";
                return str;
            }
            return str;
        }

        /// <summary>
        /// 获取业主缴费查询明细实体
        /// </summary>
        /// <param name="receive_id">实收编号</param>
        /// <param name="type">0 业主 1租户</param>
        /// <returns></returns>
        public OtherincomeFromEntity.OtherFeereceiveFromEntity GetEntitys(string receive_id, int type)
        {
            var strSql = new StringBuilder();

            if (type == 1)
            {
                strSql.Append(@"SELECT wo.incomeid AS receive_id,wo.property_id,wo.customer AS ownername,'' AS customername,wp.property_name AS propertyname
                            ,CONVERT(VARCHAR(10),wo.feedate,120)receive_date,woc.fee_income AS fee_money,wf.ticket_code,wo.currency AS pay_mode
                            FROM wy_otherincome wo
                            LEFT JOIN wy_property wp ON wo.property_id=wp.property_id
                            LEFT join  wy_otherincomeitem woc on wo.incomeid=woc.incomeid
                            LEFT join wy_feeticket wf on wo.ticketid=wf.ticket_id
                            WHERE wo.incomeid=@receive_id ");
            }
            else
            {
                strSql.Append(@"select wf.receive_id,wf.property_id
                        ,ISNULL((select top 1 customername from wy_rentcontract where contractid=wf.rentcontract_id),(select owner_name from wy_owner where owner_id=wf.owner_id)) AS ownername
                        ,(wp.property_name+'/'+ISNULL((select top 1 building_name from wy_building where building_id =(select top 1 building_id from wy_room where room_id=wf.room_id)),'')
                        + '/' + ISNULL((select top 1 room_name from wy_room where room_id=wf.room_id),''))as propertyname
                        ,CONVERT(VARCHAR(10),wf.receive_date,120)receive_date,wf.fee_money
                        ,(select top 1 ticket_code from wy_feeticket where ticket_id=wf.ticket_id) as ticket_code, pay_mode
                        FROM wy_feereceive wf
                        LEFT JOIN wy_property wp ON wf.property_id=wp.property_id
                    WHERE receive_id=@receive_id ");
            }

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@receive_id",receive_id)
                };

            RepositoryFactory<OtherincomeFromEntity.OtherFeereceiveFromEntity> repository = new RepositoryFactory<OtherincomeFromEntity.OtherFeereceiveFromEntity>();

            DataTable data = repository.BaseRepository().FindTable(strSql.ToString(), parameter);
            if (data != null && data.Rows.Count > 0)
            {
                return DataHelper.CreateItem<OtherincomeFromEntity.OtherFeereceiveFromEntity>(data.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// 获取应收收入明细
        /// </summary>
        /// <param name="receive_id">实收编号</param>
        /// <param name="type">0 业主 1租户</param>
        /// <returns></returns>
        public IEnumerable<OtherincomeFromEntity.OtherFeereceiveListEntity> GetListByOther(string receive_id, int type)
        {
            var strSql = new StringBuilder();
            if (type == 1)
            {
                strSql.Append(@"SELECT  woc.fee_income AS check_money ,
                                        CONVERT(VARCHAR(10), wo.feedate, 120) pay_enddate ,
                                        woc.subject ,
                                        we.feeitem_name
                                FROM    wy_otherincome wo
                                        LEFT JOIN wy_property wp ON wo.property_id = wp.property_id
                                        LEFT JOIN wy_otherincomeitem woc ON wo.incomeid = woc.incomeid
                                        LEFT JOIN wy_feeticket wf ON wo.ticketid = wf.ticket_id
                                        LEFT JOIN wy_feeitem we ON woc.feeitem_id = we.feeitem_id
                                WHERE   wo.ticketid = @receive_id ");
            }
            else
            {
                strSql.Append(@"SELECT  wy_feecheck.* ,
                                        CONVERT(VARCHAR(10), wy_feeincome.pay_enddate, 120) pay_enddate ,
                                        ( CAST(wy_feeincome.fee_year AS VARCHAR) + '/' + CAST(wy_feeincome.fee_month AS VARCHAR) ) AS subject ,
                                        ( SELECT TOP 1
                                                    feeitem_name
                                          FROM      wy_feeitem
                                          WHERE     feeitem_id = wy_feeincome.feeitem_id
                                        ) AS feeitem_name
                                FROM    wy_feecheck
                                        LEFT JOIN wy_feeincome ON wy_feecheck.income_id = wy_feeincome.income_id
                                WHERE   receive_id IN ( SELECT  receive_id
                                                        FROM    wy_feereceive
                                                        WHERE   ticket_id = @receive_id ) ");
            }
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@receive_id",receive_id)
                };

            RepositoryFactory<OtherincomeFromEntity.OtherFeereceiveListEntity> repository = new RepositoryFactory<OtherincomeFromEntity.OtherFeereceiveListEntity>();

            return repository.BaseRepository().FindList(strSql.ToString(), parameter);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="ticket_id">发票编号</param>
        /// <returns></returns>
        public string Getincomeid(string ticket_id)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 incomeid from wy_otherincome where ticketid=@ticketid ");
            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@ticketid",ticket_id)
                };
            object obj = this.BaseRepository().FindObject(strSql.ToString(), parameter);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 获取其他收入列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">参数</param>
        /// <returns></returns>
        public IEnumerable<OtherincomeitemListEntity> GetOtherList(Pagination pagination, string property_id, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            var strSql = new StringBuilder();
            strSql.Append(@"select wo.incomeid,wo.customer,wo.feedate,wo.feemoney,ticketid,wo.operateuser,wo.inputtime,wf.ticket_code
                            FROM wy_otherincome wo
                            LEFT join wy_feeticket wf on wo.ticketid=wf.ticket_id
                            WHERE wo.property_id=@property_id  ");

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@property_id", property_id));

            if (!queryParam["customer"].IsEmpty())
            {
                string customer = queryParam["customer"].ToString();
                strSql.AppendFormat(" AND wo.customer like '%{0}%'   ", customer);
            }
            if (!queryParam["ticketid"].IsEmpty() && queryParam["ticketid"].ToString() != "0")
            {
                string ticketid = queryParam["ticketid"].ToString();
                strSql.Append(" AND wf.ticket_id=@ticketid  ");
                parameter.Add(DbParameters.CreateDbParameter("@ticketid", ticketid));
            }
            if (!queryParam["feeitem_id"].IsEmpty())
            {
                string feeitem_id = queryParam["feeitem_id"].ToString();
                strSql.Append(" AND wo.incomeid IN (SELECT wm.incomeid FROM dbo.wy_otherincomeitem wm WHERE wm.feeitem_id=@feeitem_id)  ");
                parameter.Add(DbParameters.CreateDbParameter("@feeitem_id", feeitem_id));
            }
            if (!queryParam["state"].IsEmpty() && !queryParam["end"].IsEmpty())
            {
                string state = queryParam["state"].ToString();
                string end = queryParam["end"].ToString();
                strSql.Append(" AND feedate>=@state and feedate<=@end    ");
                parameter.Add(DbParameters.CreateDbParameter("@state", state));
                parameter.Add(DbParameters.CreateDbParameter("@end", end));
            }

            RepositoryFactory<OtherincomeitemListEntity> repository = new RepositoryFactory<OtherincomeitemListEntity>();
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination).OrderByDescending(t => t.feedate);
        }

        /// <summary>
        /// 获取其他收入明细列表
        /// </summary>
        /// <param name="incomeid"></param>
        /// <returns></returns>
        public IEnumerable<OtherincomeitemEntity> GetOtherDetailList(string incomeid)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT wm.incomeitem_id,wm.incomeid,wf.feeitem_name AS feeitem_id,wm.subject,wm.fee_income
                            FROM wy_otherincomeitem wm
                            LEFT JOIN dbo.wy_feeitem wf ON wm.feeitem_id=wf.feeitem_id
                            WHERE wm.incomeid=@incomeid ");

            DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@incomeid",incomeid)
                };

            RepositoryFactory<OtherincomeitemEntity> repository = new RepositoryFactory<OtherincomeitemEntity>();

            return repository.BaseRepository().FindList(strSql.ToString(), parameter);
        }

        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <param name="TableName">表名</param>
        /// <param name="Field">字段</param>
        /// <param name="RightLongth">长度</param>
        /// <returns></returns>
        public string GetMaxID(int pos, string TableName, string Field, int RightLongth)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select max(REPLACE(RIGHT(" + Field + "," + RightLongth + "),'-','0'))+1 FROM  " + TableName);
            string str = "0";
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
        public void SaveForm(string keyValue, OtherincomeEntity entity)
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

        /// <summary>
        /// 发票作废
        /// </summary>
        /// <param name="lastoperate">操作人</param>
        /// <param name="ticket_id">编号</param>
        public void ToVoidForm(string lastoperate, string ticket_id)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("update wy_feeticket set ticket_status=100,lasttime=getdate(),lastoperate=@lastoperate where ticket_id=@ticket_id ");
                DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@lastoperate",lastoperate),
                    DbParameters.CreateDbParameter("@ticket_id",ticket_id)
                };

                db.ExecuteBySql(strSql.ToString(), parameter);

                string str = "delete from wy_otherincomeitem where incomeid in (select top 1 incomeid from wy_otherincome where ticketid=@ticket_id)";
                DbParameter[] parameter1 ={
                    DbParameters.CreateDbParameter("@ticket_id",ticket_id)
                };

                db.ExecuteBySql(str, parameter1);

                string str2 = "delete from  wy_otherincome where ticketid=@ticket_id";
                DbParameter[] parameter2 ={
                    DbParameters.CreateDbParameter("@ticket_id",ticket_id)
                };
                db.ExecuteBySql(str2, parameter2);

                db.Commit();
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">子实体对象</param>
        /// <returns></returns>
        public string SavesForm(string keyValue, OtherincomeEntity entity, List<OtherincomeitemEntity> entryList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<OtherincomeitemEntity>(t => t.incomeid.Equals(keyValue));
                    foreach (OtherincomeitemEntity item in entryList)
                    {
                        item.Create();
                        item.incomeid = entity.incomeid;
                        db.Insert(item);
                    }
                }
                else
                {
                    //主表
                    //entity.Create();
                    entity.incomeid = entity.property_id + GetMaxID(8, "wy_otherincome", "incomeid", 8);
                    db.Insert(entity);

                    //发票状态
                    FeeticketEntity ent_fee = new FeeticketEntity();
                    ent_fee.ticket_id = entity.ticketid;
                    ent_fee.ticket_status = 1;
                    db.Update(ent_fee);

                    int maxid = GetMaxID(0, "wy_otherincomeitem", "incomeitem_id", 8).ToInt();
                    //明细
                    foreach (OtherincomeitemEntity item in entryList)
                    {
                        item.incomeitem_id = entity.property_id + Utils.SupplementZero(maxid.ToString(), 8);
                        item.incomeid = entity.incomeid;
                        db.Insert(item);

                        maxid++;
                    }
                }
                db.Commit();

                return entity.incomeid;
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