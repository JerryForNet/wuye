using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-06-20 10:32
    /// 描 述：Lblist
    /// </summary>
    public class LblistService : RepositoryFactory<LblistEntity>, LblistIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<LblistEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from wh_lblist where 1=1 ");
            //超期
            if (!queryParam["iscq"].IsEmpty())
            {
                if (queryParam["iscq"].ToString() == "1")
                {
                    strSql.Append(" and isnew=1 and datediff(day,lbenddate,getdate())>0 ");
                }
            }
            else
            {
                if (!queryParam["isnew"].IsEmpty())
                {
                    string isnew = queryParam["isnew"].ToString();
                    if (isnew != "1")
                    {
                        strSql.Append(" and isnew=1 ");
                    }
                }
                else
                {
                    strSql.Append(" and isnew=1 ");
                }
            }
            //品种
            if (!queryParam["dictitemid"].IsEmpty())
            {
                int dictitemid = queryParam["dictitemid"].ToInt();
                strSql.Append(" and dictitemid=@dictitemid ");
                parameter.Add(DbParameters.CreateDbParameter("@dictitemid", dictitemid));
            }
            //部门
            if (!queryParam["deptid"].IsEmpty())
            {
                int deptid = queryParam["deptid"].ToInt();
                strSql.Append(" and deptid=@deptid ");
                parameter.Add(DbParameters.CreateDbParameter("@deptid", deptid));
            }
            //员工
            if (!queryParam["empid"].IsEmpty())
            {
                int empid = queryParam["empid"].ToInt();
                strSql.Append(" and empid=@empid ");
                parameter.Add(DbParameters.CreateDbParameter("@empid", empid));
            }
            //领用日期
            if (!queryParam["StartDate"].IsEmpty()&& !queryParam["EndDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and CONVERT(VARCHAR(10),lbbegindate,120) BETWEEN @StartDate AND @EndDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            //到期日期
            if (!queryParam["SDate"].IsEmpty() && !queryParam["EDate"].IsEmpty())
            {
                string SDate = queryParam["SDate"].ToString();
                string EDate = queryParam["EDate"].ToString();
                strSql.Append(" and CONVERT(VARCHAR(10),lbenddate,120) BETWEEN @SDate AND @EDate ");
                parameter.Add(DbParameters.CreateDbParameter("@SDate", SDate));
                parameter.Add(DbParameters.CreateDbParameter("@EDate", EDate));
            }

            if (parameter.Count > 0)
            {
                return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
            }
            else
            {
                return this.BaseRepository().FindList(strSql.ToString(), pagination);
            }            
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<LblistEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LblistEntity GetEntity(int keyValue)
        {
            var expression = LinqExtensions.True<LblistEntity>();
            expression = expression.And(t => t.lid == keyValue);
            return this.BaseRepository().FindEntity(expression);
        }
        /// <summary>
        /// 根据品种编号和人员编号查询数据
        /// </summary>
        /// <param name="dictitemid">品种编号</param>
        /// <param name="empid">人员编号</param>
        /// <returns></returns>
        public IEnumerable<LblistEntity> GetList(int dictitemid, int empid)
        {
            var expression = LinqExtensions.True<LblistEntity>();
            expression = expression.And(t => t.dictitemid == dictitemid);
            expression = expression.And(t => t.empid == empid);

            return this.BaseRepository().IQueryable(expression).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, LblistEntity entity)
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
        /// 修改isnew
        /// </summary>
        /// <param name="dictitemid">品种编号</param>
        /// <param name="empid">人员编号</param>
        public void UpdateByisnew(int dictitemid, int empid)
        {
            string str = "update wh_lblist set isnew=0 where dictitemid=" + dictitemid + "  and empid=" + empid;
            this.BaseRepository().ExecuteBySql(str);
        }
        #endregion
    }
}
