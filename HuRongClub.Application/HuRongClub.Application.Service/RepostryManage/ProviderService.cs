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
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 15:26
    /// 描 述：供应商管理
    /// </summary>
    public class ProviderService : RepositoryFactory, ProviderIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ProviderModel> GetPageList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<ProviderModel> repository = new RepositoryFactory<ProviderModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" select * from v_providerlastbuy where 1=1  ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件 供应商名称、购买物品
            if (!queryParam["owner_name"].IsEmpty())
            {
                string owner_name = queryParam["owner_name"].ToString();
                strSql.Append(" and (fname like @fname or goodsinfo like @fname) ");
                parameter.Add(DbParameters.CreateDbParameter("@fname", '%'+ owner_name + '%'));
            }
            if (!queryParam["StartDate"].IsEmpty())
            {
                string StartDate = queryParam["StartDate"].ToString();
                strSql.Append(" and lastbuy >='" + StartDate + "' ");
                parameter.Add(DbParameters.CreateDbParameter("@StartDate", StartDate));
            }
            if (!queryParam["EndDate"].IsEmpty())
            {
                string EndDate = queryParam["EndDate"].ToString();
                strSql.Append(" and lastbuy <='" + EndDate + "' ");
                parameter.Add(DbParameters.CreateDbParameter("@EndDate", EndDate));
            }
            return repository.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ProviderModel> GetList(string queryJson)
        {
            RepositoryFactory<ProviderModel> repository = new RepositoryFactory<ProviderModel>();
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT * FROM tb_wh_provider ORDER BY fname ASC ");
            return repository.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ProviderEntity GetEntity(string keyValue)
        {
            RepositoryFactory<ProviderEntity> repository = new RepositoryFactory<ProviderEntity>();

            return repository.BaseRepository().FindEntity(keyValue);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            RepositoryFactory<ProviderEntity> repository = new RepositoryFactory<ProviderEntity>();
            repository.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ProviderEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                entity.fproviderid = modifyid();
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 获取主键id
        /// </summary>
        /// <returns></returns>
        public string modifyid()
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select isnull(max(cast(fproviderid as int)),0)+1 from tb_wh_provider ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            if (bj != null)
            {
                return bj.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion 提交数据
    }
}