using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Entity.FinanceManage.ViewModel;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：李俊
    /// 日 期：2018-12-22 21:13
    /// 描 述：财务账开关
    /// </summary>
    public interface FeeCloseIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeeCloseModel> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeCloseEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeeCloseEntity GetEntity(string keyValue);

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
        void SaveForm(string keyValue, FeeCloseEntity entity);

        #endregion

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="fyear"></param>
        /// <param name="fmonth"></param>
        /// <param name="fstatus"></param>
        void UpdateStatus(string propertyId, string fyear, string fmonth, int fstatus);

        FeeCloseEntity GetCurrentStatus(string propertyId);
    }
}