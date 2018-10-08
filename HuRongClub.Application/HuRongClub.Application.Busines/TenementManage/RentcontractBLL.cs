using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 09:28
    /// 描 述：Rentcontract
    /// </summary>
    public class RentcontractBLL
    {
        private RentcontractIService service = new RentcontractService();
        private RoomIService roomService = new RoomService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="property_id">物业编号</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RentcontractEntity> GetPageList(Pagination pagination, string queryJson, string property_id)
        {
            return service.GetPageList(pagination, queryJson, property_id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RentcontractEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RentcontractEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 判断租赁单元房间号是否存在
        /// </summary>
        /// <param name="contractid">编号</param>
        /// <param name="rentcell">租赁单元房间号</param>
        /// <returns></returns>
        public bool IsRentcell(string contractid, string rentcell)
        {
            //RoomEntity room = roomService.GetEntity(rentcell);
            //if (room != null && room.is_rent == 1)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return service.IsRentcell(contractid, rentcell);
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<RentcontractTree> GetListBySel(string property_id,int status)
        {
            return service.GetListBySel(property_id, status);
        }

        /// <summary>
        /// 获取前五条到期合同
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RentcontractEntity> GetExpireList(string property_id)
        {
            try
            {
                return service.GetExpireList(property_id);
            }
            catch (Exception)
            {
                throw;
            }
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
                // 释放 房间状态
                RentcontractEntity rentcontract = service.GetEntity(keyValue);
                if (rentcontract != null && !string.IsNullOrEmpty(rentcontract.rentcell) && rentcontract.status == 1)
                {
                    roomService.UpdateRent(rentcontract.rentcell, 0);
                }

                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除房间号
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="room_id"></param>
        public void RemoveDYForm(string keyValue, string room_id, string rentcell)
        {
            try
            {
                // 释放房间状态
                if (!string.IsNullOrEmpty(keyValue))
                {
                    RentcontractEntity contractEntity = service.GetEntity(keyValue);
                    if (contractEntity != null && contractEntity.status == 1)
                    {
                        roomService.UpdateRent(room_id, 0);
                    }
                }

                // 更新房间
                service.RemoveDYForm(keyValue, room_id, rentcell);
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
        public void SaveForm(string keyValue, RentcontractEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);

                // 锁定房间状态
                if (!string.IsNullOrEmpty(keyValue)) 
                {
                    RentcontractEntity contractEntity = service.GetEntity(keyValue);
                    if (contractEntity != null && contractEntity.status  == 1)
                    {
                        roomService.UpdateRent(contractEntity.rentcell, 1);
                    }
                    else
                    {
                        roomService.UpdateRent(contractEntity.rentcell, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="status">状态</param>
        public void UpdateStatus(string keyValue, int status)
        {
            try
            {
                // 终止合同要将对应的房号状态释放出来
                if (status != 1 && !string.IsNullOrEmpty(keyValue))
                {
                    RentcontractEntity rentcontract = service.GetEntity(keyValue);
                    if (rentcontract != null && !string.IsNullOrEmpty(rentcontract.rentcell))
                    {
                        roomService.UpdateRent(rentcontract.rentcell, 0);
                    }
                }

                service.UpdateStatus(keyValue, status);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 租凭单元
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <param name="room_id">房屋编号</param>
        /// <param name="IsTrue">是否添加 1未 2 是</param>
        public void UpdateRentcell(string keyValue, string room_id, int IsTrue, string rentcell)
        {
            try
            {
                // 锁定 房间状态
                if (!string.IsNullOrEmpty(keyValue)) 
                {
                    RentcontractEntity rentcontract = GetEntity(keyValue);
                    if (rentcontract != null && rentcontract.status == 1)
                    {
                        string roomids = string.Empty;
                        if (IsTrue == 1)
                        {
                            roomids = rentcell;
                        }
                        else
                        {

                            roomids = string.IsNullOrEmpty(rentcell) ? room_id : rentcell += "," + room_id;
                        }

                        roomService.UpdateRent(roomids, 1);
                    }
                }

                service.UpdateRentcell(keyValue, room_id, IsTrue, rentcell);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// 租聘合同费用生成
        /// </summary>
        /// <param name="data"></param>
        public bool SaveContractIncomeForm(string jsonParam)
        {
            Thread th = new Thread(SaveContractIncomeFormThread);
            th.Start(jsonParam);

            Thread.Sleep(3000);

            return true;
        }

        /// <summary>
        /// 租聘合同费用生成的线程
        /// </summary>
        /// <param name="data"></param>
        public void SaveContractIncomeFormThread(object data)
        {
            if (data != null)
            {
                try
                {
                    service.SaveContractIncomeForm(data.ToString());
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}