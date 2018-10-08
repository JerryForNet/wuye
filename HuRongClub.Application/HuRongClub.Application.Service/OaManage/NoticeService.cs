using HuRongClub.Application.Entity.OaManage;
using HuRongClub.Application.IService.OaManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HuRongClub.Application.Service.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-28 20:16
    /// 描 述：通知公告
    /// </summary>
    public class NoticeService : RepositoryFactory<NoticeEntity>, NoticeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<NoticeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<NoticeEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.title.Contains(keyword));
            }
            if (!queryParam["type_id"].IsEmpty())
            {
                string type_id = queryParam["type_id"].ToString();
                expression = expression.And(t => t.notice_type.Contains(type_id));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 是否有管理者身份
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Checkmanager(string username)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT * FROM dbo.hr_department WHERE manager ='" + username + "' ");
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

        public IEnumerable<NoticeEntity> GetTop5()
        {
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT top 5  * FROM dbo.oa_notice ORDER BY  create_time desc ");
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<NoticeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NoticeEntity GetEntity(int keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        #endregion 获取数据

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
        public void SaveForm(string keyValue, NoticeEntity entity)
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
    }
}