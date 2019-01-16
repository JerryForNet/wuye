using HuRongClub.Application.Entity.FinanceManage;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：Jerry.Li
    /// 日 期：2019-01-16 21:36
    /// 描 述：InvoiceInfo
    /// </summary>
    public interface InvoiceInfoIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<InvoiceInfoEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        InvoiceInfoEntity GetEntity(string keyValue);

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
        void SaveForm(string keyValue, InvoiceInfoEntity entity);

        #endregion
    }
}