using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 09:43
    /// 描 述：Otherincome
    /// </summary>
    public interface OtherincomeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<OtherincomeEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<OtherincomeEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        OtherincomeEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取 缴费查询 列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<OtherincomeListEntity> GetList(Pagination pagination, string property_id, string queryJson);
        /// <summary>
        /// 获取业主缴费查询明细实体
        /// </summary>
        /// <param name="receive_id">实收编号</param>
        /// <param name="type">0 业主 1租户</param>
        /// <returns></returns>
        OtherincomeFromEntity.OtherFeereceiveFromEntity GetEntitys(string receive_id,int type);
        /// <summary>
        /// 获取应收收入明细
        /// </summary>
        /// <param name="receive_id">实收编号</param>
        /// <param name="type">0 业主 1租户</param>
        /// <returns></returns>
        IEnumerable<OtherincomeFromEntity.OtherFeereceiveListEntity> GetListByOther(string receive_id, int type);
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="ticket_id">发票编号</param>
        /// <returns></returns>
        string Getincomeid(string ticket_id);
        /// <summary>
        /// 获取其他收入列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">参数</param>
        /// <returns></returns>
        IEnumerable<OtherincomeitemListEntity> GetOtherList(Pagination pagination, string property_id, string queryJson);
        /// <summary>
        /// 获取其他收入明细列表
        /// </summary>
        /// <param name="incomeid"></param>
        /// <returns></returns>
        IEnumerable<OtherincomeitemEntity> GetOtherDetailList(string incomeid);
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
        void SaveForm(string keyValue, OtherincomeEntity entity);
        /// <summary>
        /// 发票作废
        /// </summary>
        /// <param name="lastoperate">操作人</param>
        /// <param name="ticket_id">编号</param>
        void ToVoidForm(string lastoperate, string ticket_id);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="entryList">子实体对象</param>
        /// <returns></returns>
        string SavesForm(string keyValue, OtherincomeEntity entity, List<OtherincomeitemEntity> entryList);
        #endregion
    }
}
