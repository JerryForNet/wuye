using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.IService.FinanceManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-08 16:21
    /// 描 述：进账认领
    /// </summary>
    public class FeenoticeService : RepositoryFactory<FeenoticeEntity>, FeenoticeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeenoticeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeenoticeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeenoticeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">参数</param>
        /// <returns></returns>
        public IEnumerable<FeenoticeEntity> GetList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var parameter = new List<DbParameter>();

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM (
                            SELECT wf.NoticeID,wf.accountcode,wf.accountcompany,wf.accountdate,wf.account,
                            wf.memo,wf.CreatorName,wf.CreateDate,ISNULL(bu.RealName,bu.Account)checkuserid,wf.checkdate,wp.property_name AS checkproperty,wf.checkremark,wf.accounts,wf.purpose
                            FROM wy_feenotice wf
                            LEFT JOIN dbo.Base_User bu ON wf.checkuserid=bu.UserId
                            LEFT JOIN wy_property wp ON wf.checkproperty=wp.property_id ");
            //物业编号
            if (!queryParam["checkproperty"].IsEmpty())
            {
                string checkproperty = queryParam["checkproperty"].ToString();
                strSql.Append(" WHERE wf.checkproperty=@checkproperty");
                parameter.Add(DbParameters.CreateDbParameter("@checkproperty", checkproperty));
            }
            strSql.Append(@" )x1 WHERE 1=1  ");
            //帐号编号
            if (!queryParam["accountcode"].IsEmpty())
            {
                string accountcode = queryParam["accountcode"].ToString();
                strSql.Append(" AND accountcode=@accountcode ");
                parameter.Add(DbParameters.CreateDbParameter("@accountcode", accountcode));
            }
            //账单单位
            if (!queryParam["accountcompany"].IsEmpty())
            {
                string accountcompany = queryParam["accountcompany"].ToString();
                strSql.Append(" AND accountcompany like '%" + accountcompany + "%' ");
            }
            //认领用户
            if (!queryParam["checkuserid"].IsEmpty())
            {
                string checkuserid = queryParam["checkuserid"].ToString();
                strSql.Append(" AND checkuserid=@checkuserid ");
                parameter.Add(DbParameters.CreateDbParameter("@checkuserid", checkuserid));
            }
            //参建时间
            if (!queryParam["StartDate"].IsEmpty() && !queryParam["EndDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" AND CONVERT(VARCHAR(10),CreateDate,120) BETWEEN @StartDate AND @EndDate ");

                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            //认领时间
            if (!queryParam["CSDate"].IsEmpty() && !queryParam["CEDate"].IsEmpty())
            {
                string CSDate = queryParam["CSDate"].ToString();
                string CEDate = queryParam["CEDate"].ToString();
                strSql.Append(" AND CONVERT(VARCHAR(10),checkdate,120) BETWEEN @CSDate AND @CEDate ");

                parameter.Add(DbParameters.CreateDbParameter("@CSDate", CSDate));
                parameter.Add(DbParameters.CreateDbParameter("@CEDate", CEDate));
            }
            //认领状态
            if (!queryParam["state"].IsEmpty())
            {
                string state = queryParam["state"].ToString();
                if (state == "1")
                {
                    strSql.Append(" AND x1.checkproperty IS NOT NULL ");
                }
                else if (state == "2") {
                    strSql.Append(" AND x1.checkproperty IS NULL ");
                }
            }
            //金额
            if (!queryParam["account"].IsEmpty())
            {
                string account = queryParam["account"].ToString();
                strSql.Append(" AND x1.account = @account ");

                parameter.Add(DbParameters.CreateDbParameter("@account", account));
            }

            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }


        /// <summary>
        /// 账单编号不能重复
        /// </summary>
        /// <param name="accountcode">账单编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool Existaccountcode(string accountcode, string keyValue)
        {
            var expression = LinqExtensions.True<FeenoticeEntity>();
            expression = expression.And(t => t.accountcode == accountcode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.NoticeID != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 是否认领
        /// </summary>
        /// <param name="keyValue">账单编号</param>
        /// <returns></returns>
        public bool ExistIsClaim(string keyValue)
        {
            string strSql = "SELECT checkuserid FROM wy_feenotice WHERE NoticeID=@NoticeID";
            DbParameter[] param = {
                DbParameters.CreateDbParameter("@NoticeID", keyValue)
            };

            object obj = this.BaseRepository().FindObject(strSql, param);
            if (obj != null&& !string.IsNullOrEmpty(obj.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public void SaveForm(string keyValue, FeenoticeEntity entity)
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
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        public void ImportForm(List<FeenoticeEntity> list)
        {
            if (list != null && list.Count > 0)
            {
                IRepository<FeenoticeEntity> db = this.BaseRepository().BeginTrans();
                try
                {
                    foreach (FeenoticeEntity item in list)
                    {
                        item.Create();
                        db.Insert(item);
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
