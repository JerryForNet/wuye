using HuRongClub.Application.Code;
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
    /// 日 期：2017-04-06 15:01
    /// 描 述：Energy
    /// </summary>
    public class EnergyService : RepositoryFactory<EnergyEntity>, EnergyIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EnergyListEntity> GetPageList(Pagination pagination, string queryJson,string property_id)
        {
            RepositoryFactory<EnergyListEntity> repository = new RepositoryFactory<EnergyListEntity>();
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();
            strSql.Append(@"SELECT *,(ISNULL(FEmoney,0)+ISNULL(FWmoney,0)+ISNULL(FOmoney,0)+ISNULL(FGmoney,0)) Subtotal FROM tb_energy  where FPropID='"+ property_id + "' ");
            
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and fdate>=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and fdate<=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EnergyListEntity> GetList(string queryJson, string property_id)
        {
            RepositoryFactory<EnergyListEntity> repository = new RepositoryFactory<EnergyListEntity>();
            var strSql = new StringBuilder();
            var parameter = new List<DbParameter>();
            strSql.Append(@"SELECT *,(ISNULL(FEmoney,0)+ISNULL(FWmoney,0)+ISNULL(FOmoney,0)+ISNULL(FGmoney,0)) Subtotal FROM tb_energy  where FPropID='" + property_id + "' ");

            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and fdate>=@StartDate ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and fdate<=@EndDate  ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }

            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray()).OrderByDescending(t=>t.FDate);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EnergyEntity GetEntity(int keyValue)
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
        public void SaveForm(string keyValue, EnergyEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.finputdate = DateTime.Now; //录入时间
                entity.Fuserid = Convert.ToInt32(OperatorProvider.Provider.Current().OldSystemUserID); //当前登录者id

                if (entity.FDate == null)
                {
                    throw new Exception("消耗日期不能为空！");
                }

                this.BaseRepository().Insert(entity);
            }
        }

        #endregion 提交数据
    }
}