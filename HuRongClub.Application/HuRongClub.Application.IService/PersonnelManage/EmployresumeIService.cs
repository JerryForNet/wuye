using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 16:33
    /// 描 述：员工履历信息
    /// </summary>
    public interface EmployresumeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<EmployresumeEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        EmployresumeEntity GetEntity(int keyValue);

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
        void SaveForm(string keyValue, EmployresumeEntity entity);

        #endregion 提交数据
    }
}