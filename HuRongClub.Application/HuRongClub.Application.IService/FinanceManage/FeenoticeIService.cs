using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.FinanceManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-05-08 16:21
    /// 描 述：进账认领
    /// </summary>
    public interface FeenoticeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeenoticeEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeenoticeEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeenoticeEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">参数</param>
        /// <returns></returns>
        IEnumerable<FeenoticeEntity> GetList(Pagination pagination, string queryJson);
        /// <summary>
        /// 账单编号不能重复
        /// </summary>
        /// <param name="accountcode">账单编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool Existaccountcode(string accountcode, string keyValue);
        /// <summary>
        /// 是否认领
        /// </summary>
        /// <param name="keyValue">账单编号</param>
        /// <returns></returns>
        bool ExistIsClaim(string keyValue);
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
        void SaveForm(string keyValue, FeenoticeEntity entity);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="list"></param>
        void ImportForm(List<FeenoticeEntity> list);
        #endregion
    }
}
