using HuRongClub.Application.Entity.FinanceManage.ViewModel;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Util.WebControl;
using System;
using System.Data;

using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:08
    /// 描 述：Feeticket
    /// </summary>
    public interface FeeticketIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeeticketModel> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeticketEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeeticketEntity GetEntity();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeeticketEntity GetEntity(string keyValue);

        IEnumerable<FeeticketEntity> GetListByIds(string keyValue);

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="dept_id">领用部门ID</param>
        /// <param name="ticket_type">发票类别</param>
        /// <param name="ticket_status">发票状态</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeticketEntity> GetSelList(string dept_id, string ticket_type, int ticket_status);

        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <param name="keyVaule"></param>
        /// <returns></returns>
        CostInfoEntity GetCostByTicketId(string keyVaule);

        /// <summary>
        /// 获取其他收入明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IEnumerable<OtherIncomeEntity> GetOherIncomeList(string keyValue, string queryJson);

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 修改发票状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：100是作废发票，2是已归档，1是已使用</param>

        void UpdateState(string keyValue, Int16 State);

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
        void SaveForm(string keyValue, FeeticketEntity entity);

        #endregion 提交数据

        IEnumerable<TicketPrintEntity> GetPrintListJson(string keyValue, string queryJson);

        string GetMaxID();

        DataTable GetBatchPrint(string tickets);
        

    }
}