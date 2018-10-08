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
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    public class InbillBLL
    {
        private InbillIService service = new InbillService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<InbillModel> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public InbillEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<InbillItemByfinidModel> GetDetails(string keyValue)
        {
            return service.GetDetails(keyValue);
        }

        #endregion 获取数据

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

        public void SaveForm(string keyValue, InbillEntity entity, List<InbillItemEntity> entryList)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    InbillEntity old = this.GetEntity(entity.finbillid);
                    if (old != null)
                    {
                        throw new Exception("编号需要唯一");
                    }
                }

                service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 提交数据

        #region 验证数据

        public string modifyid(string finbillid)
        {
            return service.modifyid(finbillid);
        }

        public string CheckDel(string findid)
        {
            return service.CheckDel(findid);
        }

        #endregion 验证数据
    }
}