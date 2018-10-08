using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-21 09:28
    /// 描 述：Rentfeeitem
    /// </summary>
    public interface RentfeeitemIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<RentfeeitemEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RentfeeitemEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RentfeeitemEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns>返回列表</returns>
        IEnumerable<RentfeeitemListEntity> GetLists(string contractid);
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
        /// <param name="property_id">物业编号</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        string SaveForm(string keyValue,string property_id, RentfeeitemEntity entity);
        #endregion
    }
}
