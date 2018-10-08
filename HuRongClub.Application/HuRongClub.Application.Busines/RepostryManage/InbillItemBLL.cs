using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage.ViewModel;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.Service.RepostryManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-26 13:42
    /// 描 述：入库汇总
    /// </summary>
    public class InbillItemBLL
    {
        private InbillItemIService service = new InbillItemService();

        #region 获取数据

        /// <summary>
        /// 入库汇总列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<billItemModel> GetList(Pagination pagination, string queryJson)
        {
            return service.GetList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<InbillItemByfinidModel> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 根据入库单号获取列表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<InbillItemByfinidModel> GetListJsonByfinbillid(string queryJson)
        {
            return service.GetItemList(queryJson);
        }

        /// <summary>
        /// 入库统计列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<billStatisticsModel> GetStatisticsList(Pagination pagination, string queryJson)
        {
            return service.GetStatisticsList(pagination, queryJson);
        }

        /// <summary>        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public InbillItemEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        public void RemoveForm(string keyValue,string type)
        {
            try
            {
                service.RemoveForm(keyValue, type);
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
        /// <param name="type">inid 入库单  outid 出库单</param>
        public void RemoveFormAll(string keyValue,string type)
        {
            try
            {
                service.RemoveFormAll(keyValue, type);
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
        public void SaveForm(string keyValue, InbillItemEntity entity)
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