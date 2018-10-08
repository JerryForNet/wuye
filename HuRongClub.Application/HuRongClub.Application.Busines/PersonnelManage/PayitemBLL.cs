using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Application.Service.PersonnelManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HuRongClub.Application.Busines.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:06
    /// 描 述：Payitem
    /// </summary>
    public class PayitemBLL
    {
        private PayitemIService service = new PayitemService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PayitemEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PayitemEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PayitemEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        public void SaveForm(string keyValue, PayitemEntity entity)
        {
            try
            {
                PayitemService payservice = new PayitemService();
                if (entity.itemcode == null || entity.itemcode == "")
                {

                    entity.itemcode = payservice.GetKey(10);
                    entity.CreatorId = 1;
                    entity.disable = "1";
                    entity.CreatorName = Code.OperatorProvider.Provider.Current().UserName;
                }
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

         /// <summary>
        /// 修改状态（启用、禁用）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void UpdateDisable(string keyValue,string disable)
        {
            try
            {
                PayitemEntity entity = new PayitemEntity();
                if (disable == "1")
                {
                    entity.disable = "0";
                }
                else
                {
                    entity.disable = "1";
                }
                service.SaveForm(keyValue, entity);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
