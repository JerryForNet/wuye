using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuRongClub.Application.IService.PersonnelManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 09:43
    /// 描 述：员工档案
    /// </summary>
    public interface EmployinfoIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<EmployinfoEntity> GetList();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<EmployinfoListEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 用户组列表(ALL)
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployinfoEntity> GetAllList();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        EmployinfoEntity GetEntity(int keyValue);

        /// <summary>
        /// 获取信息--导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetListInfo(string queryJson);

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="deptid">部门编号</param>
        /// <returns></returns>
        IEnumerable<EmployinfoEntity> GetList(int deptid);

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        void UpdateState(string keyValue, Int16 State, DateTime? fireoutdate);

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
        void SaveForm(string keyValue, EmployinfoEntity entity);

        /// <summary>
        /// 合同续费
        /// </summary>
        /// <param name="keyValue">续费员工编号</param>
        /// <param name="firedate">续费时间</param>
        void ExpireFrom(string keyValue, DateTime? firedate);

        #endregion 提交数据
    }
}