using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 11:47
    /// 描 述：领用单对应物品信息
    /// </summary>
    public interface OutbillitemIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<OutbillgoodModel> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<OutbillgoodModel> GetList(string queryJson);

        /// <summary>
        /// 获取领用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<billItemModel> GetOutList(Pagination pagination, string queryJson);

        /// <summary>
        /// 领用统计
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<OutStatisticsModel> GetStatisticsList(Pagination pagination, string queryJson);
        /// <summary>
        /// 领用费用
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<ReceiveCostModel> GetReceiveCostList();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        OutbillitemEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据出库单编号查询 明细列表
        /// </summary>
        /// <param name="foutbillid"></param>
        /// <returns></returns>
        IEnumerable<OutbillitemEntity> GetListByFoutbillid(string foutbillid);
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
        void SaveForm(string keyValue, OutbillitemEntity entity);

        string NuCounts(string fnumber);

        #endregion 提交数据
    }
}