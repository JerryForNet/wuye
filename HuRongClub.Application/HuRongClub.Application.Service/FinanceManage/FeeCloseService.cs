using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.IService.FinanceManage;
using HuRongClub.Data;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：李俊
    /// 日 期：2018-12-22 21:13
    /// 描 述：财务账开关
    /// </summary>
    public class FeeCloseService : RepositoryFactory<FeeCloseEntity>, FeeCloseIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeCloseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            if (String.IsNullOrEmpty(queryParam["propertyIds"].ToString()))
            {
                return null;
            }

            this.CheckData();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT  wy_feeclose.*
                                FROM    dbo.wy_feeclose
                                        INNER JOIN dbo.wy_property ON wy_property.property_id = wy_feeclose.property_id
                                WHERE   wy_feeclose.property_id IN ('{0}')", queryParam["propertyIds"].ToString().Replace(",", "','"));

            var parameter = new List<DbParameter>();
            if (queryParam["fyear"] != null)
            {
                sql.AppendFormat(" AND fyear = '{0}'", queryParam["fyear"]);
                parameter.Add(DbParameters.CreateDbParameter("@fyear", queryParam["fyear"].ToString()));
            }
            if (queryParam["fmonth"] != null)
            {
                sql.AppendFormat(" AND fmonth = '{0}'", queryParam["fmonth"]);
                parameter.Add(DbParameters.CreateDbParameter("@fmonth", queryParam["fmonth"].ToString()));
            }

            return this.BaseRepository().FindList(sql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeCloseEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeCloseEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        #endregion 获取数据

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
        public void SaveForm(string keyValue, FeeCloseEntity entity)
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

        #endregion 提交数据

        #region 私有方法

        private void CheckData()
        {
            DbParameter[] parameter ={
                DbParameters.CreateDbParameter("@fuser",string.Empty)
            };

            this.BaseRepository().ExecuteByProc("CreateFeeClose", parameter);
        }

        #endregion 私有方法
    }
}