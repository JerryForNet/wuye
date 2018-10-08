using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Application.Service.PersonnelManage;
using HuRongClub.Cache.Factory;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.PersonnelManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 10:41
    /// 描 述：部门信息
    /// </summary>
    public class HrDepartmentBLL
    {
        private HrDepartmentIService service = new HrDepartmentService();

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "HrDepartmentCache";

        #region 获取数据

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HrDepartmentEntity> GetList()
        {
            return service.GetList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<HrDepartmentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<HrDepartmentEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public HrDepartmentEntity GetEntity(int keyValue)
        {
            int keyValues = Convert.ToInt32(keyValue);
            return service.GetEntity(keyValues);
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
                
                CacheFactory.Cache().RemoveCache(cacheKey);
                CacheFactory.Cache().RemoveCache("DepartmentCache");
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
        public void SaveForm(string keyValue, HrDepartmentEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
                CacheFactory.Cache().RemoveCache(cacheKey);
                CacheFactory.Cache().RemoveCache("DepartmentCache");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 提交数据
    }
}