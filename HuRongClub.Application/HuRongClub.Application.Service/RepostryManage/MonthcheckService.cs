using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.IService.RepostryManage;
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

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-27 14:25
    /// 描 述：月结账
    /// </summary>
    public class MonthcheckService : RepositoryFactory, MonthcheckIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MonthcheckModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<MonthcheckModel> repository = new RepositoryFactory<MonthcheckModel>();
            var strSql = new StringBuilder();
            strSql.Append(@"select a.*,b.ftypename from tb_wh_monthcheck a left join tb_wh_goodstype b on b.ftypecode=a.ftypecode where 1=1  ");
            return repository.BaseRepository().FindList(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<MonthcheckModel> GetList(string queryJson)
        {
            RepositoryFactory<MonthcheckModel> repository = new RepositoryFactory<MonthcheckModel>();
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT a.ftypecode,a.fyear,a.fmonth
                            ,CAST(round(a.fbeginmoney,2) as numeric(20,2))fbeginmoney
                            ,CAST(round(a.finmoney,2) as numeric(20,2))finmoney
                            ,CAST(round(a.foutmoney,2) as numeric(20,2))foutmoney
                            ,CAST(round(a.fendmoney,2) as numeric(20,2))fendmoney
                            ,a.fbegindate,a.fenddate,b.ftypename from tb_wh_monthcheck a 
                            LEFT join tb_wh_goodstype b on b.ftypecode=a.ftypecode where 1=1  ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();

            #region 年份和月份查询

            //查询条件 年份
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and a.fyear=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            //月份
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and a.fmonth=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }

            #endregion 年份和月份查询

            //数据量大, 如若未选择年月查询，则默认不显示数据
            if (queryParam.Count < 1)
            {
                return null;
            }
            else
            {
                return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MonthcheckEntity GetEntity(string keyValue)
        {
            return null;
        }
        /// <summary>
        /// 查询上个月是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistsLastMonth(int year, int mouth)
        {
            string str = "select * from tb_wh_monthcheck where fyear="+ year + " and fmonth="+ mouth;
            DataTable dt = this.BaseRepository().FindTable(str);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 月结账
        /// </summary>
        /// <param name="queryJson"></param>
        public void OKMonth(string queryJson)
        {
            try
            {
                //解析参数 月份和时间段
                var queryParam = queryJson.ToJObject();
                //查询条件 统计年月
                if (!queryParam["Statrmonths"].IsEmpty() && !queryParam["Eedmonths"].IsEmpty() && !queryParam["beginDate"].IsEmpty() && !queryParam["stopDate"].IsEmpty())
                {
                    string Statryear = queryParam["Statrmonths"].ToString();
                    string Eedmonths = queryParam["Eedmonths"].ToString();
                    string beginDate = queryParam["beginDate"].ToString();
                    string stopDate = queryParam["stopDate"].ToString();
                    string deletesql = " delete tb_wh_monthcheck where fyear='" + Statryear + "' and fmonth='" + Eedmonths + "' ";
                    int rows = BaseRepository().ExecuteBySql(deletesql);

                    //删除成功后, 查询出大类
                    string sql = " select ftypecode from tb_wh_goodstype where fparentcode='0'  ";
                    DataTable dt = BaseRepository().FindTable(sql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        //逐条
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ProcCheck(dt.Rows[i]["ftypecode"].ToString(), beginDate, stopDate, Statryear, Eedmonths);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 调取的存储过程
        /// </summary>
        /// <param name="typecode"></param>
        /// <param name="dtbegin"></param>
        /// <param name="dtend"></param>
        /// <param name="dtyear"></param>
        /// <param name="dtmont"></param>
        public void ProcCheck(string typecode, string dtbegin, string dtend, string dtyear, string dtmont)
        {
            //调用存储过程
            DbParameter[] parameter =
                                                    {
                                                        DbParameters.CreateDbParameter("@typecode",typecode),
                                                        DbParameters.CreateDbParameter("@dtbegin",dtbegin),
                                                        DbParameters.CreateDbParameter("@dtend",dtend),
                                                        DbParameters.CreateDbParameter("@dtyear",dtyear),
                                                        DbParameters.CreateDbParameter("@dtmont",dtmont)
                                                    };
            int IsOk = BaseRepository().ExecuteByProc("sp_goods_monthcheck", parameter);
        }

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
        public void SaveForm(string keyValue, MonthcheckEntity entity)
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
        /// 月销量库存
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<MonthGoodsModel> GetMonthDetailListJson(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            List<DbParameter> param = new List<DbParameter>();
            param.Add(DbParameters.CreateDbParameter("@fyear", queryParam["startDate"].ToString()));
            param.Add(DbParameters.CreateDbParameter("@fmonth", queryParam["endDate"].ToString()));

            RepositoryFactory<MonthGoodsModel> repository = new RepositoryFactory<MonthGoodsModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT TOP 50
                                    tb_wh_monthgoods.fgoodsid ,
                                    tb_wh_goodsinfo.funit ,
                                    tb_wh_goodsinfo.fname ,
                                    tb_wh_monthgoods.fcount ,
                                    tb_wh_monthgoods.fprice ,
                                    tb_wh_monthgoods.fmoney
                            FROM    tb_wh_monthgoods
                                    LEFT JOIN dbo.tb_wh_goodsinfo ON tb_wh_goodsinfo.fgoodsid = tb_wh_monthgoods.fgoodsid
                            WHERE   fyear = @fyear
                                    AND fmonth = @fmonth");

            return repository.BaseRepository().FindList(strSql.ToString(), param.ToArray(), pagination);
        }

        #endregion 提交数据
    }
}