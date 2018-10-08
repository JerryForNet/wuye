using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.Service.RepostryManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.RepostryManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 13:28
    /// 描 述：物品类别
    /// </summary>
    public class GoodstypeBLL
    {
        private GoodstypeIService service = new GoodstypeService();

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "GoodstypeCache";

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<GoodstypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GoodstypeEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        public IEnumerable<GoodstypeEntity> GetList()
        {
            return service.GetList();
        }

        public IEnumerable<GoodstypeEntity> GetListJson(string fparentcode)
        {
            return service.GetListJson(fparentcode);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GoodstypeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, GoodstypeEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 提交数据
    }
}