using HuRongClub.Application.Entity.FinanceManage.ViewModel;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Cache.Factory;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:08
    /// 描 述：Feeticket
    /// </summary>
    public class FeeticketBLL
    {
        private FeeticketIService service = new FeeticketService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeticketModel> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeticketEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeticketEntity> GetListByIds(string keyValue)
        {
            return service.GetListByIds(keyValue);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeticketEntity GetEntity()
        {
            return service.GetEntity();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeticketEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }


        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public CostInfoEntity GetCostByTicketId(string keyValue)
        {
            return service.GetCostByTicketId(keyValue);
        }


        /// <summary>
        /// 获取其他收入明细列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<OtherIncomeEntity> GetOherIncomeList(string keyValue, string queryJson)
        {
            return service.GetOherIncomeList(keyValue, queryJson);
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="dept_id">领用部门ID</param>
        /// <param name="ticket_type">发票类别</param>
        /// <param name="ticket_status">发票状态</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeticketEntity> GetSelList(string dept_id, string ticket_type, int ticket_status)
        {
            return service.GetSelList(dept_id, ticket_type, ticket_status);
        }
        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 修改发票状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：100是作废发票，2是已归档，1是已使用</param>
        public void UpdateState(string keyValue, Int16 State)
        {
            try
            {
                service.UpdateState(keyValue, State);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FeeticketEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 提交数据
    }
}