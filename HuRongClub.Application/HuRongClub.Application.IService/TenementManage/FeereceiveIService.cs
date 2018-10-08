using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 10:39
    /// 描 述：Feereceive
    /// </summary>
    public interface FeereceiveIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeereceiveEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeereceiveEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeereceiveEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="pos">位数 不够前面补0</param>
        /// <returns></returns>
        string GetMaxID(int pos);
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="ticket_id">发票编号</param>
        /// <returns></returns>
        string Getreceive_id(string ticket_id);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, FeereceiveEntity entity);
        /// <summary>
        /// 发票作废
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="ticketid"></param>
        /// <param name="operate"></param>
        /// <returns></returns>
        int ToVoidForm(int flag, string ticketid, string operate);
        #endregion
    }
}
