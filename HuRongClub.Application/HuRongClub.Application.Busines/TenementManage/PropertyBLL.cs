using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;
using HuRongClub.Application.Entity.TenementManage.ViewModel;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：姜良福
    /// 日 期：2017-04-01 10:09
    /// 描 述：产业档案
    /// </summary>
    public class PropertyBLL
    {
        private PropertyIService service = new PropertyService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PropertyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PropertyEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PropertyEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }


        /// <summary>
        /// 获取物业下拉列表
        /// </summary>
        /// <param name="property_ids">property_id 集合</param>
        /// <returns></returns>
        public IEnumerable<PropertyEntity> GetListBySel(string property_ids)
        {
            return service.GetListBySel(property_ids);
        }
        /// <summary>
        /// 获取入库汇总
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OutInModel> GetInList()
        {
            return service.GetInList();
        }
        /// <summary>
        /// 获取出库汇总
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OutInModel> GetOutList()
        {
            return service.GetOutList();
        }


        #endregion

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
        public void SaveForm(string keyValue, PropertyEntity entity)
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
        #endregion
    }
}
