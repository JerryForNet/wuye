using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本 6.1
    
    /// 创 建：超级管理员
    /// 日 期：2017-04-01 10:57
    /// 描 述：Owner
    /// </summary>
    public interface OwnerIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<OwnerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<OwnerEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        OwnerEntity GetEntity(string keyValue);
        /// <summary>
        /// 单元业主信息
        /// </summary>
        /// <param name="id">房间编号</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="type">1按room_id 查询 2 按owner_id 查询</param>
        /// <returns></returns>
        IEnumerable<OwnerModelEntity> GetInfo(string id, string property_id, int type);
        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        IEnumerable<OwnerListEntity> GetInfoList(string property_id,string queryJson, Pagination pagination);
        /// <summary>
        /// 业主下拉
        /// </summary>
        /// <param name="building_id"></param>
        /// <returns></returns>
        IEnumerable<OwnerEntity> GetListBySel(string building_id);
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
        string SaveForm(string keyValue, OwnerEntity entity);
        /// <summary>
        /// 修改业主姓名
        /// </summary>
        /// <param name="owner_id">业主编号</param>
        /// <param name="owner_name">业主姓名</param>
        void UpdateOwnerName(string owner_id, string owner_name);
        /// <summary>
        /// 批量进户
        /// </summary>
        /// <param name="list"></param>
        void BatchFrom(List<OwnerEntity> list);
        #endregion
    }
}
