using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using HuRongClub.Util.Offices;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-10 15:27
    /// 描 述：Room
    /// </summary>
    public class RoomBLL
    {
        private RoomIService service = new RoomService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RoomEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RoomEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoomEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取房屋类型下拉列表
        /// </summary>
        /// <param name="dictid">主表ID</param>
        /// <returns></returns>
        public IEnumerable<Entity.SysManage.DictitemEntity> GetListBySel(int dictid)
        {
            return service.GetListBySel(dictid);
        }

        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="room_id">房间编号</param>
        /// <param name="property_id">物业编号</param>
        /// <returns></returns>
        public IEnumerable<RoomModelEntity> GetInfo(string room_id, string property_id)
        {
            return service.GetInfo(room_id, property_id);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="building_id">所属楼栋编号</param>
        /// <param name="Type">1 全部 2空房间</param>
        /// <returns>返回列表</returns>
        public IEnumerable<RoomEntity> GetTreeList(string building_id,int Type)
        {
            return service.GetTreeList(building_id, Type);
        }
        /// <summary>
        /// 租赁单元
        /// </summary>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        public IEnumerable<RoomModelEntity> GetListByRent(string contractid)
        {
            return service.GetListByRent(contractid);
        }
        /// <summary>
        /// 获取批量费用导出数据
        /// </summary>
        /// <param name="Type">1 业主 2 租户</param>
        /// <param name="id">查询id</param>
        /// <returns></returns>
        public void GetListByFee(int Type, string id)
        {
            //取出数据源
            DataTable exportTable = service.GetListByFee(Type, id);
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = null;
            excelconfig.TitleFont = "";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "oid", ExcelColumn = "编码",Width=20 });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "rid", ExcelColumn = "单元代码", Width = 20 });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "room_name", ExcelColumn = "室号", Width = 20 });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "oname", ExcelColumn = "客户", Width = 20 });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "dim", ExcelColumn = "面积", Width = 20 });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fee_income", ExcelColumn = "金额", Width = 20 });
            //调用导出方法
            ExcelHelper.ExcelDownload(exportTable, excelconfig);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">owner_id</param>
        /// <returns></returns>
        public RoomEntity GetEntitys(string keyValue)
        {
            return service.GetEntitys(keyValue);
        }
        /// <summary>
        /// 获取空房间信息（用于下载批量导出数据）
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="building_id">归属楼栋</param>
        /// <returns></returns>
        public int GetUnitInfo(string property_id, string building_id)
        {
            //取出数据源
            DataTable exportTable = service.GetRoomInfo(property_id, building_id);
            if (exportTable != null && exportTable.Rows.Count > 0)
            {
                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = null;
                excelconfig.TitleFont = "";
                excelconfig.TitlePoint = 25;
                excelconfig.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                excelconfig.ColumnEntity = listColumnEntity;
                ColumnEntity columnentity = new ColumnEntity();
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "room_id", ExcelColumn = "编码", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "building_name", ExcelColumn = "所属楼栋", Width = 20 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "room_name", ExcelColumn = "房间名称", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "building_dim", ExcelColumn = "建筑面积", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "room_dim", ExcelColumn = "使用面积", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "room_number", ExcelColumn = "房间号", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "owner_name", ExcelColumn = "业主姓名", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "owner_tel", ExcelColumn = "业主电话", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "owner_cardtype", ExcelColumn = "业主证件名称", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "owner_cardno", ExcelColumn = "证件号码", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "in_date", ExcelColumn = "入住日期", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "sign_date", ExcelColumn = "登记日期", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "link1_name", ExcelColumn = "同住人", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "link1_tel", ExcelColumn = "同住人电话", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "link1_mark", ExcelColumn = "关系", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "link2_name", ExcelColumn = "同住人2", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "link2_tel", ExcelColumn = "同住人电话2", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "link2_mark", ExcelColumn = "关系2", Width = 10 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "remark", ExcelColumn = "备注", Width = 20 });
                //调用导出方法
                ExcelHelper.ExcelDownload(exportTable, excelconfig);

                return 1;
            }
            else
            {
                return 0;
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
        public void SaveForm(string keyValue, RoomEntity entity)
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
        /// 修改表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(RoomEntity entity)
        {
            try
            {
                service.SaveForm(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 出户操作
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="owner_id"></param>
        public void SaveOutFrom(string room_id, string owner_id)
        {
            try
            {
                service.SaveOutFrom(room_id, owner_id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}