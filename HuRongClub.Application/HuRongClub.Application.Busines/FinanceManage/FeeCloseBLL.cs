using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Entity.FinanceManage.ViewModel;
using HuRongClub.Application.IService.FinanceManage;
using HuRongClub.Application.Service.FinanceManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;

namespace HuRongClub.Application.Busines.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：李俊
    /// 日 期：2018-12-22 21:13
    /// 描 述：财务账开关
    /// </summary>
    public class FeeCloseBLL
    {
        private FeeCloseIService service = new FeeCloseService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeCloseModel> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeCloseEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeCloseEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 查询当前财务账状态
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public bool GetCurrentStatus(string propertyId)
        {
            FeeCloseEntity entity = service.GetCurrentStatus(propertyId);
            if (entity != null)
            {
                return Convert.ToInt32(entity.fstatus) > 0 ? true : false;
            }

            return false;
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
        public void SaveForm(string keyValue, FeeCloseEntity entity)
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

        public void UpdateStatus(string propertyId, string fyear, string fmonth, int fstatus)
        {
            service.UpdateStatus(propertyId, fyear, fmonth, fstatus);
        }

        #endregion
    }
}