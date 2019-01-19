using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.FinanceManage.ViewModel;
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
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:08
    /// 描 述：Feeticket
    /// </summary>
    public class FeeticketService : RepositoryFactory, FeeticketIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeticketModel> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(@" select *,(select top 1 deptname from hr_department where deptid=wy_feeticket.dept_id) as deptname from wy_feeticket where ticket_id is not null   ");
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ticket_code":            //票号
                        strSql.Append(" and ticket_code like '%" + keyword + "%' ");
                        break;

                    case "signname":          //领用人
                        strSql.Append(" and signname like '%" + keyword + "%' ");
                        break;

                    default:
                        break;
                }
            }
            if (!queryParam["type_id"].IsEmpty())
            {
                string type_id = queryParam["type_id"].ToString();
                strSql.Append(" and  ticket_type='" + type_id + "' ");
            }
            if (!queryParam["state_id"].IsEmpty())
            {
                string state_id = queryParam["state_id"].ToString();
                strSql.Append(" and  ticket_status='" + state_id + "' ");
            }

            if (!queryParam["dept_id"].IsEmpty())
            {
                string dept_id = queryParam["dept_id"].ToString();
                strSql.Append(" and  dept_id='" + dept_id + "' ");
            }
            //else
            //{
            //    string dept_id = OperatorProvider.Provider.Current().DepartmentId;
            //    if (!string.IsNullOrEmpty(dept_id))
            //    {
            //        strSql.Append(" and  dept_id='" + dept_id + "' ");
            //    }
            //}

            if (!queryParam["StartDate"].IsEmpty() && !queryParam["EndDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" AND CONVERT(VARCHAR(10),signdate,120) BETWEEN '" + StartDate + "' AND '" + EndDate + "' ");
            }
            //最后更新时间
            if (!queryParam["SDate"].IsEmpty() && !queryParam["EDate"].IsEmpty())
            {
                string StartDate = queryParam["SDate"].ToString();
                string EndDate = queryParam["EDate"].ToString();
                strSql.Append(" AND CONVERT(VARCHAR(10),lasttime,120) BETWEEN '" + StartDate + "' AND '" + EndDate + "' ");
            }
            return this.BaseRepository().FindList<FeeticketModel>(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeticketEntity> GetList(string queryJson)
        {
            return null;
        }

        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <param name="keyVaule"></param>
        /// <returns></returns>
        public CostInfoEntity GetCostByTicketId(string keyVaule)
        {
            FeereceiveEntity income = new FeereceiveService().GetEntityByTicket(keyVaule);
            bool is_income = false;
            OtherincomeEntity otherIncome = new OtherincomeService().GetEntityByTicket(keyVaule);
            bool is_otherIncome = false;

            StringBuilder builder = new StringBuilder();
            if (income != null)
            {
                is_income = true;

                #region 应收
                builder.Append(@"SELECT  receive_id ,
                                    property_id ,
                                    ( SELECT    owner_name
                                      FROM      wy_owner
                                      WHERE     owner_id = wy_feereceive.owner_id
                                    ) AS ownername ,
                                    ( SELECT TOP 1
                                                customername
                                      FROM      wy_rentcontract
                                      WHERE     contractid = wy_feereceive.rentcontract_id
                                    ) AS customername ,
                                    ( ( SELECT TOP 1
                                                building_name
                                        FROM    wy_building
                                        WHERE   building_id = ( SELECT TOP 1
                                                                        building_id
                                                                FROM    wy_room
                                                                WHERE   room_id = wy_feereceive.room_id
                                                              )
                                      ) + '/' + ( SELECT TOP 1
                                                            room_name
                                                  FROM      wy_room
                                                  WHERE     room_id = wy_feereceive.room_id
                                                ) ) AS propertyname ,
                                    receive_date ,
                                    fee_money ,
                                    ( SELECT TOP 1
                                                ticket_code
                                      FROM      wy_feeticket
                                      WHERE     ticket_id = wy_feereceive.ticket_id
                                    ) AS ticket_code ,
                                    pay_mode
                            FROM    wy_feereceive
                            WHERE   ticket_id = @ticketid");
                #endregion
            }
            else if (otherIncome != null)
            {
                is_otherIncome = true;

                #region 其他收入
                builder.Append(@"SELECT  customer AS ownername ,
                                        feedate AS receive_date ,
                                        property_id ,
                                        feemoney AS fee_money ,
                                        currency AS pay_mode ,
                                        '' AS propertyname ,
                                        incomeid ,
                                        ( SELECT TOP 1
                                                    ticket_code
                                          FROM      wy_feeticket
                                          WHERE     ticket_id = wy_otherincome.ticketid
                                        ) AS ticket_code
                                FROM    wy_otherincome
                                WHERE   ticketid = @ticketid ");
                #endregion
            }
            else
            {
                throw new Exception("发票编号不存在");
            }

            DbParameter[] parameter ={
                DbParameters.CreateDbParameter("@ticketid",keyVaule)
            };

            DataTable dtCost = new RepositoryFactory().BaseRepository().FindTable(builder.ToString(), parameter);
            if (dtCost != null && dtCost.Rows.Count > 0)
            {
                CostInfoEntity model = DataHelper.CreateItem<CostInfoEntity>(dtCost.Rows[0]);
                model.is_income = is_income;
                model.is_otherIncome = is_otherIncome;

                PropertyEntity proinfo = new PropertyService().GetEntity(model.property_id);
                if (proinfo != null)
                {
                    model.property_name = proinfo.property_name;
                }

                return model;
            }
            return null;
        }

        /// <summary>
        /// 获取其他收入明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<OtherIncomeEntity> GetOherIncomeList(string keyValue, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string is_otherIncome = queryParam["is_otherIncome"].ToString();
            string is_income = queryParam["is_income"].ToString();
            string is_group = queryParam["is_group"].ToString();

            if (is_otherIncome == "true")
            {
                StringBuilder strsql = new StringBuilder();

                if (is_group == "1")
                {
                    strsql.AppendFormat(@"  SELECT  SUM(temp.check_money) check_money ,
                                                    MIN(temp.subject) ,
                                                    MIN(temp.pay_enddate) ,
                                                    temp.fn
                                            FROM    ( SELECT    fee_income AS check_money ,
                                                                subject ,
                                                                ( SELECT TOP 1
                                                                            feedate
                                                                  FROM      wy_otherincome
                                                                  WHERE     wy_otherincome.incomeid = wy_otherincomeitem.incomeid
                                                                ) AS pay_enddate ,
                                                                ( SELECT TOP 1
                                                                            feeitem_name
                                                                  FROM      wy_feeitem
                                                                  WHERE     feeitem_id = wy_otherincomeitem.feeitem_id
                                                                ) AS fn
                                                      FROM      wy_otherincomeitem
                                                      WHERE     incomeid = '{0}'
                                                    ) temp
                                            GROUP BY temp.fn", keyValue);
                }
                else
                {
                    strsql.Append("select fee_income as check_money,subject,(select top 1 feedate from wy_otherincome where wy_otherincome.incomeid=wy_otherincomeitem.incomeid) as pay_enddate,(select top 1 feeitem_name from wy_feeitem where feeitem_id=wy_otherincomeitem.feeitem_id) as fn from wy_otherincomeitem where incomeid='" + keyValue + "'");
                }

                return this.BaseRepository().FindList<OtherIncomeEntity>(strsql.ToString());
            }
            else if (is_income == "true")
            {
                StringBuilder strsql = new StringBuilder();
                if (is_group == "1")
                {
                    strsql.AppendFormat(@"  SELECT  SUM(temp.check_money) check_money ,
                                                    fn
                                            FROM    ( SELECT    wy_feecheck.* ,
                                                                wy_feeincome.pay_enddate ,
                                                                ( CAST(wy_feeincome.fee_year AS VARCHAR) + '/'
                                                                  + CAST(wy_feeincome.fee_month AS VARCHAR) ) AS subject ,
                                                                ( SELECT TOP 1
                                                                            feeitem_name
                                                                  FROM      wy_feeitem
                                                                  WHERE     feeitem_id = wy_feeincome.feeitem_id
                                                                ) AS fn
                                                      FROM      wy_feecheck
                                                                LEFT JOIN wy_feeincome ON wy_feecheck.income_id = wy_feeincome.income_id
                                                      WHERE     receive_id = '{0}'
                                                    ) temp
                                            GROUP BY temp.fn", queryParam["receive_id"].ToString());
                }
                else
                {
                    strsql.Append("select wy_feecheck.*,wy_feeincome.pay_enddate,(cast(wy_feeincome.fee_year as varchar) + '/' + cast(wy_feeincome.fee_month as varchar)) as subject,");
                    strsql.Append(" (select top 1 feeitem_name from wy_feeitem where feeitem_id=wy_feeincome.feeitem_id) as fn from wy_feecheck left join ");
                    strsql.Append(" wy_feeincome on wy_feecheck.income_id=wy_feeincome.income_id where receive_id='" + queryParam["receive_id"].ToString() + "'");
                }

                return this.BaseRepository().FindList<OtherIncomeEntity>(strsql.ToString());
            }

            return null;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeticketEntity GetEntity()
        {
            //var strSql = new StringBuilder();
            //strSql.Append(@" SELECT MAX(ticket_code)+1  AS ticket_code FROM dbo.wy_feeticket  ");
            //FeeticketEntity model = this.BaseRepository().FindList<FeeticketEntity>(strSql.ToString()).FirstOrDefault();
            //return model;
            return null;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeticketEntity GetEntity(string keyValue)
        {
            RepositoryFactory<FeeticketEntity> repository = new RepositoryFactory<FeeticketEntity>();
            return repository.BaseRepository().FindEntity(keyValue);
        }

        public IEnumerable<FeeticketEntity> GetListByIds(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var expression = LinqExtensions.True<FeeticketEntity>();
                if (keyValue.IndexOf(',') == -1)
                {
                    expression = expression.And(o => o.ticket_id == keyValue);
                    return this.BaseRepository().IQueryable(expression).ToList();
                }
                else
                {
                    string[] keyValues = keyValue.Split(',');
                    string str = "";
                    foreach (string item in keyValues)
                    {
                        str += "," + item;
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str + ",";
                    }
                    expression = expression.And(o => str.Contains("," + o.ticket_id + ","));
                    return this.BaseRepository().IQueryable(expression).ToList();
                }
            }
            return null;
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="dept_id">领用部门ID</param>
        /// <param name="ticket_type">发票类别</param>
        /// <param name="ticket_status">发票状态</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeticketEntity> GetSelList(string dept_id, string ticket_type, int ticket_status)
        {
            var expression = LinqExtensions.True<FeeticketEntity>();
            //查询条件
            //领用部门ID
            if (!dept_id.IsEmpty())
            {
                expression = expression.And(t => t.dept_id == dept_id);
            }
            //发票类别
            if (!ticket_type.IsEmpty())
            {
                expression = expression.And(t => t.ticket_type == ticket_type);
            }

            expression = expression.And(t => t.ticket_status == ticket_status);

            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.ticket_code).ToList();
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 修改发票状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：100是作废发票，2是已归档，1是已使用</param>
        public void UpdateState(string keyValue, Int16 State)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (keyValue.IndexOf(',') == -1)
                {
                    FeeticketEntity feeticketEntity = new FeeticketEntity();
                    feeticketEntity.Modify(keyValue);
                    feeticketEntity.ticket_status = State;
                    feeticketEntity.lasttime = DateTime.Now;
                    feeticketEntity.lastoperate = OperatorProvider.Provider.Current().UserName;
                    this.BaseRepository().Update(feeticketEntity);
                }
                else
                {
                    string[] keyValues = keyValue.Split(',');
                    string str = "";
                    foreach (string item in keyValues)
                    {
                        str += "'" + item + "',";
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        str = str.Substring(0, str.Length - 1);
                    }
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("UPDATE wy_feeticket SET ticket_status=@State,lasttime=GETDATE(),lastoperate=@lastoperate WHERE ticket_id IN (" + str + ")");

                    DbParameter[] parameter ={
                    DbParameters.CreateDbParameter("@State",State),
                    DbParameters.CreateDbParameter("@lastoperate",OperatorProvider.Provider.Current().UserName)
                };

                    this.BaseRepository().ExecuteBySql(strSql.ToString(), parameter);
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            RepositoryFactory<FeeticketEntity> repository = new RepositoryFactory<FeeticketEntity>();
            repository.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FeeticketEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                this.BaseRepository().Insert(entity);
            }
        }

        #endregion 提交数据

        /// <summary>
        /// 查询打印数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<TicketPrintEntity> GetPrintListJson(string keyValue, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT  a.feeitem_id ,
                                ( RTRIM(a.fee_year) + '/' + RTRIM(a.fee_month) ) AS fperiod ,
                                c.check_money
                        INTO    #tmp
                        FROM    wy_feecheck c
                                INNER JOIN wy_feereceive r ON r.receive_id = c.receive_id
                                INNER JOIN wy_feeincome a ON a.income_id = c.income_id
                        WHERE   r.ticket_id = @ticketid

                        SELECT cc.*,ROUND(cc.fmoney*cc.sl/(1+cc.sl),2) AS se FROM ( SELECT  bb.* ,
                                ff.feedispname ,
                                ff.taxrate ,
                                left(ff.taxtype,19) as taxtype,
								CONVERT(FLOAT,REPLACE(ff.taxrate,'%',''))/100 AS sl
                        FROM    ( SELECT    feeitem_id ,
                                            SUM(check_money) AS fmoney ,
                                            STUFF(( SELECT  ',' + fperiod
                                                    FROM    #tmp
                                                    WHERE   feeitem_id = #tmp.feeitem_id
                                                    FOR
                                                    XML PATH('')
                                                    ), 1, 1, '') AS speriod
                                    FROM      #tmp
                                    GROUP BY  feeitem_id
                                ) bb
                                LEFT JOIN wy_feeitem ff ON ff.feeitem_id = bb.feeitem_id ) cc");
            DbParameter[] parameter = { DbParameters.CreateDbParameter("@ticketid",keyValue) };

            IRepository<TicketPrintEntity> resitory = new RepositoryFactory<TicketPrintEntity>().BaseRepository();
            return resitory.FindList(sql.ToString(), parameter);
        }
    }
}