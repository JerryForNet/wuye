using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-10 15:27
    /// 描 述：Room
    /// </summary>
    public interface RoomIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<RoomEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RoomEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RoomEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取房屋类型下拉列表
        /// </summary>
        /// <param name="dictid">主表ID</param>
        /// <returns></returns>
        IEnumerable<Entity.SysManage.DictitemEntity> GetListBySel(int dictid);

        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="room_id">房间编号</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        IEnumerable<RoomModelEntity> GetInfo(string room_id, string property_id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="building_id">所属楼栋编号</param>
        /// <param name="Type">1 全部 2空房间</param>
        /// <returns>返回列表</returns>
        IEnumerable<RoomEntity> GetTreeList(string building_id, int Type);

        /// <summary>
        /// 租赁单元
        /// </summary>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        IEnumerable<RoomModelEntity> GetListByRent(string contractid);

        /// <summary>
        /// 获取批量费用导出数据
        /// </summary>
        /// <param name="Type">1 业主 2 租户</param>
        /// <param name="id">查询id</param>
        /// <returns></returns>
        DataTable GetListByFee(int Type, string id);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">owner_id</param>
        /// <returns></returns>
        RoomEntity GetEntitys(string keyValue);

        /// <summary>
        /// 获取空房间信息（用于下载批量导出数据）
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="building_id">归属楼栋</param>
        /// <returns></returns>
        DataTable GetRoomInfo(string property_id, string building_id);

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
        void SaveForm(string keyValue, RoomEntity entity);

        /// <summary>
        /// 修改表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(RoomEntity entity);

        /// <summary>
        /// 出户操作
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="owner_id"></param>
        void SaveOutFrom(string room_id, string owner_id);

        #endregion

        /// <summary>
        /// 更新房间可用状态
        /// </summary>
        /// <param name="roomids"></param>
        /// <param name="p"></param>
        void UpdateRent(string roomids, int p);
    }
}