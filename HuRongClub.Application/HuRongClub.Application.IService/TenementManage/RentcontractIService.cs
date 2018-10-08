using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 09:28
    /// 描 述：Rentcontract
    /// </summary>
    public interface RentcontractIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="property_id">物业编号</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<RentcontractEntity> GetPageList(Pagination pagination, string queryJson, string property_id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<RentcontractEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RentcontractEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取租赁单元房间号
        /// </summary>
        /// <param name="contractid">编号</param>
        /// <returns></returns>
        string GetRentcell(string contractid);

        /// <summary>
        /// 判断租赁单元房间号是否存在
        /// </summary>
        /// <param name="contractid">编号</param>
        /// <param name="rentcell">租赁单元房间号</param>
        /// <returns></returns>
        bool IsRentcell(string contractid, string rentcell);

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        IEnumerable<RentcontractTree> GetListBySel(string property_id, int status = 1);

        /// <summary>
        /// 获取前五条到期合同
        /// </summary>
        /// <returns></returns>
        IEnumerable<RentcontractEntity> GetExpireList(string property_id);

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);

        /// <summary>
        /// 删除房间号
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="room_id"></param>
        void RemoveDYForm(string keyValue, string room_id, string rentcell);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, RentcontractEntity entity);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="status">状态</param>
        void UpdateStatus(string keyValue, int status);

        /// <summary>
        /// 租凭单元
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="room_id">房屋编号</param>
        /// <param name="IsTrue">是否添加 1未 2 是</param>
        void UpdateRentcell(string keyValue, string room_id, int IsTrue, string rentcell);

        #endregion

        /// <summary>
        /// 租聘合同费用生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool SaveContractIncomeForm(string jsonParam);
    }
}