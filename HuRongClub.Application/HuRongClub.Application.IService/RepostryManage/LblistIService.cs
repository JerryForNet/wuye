using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-06-20 10:32
    /// 描 述：Lblist
    /// </summary>
    public interface LblistIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<LblistEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<LblistEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        LblistEntity GetEntity(int keyValue);
        /// <summary>
        /// 根据品种编号和人员编号查询数据
        /// </summary>
        /// <param name="dictitemid">品种编号</param>
        /// <param name="empid">人员编号</param>
        /// <returns></returns>
        IEnumerable<LblistEntity> GetList(int dictitemid, int empid);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(int keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, LblistEntity entity);
        /// <summary>
        /// 修改isnew
        /// </summary>
        /// <param name="dictitemid">品种编号</param>
        /// <param name="empid">人员编号</param>
        void UpdateByisnew(int dictitemid, int empid);
        #endregion
    }
}
