using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:07
    /// 描 述：Owner_fee
    /// </summary>
    public class Owner_feeBLL
    {
        private Owner_feeIService service = new Owner_feeService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Owner_feeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Owner_feeEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Owner_feeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="owner_id">业主编号</param>
        /// <param name="room_id">房屋编号</param>
        /// <returns></returns>
        public IEnumerable<OwnerFeeModelEntity> GetList(string owner_id, string room_id)
        {
            return service.GetList(owner_id, room_id);
        }
        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <param name="pagination">排序</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<OwnerFeeIndexEntity> GetList(Pagination pagination, string queryJson, string property_id)
        {
            return service.GetList(pagination, queryJson, property_id);
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
        /// <param name="property_id">物业编号</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveForm(string keyValue,string property_id, Owner_feeEntity entity)
        {
            try
            {
               return service.SaveForm(keyValue, property_id, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
