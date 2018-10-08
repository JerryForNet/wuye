using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.SysManage;
using HuRongClub.Application.IService.SysManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuRongClub.Application.Service.SysManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 20:55
    /// 描 述：字典详情
    /// </summary>
    public class DictitemService : RepositoryFactory<DictitemEntity>, DictitemIService
    {
        #region 获取数据

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DictitemEntity> GetList()
        {
            return this.BaseRepository().FindList("SELECT * FROM [sys_dictitem]");
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DictitemEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DictitemEntity> GetPageList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            //查询条件
            int dictid = queryParam["dictid"].ToInt();
            var strSql = new StringBuilder();
            strSql.Append(" SELECT  * FROM dbo.sys_dictitem ");
            strSql.AppendFormat("WHERE dictid='{0}' ", dictid);
            return this.BaseRepository().FindList(strSql.ToString());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DictitemEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, DictitemEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                entity.itemid = modifyid();
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
            strSql.Append(@" select isnull(max(cast(itemid as int)),0)+1 from sys_dictitem ");
            object bj = this.BaseRepository().FindObject(strSql.ToString());
            return bj.ToString();
        }

        #endregion 提交数据
    }
}