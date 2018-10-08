using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:13
    /// 描 述：FixReport
    /// </summary>
    public interface FixReportIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FixReportEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="propertyid">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FixReportEntity> GetPageList(Pagination pagination, string propertyid, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FixReportEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FixReportEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取维修材料列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IEnumerable<FixmaterialEntity> GetFixList(string keyValue);
        /// <summary>
        /// 获取报修单号
        /// </summary>
        /// <returns></returns>
        string fixNumber_No();
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        string SaveForm(string keyValue, FixReportEntity entity);
        /// <summary>
        /// 修改材料
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entitylist"></param>
        void SavesForm(FixReportEntity entity, List<FixmaterialEntity> entitylist);
        #endregion
    }
}
