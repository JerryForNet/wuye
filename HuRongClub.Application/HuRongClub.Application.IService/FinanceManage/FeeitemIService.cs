using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 17:11
    /// 描 述：费用科目表
    /// </summary>
    public interface FeeitemIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeeitemEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeitemEntity> GetList(string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeitemEntity> GetListg();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeeitemEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="group_id">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeitemEntity> GetListSel(string group_id);

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
        void SaveForm(string keyValue, FeeitemEntity entity);

        #endregion 提交数据
    }
}