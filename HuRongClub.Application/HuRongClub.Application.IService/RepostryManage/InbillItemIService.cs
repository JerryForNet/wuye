using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-26 13:42
    /// 描 述：入库汇总
    /// </summary>
    public interface InbillItemIService
    {
        #region 获取数据

        /// <summary>
        /// 入库汇总列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<billItemModel> GetList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<InbillItemByfinidModel> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表--根据入库单主键
        /// </summary>
        /// <param name="finbillid"></param>
        /// <returns></returns>
        IEnumerable<InbillItemByfinidModel> GetItemList(string queryJson);

        /// <summary>
        /// 入库统计列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<billStatisticsModel> GetStatisticsList(Pagination pagination, string queryJson);

        /// <summary>        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        InbillItemEntity GetEntity(string keyValue);

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        void RemoveForm(string keyValue, string type);
        /// <summary>
        /// 删除数据--整张
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        void RemoveFormAll(string keyValue, string type);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, InbillItemEntity entity);

        #endregion 提交数据
    }
}