using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using HuRongClub.Application.Entity.TenementManage.ViewModel;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：姜良福
    /// 日 期：2017-04-01 10:09
    /// 描 述：产业档案
    /// </summary>
    public interface PropertyIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PropertyEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<PropertyEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        PropertyEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取物业下拉列表
        /// </summary>
        /// <param name="property_ids">property_id 集合</param>
        /// <returns></returns>
        IEnumerable<PropertyEntity> GetListBySel(string property_ids);
        /// <summary>
        /// 获取入库汇总
        /// </summary>
        /// <returns></returns>
        IEnumerable<OutInModel> GetInList();
        /// <summary>
        /// 获取出库汇总
        /// </summary>
        /// <returns></returns>
        IEnumerable<OutInModel> GetOutList();

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
        void SaveForm(string keyValue, PropertyEntity entity);
        #endregion
    }
}
