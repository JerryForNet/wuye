using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.IService.FinanceManage;
using HuRongClub.Data.Repository;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using HuRongClub.Util;

namespace HuRongClub.Application.Service.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：Jerry.Li
    /// 日 期：2019-01-16 21:36
    /// 描 述：InvoiceInfo
    /// </summary>
    public class InvoiceInfoService : RepositoryFactory<InvoiceInfoEntity>, InvoiceInfoIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<InvoiceInfoEntity> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            if (queryParam["keyword"] != null)
            {
                return this.BaseRepository().IQueryable().Where(w => w.khmc.Contains(queryParam["keyword"].ToString().Trim())).ToList();
            }
            else
            {
                return this.BaseRepository().IQueryable().ToList();
            }

        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public InvoiceInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
        public void SaveForm(string keyValue, InvoiceInfoEntity entity)
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
        #endregion
    }
}
