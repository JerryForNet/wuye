using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.PersonnelManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 10:41
    /// 描 述：部门信息
    /// </summary>
    public interface HrDepartmentIService
    {
        #region 获取数据

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<HrDepartmentEntity> GetList();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<HrDepartmentEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<HrDepartmentEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        HrDepartmentEntity GetEntity(int keyValues);

        #endregion 获取数据

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
        void SaveForm(string keyValue, HrDepartmentEntity entity);

        #endregion 提交数据
    }
}