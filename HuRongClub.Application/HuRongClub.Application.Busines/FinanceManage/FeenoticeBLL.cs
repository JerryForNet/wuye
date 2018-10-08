using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.IService.FinanceManage;
using HuRongClub.Application.Service.FinanceManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HuRongClub.Application.Busines.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-08 16:21
    /// 描 述：进账认领
    /// </summary>
    public class FeenoticeBLL
    {
        private FeenoticeIService service = new FeenoticeService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeenoticeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeenoticeEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeenoticeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">参数</param>
        /// <returns></returns>
        public IEnumerable<FeenoticeEntity> GetList(Pagination pagination, string queryJson)
        {
            return service.GetList(pagination, queryJson);
        }
        /// <summary>
        /// 账单编号不能重复
        /// </summary>
        /// <param name="accountcode">账单编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool Existaccountcode(string accountcode, string keyValue)
        {
            return service.Existaccountcode(accountcode, keyValue);
        }
        /// <summary>
        /// 是否认领
        /// </summary>
        /// <param name="keyValue">账单编号</param>
        /// <returns></returns>
        public bool ExistIsClaim(string keyValue)
        {
            return service.ExistIsClaim(keyValue);
        }
        #endregion

        #region 提交数据
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
        public void SaveForm(string keyValue, FeenoticeEntity entity)
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
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        public void ImportForm(List<FeenoticeEntity> list)
        {
            try
            {
                service.ImportForm(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
