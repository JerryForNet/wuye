using HuRongClub.Application.Entity.OaManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace HuRongClub.Application.IService.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-28 20:16
    /// 描 述：通知公告
    /// </summary>
    public interface NoticeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<NoticeEntity> GetPageList(Pagination pagination, string queryJson);

        string Checkmanager(string username);

        IEnumerable<NoticeEntity> GetTop5();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<NoticeEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        NoticeEntity GetEntity(int keyValue);

        #endregion 获取数据

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
        void SaveForm(string keyValue, NoticeEntity entity);

        #endregion 提交数据
    }
}