using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.IService.RepostryManage;
using HuRongClub.Application.Service.RepostryManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;

namespace HuRongClub.Application.Busines.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-06-20 10:32
    /// 描 述：Lblist
    /// </summary>
    public class LblistBLL
    {
        private LblistIService service = new LblistService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<LblistEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<LblistEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LblistEntity GetEntity(int keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 根据品种编号和部门编号查询数据
        /// </summary>
        /// <param name="dictitemid">品种编号</param>
        /// <param name="empid">人员编号</param>
        /// <returns></returns>
        public IEnumerable<LblistEntity> GetList(int dictitemid, int empid)
        {
            return service.GetList(dictitemid, empid);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int keyValue)
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
        public void SaveForm(string keyValue, LblistEntity entity)
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
        /// 修改isnew
        /// </summary>
        /// <param name="dictitemid">品种编号</param>
        /// <param name="empid">人员编号</param>
        public void UpdateByisnew(int dictitemid, int empid)
        {
            try
            {
                service.UpdateByisnew(dictitemid, empid);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
    }
}
