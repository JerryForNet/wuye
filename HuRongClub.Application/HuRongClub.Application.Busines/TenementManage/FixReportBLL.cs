using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:13
    /// 描 述：FixReport
    /// </summary>
    public class FixReportBLL
    {
        private FixReportIService service = new FixReportService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FixReportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="propertyid">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FixReportEntity> GetPageList(Pagination pagination, string propertyid, string queryJson)
        {
            return service.GetPageList(pagination, propertyid, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FixReportEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FixReportEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取维修材料列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<FixmaterialEntity> GetFixList(string keyValue)
        {
            return service.GetFixList(keyValue);
        }
        /// <summary>
        /// 获取报修单号
        /// </summary>
        /// <returns></returns>
        public string fixNumber_No()
        {
            return service.fixNumber_No();
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
        public string SaveForm(string keyValue, FixReportEntity entity)
        {
            try
            {
                return service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 修改材料
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entitylist"></param>
        public void SavesForm(FixReportEntity entity, List<FixmaterialEntity> entitylist)
        {
            try
            {
                service.SavesForm(entity, entitylist);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
