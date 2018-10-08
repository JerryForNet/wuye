using HuRongClub.Application.Entity.TenementManage;
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
    /// 日 期：2017-04-06 15:13
    /// 描 述：FixReport
    /// </summary>
    public class FixReportService : RepositoryFactory<FixReportEntity>, FixReportIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FixReportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="propertyid">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FixReportEntity> GetPageList(Pagination pagination,string propertyid, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            var strSql = new StringBuilder();
            strSql.Append(@"select * FROM wy_FixReport where propertyid=@property_id ");

            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@property_id", propertyid));
            
            if (!queryParam["fixtype"].IsEmpty())
            {
                int fixtype = queryParam["fixtype"].ToInt();
                strSql.Append(" and fixtype="+ fixtype);
            }
            if (!queryParam["fixgroup"].IsEmpty())
            {
                string fixgroup = queryParam["fixgroup"].ToString();
                strSql.Append(" and fixgroup=@fixgroup ");
                parameter.Add(DbParameters.CreateDbParameter("@fixgroup", fixgroup));
            }
            if (!queryParam["fixNumber_No"].IsEmpty())
            {
                string fixNumber_No = queryParam["fixNumber_No"].ToString();
                strSql.Append(" and fixNumber_No=@fixNumber_No ");
                parameter.Add(DbParameters.CreateDbParameter("@fixNumber_No", fixNumber_No));
            }
            if (!queryParam["owner_id"].IsEmpty()&& !queryParam["customertype"].IsEmpty())
            {
                string owner_id = queryParam["owner_id"].ToString();
                int customertype = queryParam["customertype"].ToInt();

                strSql.Append(" and owner_id=@owner_id and customertype="+ customertype);
                parameter.Add(DbParameters.CreateDbParameter("@owner_id", owner_id));
                
            }
            if (!queryParam["Content"].IsEmpty())
            {
                string Content = queryParam["Content"].ToString();

                strSql.AppendFormat(" and Content like '%{0}%' ", Content);
            }
            if (!queryParam["StartDate"].IsEmpty() && !queryParam["EndDate"].IsEmpty())
            {
                DateTime expire_begin = queryParam["StartDate"].ToDate();
                DateTime expire_end = queryParam["EndDate"].ToDate();

                strSql.Append(" and CONVERT(VARCHAR(10),ReportDate,120) BETWEEN @StartDate AND @EndDate ");

                parameter.Add(DbParameters.CreateDbParameter("@StartDate", expire_begin));
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", expire_end));                
            }

            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FixReportEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FixReportEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取维修材料列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<FixmaterialEntity> GetFixList(string keyValue)
        {
            var expression = LinqExtensions.True<FixmaterialEntity>();
            expression = expression.And(t => t.fixreportid == keyValue);

            RepositoryFactory<FixmaterialEntity> repository = new RepositoryFactory<FixmaterialEntity>();
            return repository.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取报修单号
        /// </summary>
        /// <returns></returns>
        public string fixNumber_No()
        {
            OtherincomeService dal = new OtherincomeService();
            return DateTime.Now.ToString("yyyyMMdd") + dal.GetMaxID(3, "wy_FixReport WHERE CONVERT(VARCHAR(10),ReportDate,120)=CONVERT(VARCHAR(10),GETDATE(),120)", "fixNumber_No", 3);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<FixReportEntity>(keyValue);

                string str2 = "DELETE FROM dbo.wy_fix_material WHERE fixreportid=@fixreportid ";
                DbParameter[] parameter2 ={
                            DbParameters.CreateDbParameter("@fixreportid",keyValue)
                        };
                db.ExecuteBySql(str2, parameter2);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }

            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, FixReportEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                return this.BaseRepository().Update(entity).ToString();
            }
            else
            {
                OtherincomeService dal = new OtherincomeService();
                entity.FixReportID = entity.propertyid + dal.GetMaxID(8, "wy_FixReport", "FixReportID", 8);
                if (string.IsNullOrEmpty(entity.fixNumber_No))
                {
                    entity.fixNumber_No = DateTime.Now.ToString("yyyyMMdd") + dal.GetMaxID(3, "wy_FixReport WHERE CONVERT(VARCHAR(10),ReportDate,120)=CONVERT(VARCHAR(10),GETDATE(),120)", "fixNumber_No", 3);
                }
                //entity.Create(); GetMaxID
                this.BaseRepository().Insert(entity);

                return entity.FixReportID;
            }
        }
        /// <summary>
        /// 修改材料
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entitylist"></param>
        public void SavesForm(FixReportEntity entity, List<FixmaterialEntity> entitylist)
        {
            OtherincomeService dal = new OtherincomeService();
            int maxid = dal.GetMaxID(0, "wy_fix_material", "pkeyid", 8).ToInt();
            var strSql = new StringBuilder();
            
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Update(entity);
                if (entitylist != null)
                {
                    foreach (FixmaterialEntity item in entitylist)
                    {
                        ////修改库存
                        if (string.IsNullOrEmpty(item.pkeyid))
                        {
                            item.fixreportid = entity.FixReportID;
                            item.pkeyid = entity.propertyid + Utils.SupplementZero(maxid.ToString(), 8);
                            db.Insert(item);

                            maxid++;
                        }
                        else
                        {
                        }
                    }
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
