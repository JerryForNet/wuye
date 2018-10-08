using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuRongClub.Application.IService.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:36
    /// 描 述：Feeincome
    /// </summary>
    public interface FeeincomeIService
    {
        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeeincomeEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeincomeEntity> GetList(string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FeeincomeEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="owner_id">业主编号</param>
        /// <param name="property_id">物业名称</param>
        /// <param name="type">1已缴费 2欠缴费</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<FeeincomeListEntity> GetPageList(Pagination pagination, string owner_id, string property_id, int type);

        /// <summary>
        /// 获取列表主键集
        /// </summary>
        /// <param name="keyValues">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<FeeincomeEntity> GetLists(string keyValues);

        /// <summary>
        /// 获取租户缴费情况
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        IEnumerable<FeeincomeListEntity> GetPageLists(Pagination pagination, string contractid);

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="feeitem_id">费用科目ID</param>
        /// <param name="room_id">房间编号/租赁合同编号</param>
        /// <param name="fee_year">应收日期年</param>
        /// <param name="fee_month">应收日期月</param>
        /// <param name="type">1业主 2租户</param>
        /// <returns></returns>
        IEnumerable<FeeincomeEntity> GetList(string feeitem_id, string room_id, int fee_year, int fee_month, int type);

        /// <summary>
        /// 实收查询列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="Mode">1实收 2 应收</param>
        /// <returns></returns>
        IEnumerable<ActualEntity> GetActualList(Pagination pagination, string property_id, string queryJson, int Mode);

        /// <summary>
        /// 应收查询不分页
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="building_id"></param>
        /// <param name="owner_id"></param>
        /// <param name="feeitem_id"></param>
        /// <param name="stateY"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        DataTable GetActualList(Pagination pagination, string property_id, string queryJson);

        /// <summary>
        /// 实收查询
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetReceiveList(Pagination pagination, string property_id, string queryJson);

        /// <summary>
        /// 是否欠费
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="owner_id"></param>
        /// <param name="Type">1业主 2合同</param>
        /// <returns></returns>
        int IsQianFei(string room_id, string owner_id, int Type);

        /// <summary>
        /// 获取小台帐
        /// </summary>
        /// <param name="property_id"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<AccountTZEntity> GetAccountList(string property_id, string queryJson);

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="list">日志实体集合</param>
        void RemoveForm(string keyValue, List<FeechangelogEntity> list);

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, FeeincomeEntity entity);

        /// <summary>
        /// 生成费用
        /// </summary>
        /// <param name="roomid">单元编号</param>
        /// <param name="enddate">时间</param>
        /// <param name="type">1业主 2租户</param>
        void Generate(string roomid, DateTime enddate, int type);

        /// <summary>
        /// 调整
        /// </summary>
        /// <param name="list">主键值</param>
        /// <param name="list_f">日志实体对象</param>
        /// <param name="list_c">费用减免记录实体对象</param>
        /// <returns></returns>
        void SavesForm(List<FeeincomeEntity> list, List<FeechangelogEntity> list_f, List<FeeincomeCutEntity> list_c);

        /// <summary>
        /// 缴费确定
        /// </summary>
        /// <param name="list">费用应收实体对象</param>
        /// <param name="ent_ft">发票领用实体对象</param>
        /// <param name="list_fe">费用实收实体对象</param>
        /// <param name="list_fK">收费核销实体对象</param>
        void FixedCost(List<FeeincomeEntity> list, FeeticketEntity ent_ft, List<FeereceiveEntity> list_fe, List<FeecheckEntity> list_fK);

        /// <summary>
        /// 导入费用
        /// </summary>
        /// <param name="list"></param>
        void FeeManage(List<FeeincomeEntity> list);

        #endregion
    }
}