using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.IService.FinanceManage;
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

namespace HuRongClub.Application.Service.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 17:11
    /// 描 述：费用科目表
    /// </summary>
    public class FeeitemService : RepositoryFactory<FeeitemEntity>, FeeitemIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeitemEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<FeeitemEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "feeitem_name":            //科目名称
                        expression = expression.And(t => t.feeitem_name.Contains(keyword));
                        break;

                    case "group_id":          //分组名称
                        expression = expression.And(t => t.group_id.Contains(keyword));
                        break;

                    case "taxrate":          //税率
                        expression = expression.And(t => t.taxrate.Contains(keyword));
                        break;

                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeitemEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<FeeitemEntity>();
            expression = expression.And(t => t.group_id.Contains(queryJson));
            return this.BaseRepository().FindList(queryJson);
        }

        /// <summary>
        /// 费用列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FeeitemEntity> GetListg()
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeitemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="group_id">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeitemEntity> GetListSel(string group_id)
        {
            return this.BaseRepository().IQueryable().Where(t => t.group_id == group_id).OrderBy(t => t.feeitem_id).ToList();
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
        public void SaveForm(string keyValue, FeeitemEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                entity.feeitem_id = modifyid();
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
            strSql.Append(@" select isnull(max(cast(feeitem_id as int)),0)+1 from dbo.wy_feeitem ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            return bj.ToString();
        }

        #endregion 提交数据
    }
}