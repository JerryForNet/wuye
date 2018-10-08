using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System;
using HuRongClub.Util;
using System.Data;
using HuRongClub.Util.Extension;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：超级管理员
    /// 日 期：2017-04-01 10:57
    /// 描 述：Owner
    /// </summary>
    public class OwnerBLL
    {
        private OwnerIService service = new OwnerService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OwnerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OwnerEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OwnerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 单元业主信息
        /// </summary>
        /// <param name="room_id">房间编号</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="type">1按room_id 查询 2 按owner_id 查询</param>
        /// <returns></returns>
        public IEnumerable<OwnerModelEntity> GetInfo(string room_id, string property_id, int type)
        {
            return service.GetInfo(room_id, property_id, type);
        }
        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        public IEnumerable<OwnerListEntity> GetInfoList(string property_id, string queryJson, Pagination pagination)
        {
            return service.GetInfoList(property_id, queryJson, pagination);
        }
        /// <summary>
        /// 业主下拉
        /// </summary>
        /// <param name="building_id"></param>
        /// <returns></returns>
        public IEnumerable<OwnerEntity> GetListBySel(string building_id)
        {
            return service.GetListBySel(building_id);
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
        public string SaveForm(string keyValue, OwnerEntity entity)
        {
            try
            {
                return service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 修改业主姓名
        /// </summary>
        /// <param name="owner_id">业主编号</param>
        /// <param name="owner_name">业主姓名</param>
        public void UpdateOwnerName(string owner_id, string owner_name)
        {
            service.UpdateOwnerName(owner_id, owner_name);
        }
        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="building_id">归属楼栋</param>
        /// <param name="file">附件</param>
        /// <returns></returns>
        public string BatchFrom(string property_id, string building_id, string file)
        {
            string ret = "0";
            if (!string.IsNullOrEmpty(file))
            {
                DataTable dt = Util.Offices.ExcelHelper.ExcelImport(Utils.GetMapPath(file));
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("编码") && dt.Columns.Contains("业主姓名") && dt.Columns.Contains("入住日期"))
                    {
                        RoomIService dal = new RoomService();
                        DataTable exportTable = dal.GetRoomInfo(property_id, building_id);
                        if (exportTable != null && exportTable.Rows.Count > 0)
                        {
                            List<OwnerEntity> list = new List<OwnerEntity>();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (!dt.Rows[i]["编码"].IsEmpty() && !dt.Rows[i]["业主姓名"].IsEmpty() && !dt.Rows[i]["入住日期"].IsEmpty())
                                {
                                    //判断上传数据在空房间内
                                    DataRow[] dr_c = exportTable.Select("room_id='" + dt.Rows[i]["编码"].ToString().Trim() + "'");
                                    if (dr_c.Length > 0)
                                    {
                                        #region 赋值

                                        OwnerEntity ent = new OwnerEntity();
                                        ent.sign_userid = Code.OperatorProvider.Provider.Current().UserName;
                                        ent.property_id = property_id;
                                        ent.room_id = dt.Rows[i]["编码"].ToString();
                                        ent.owner_name = dt.Rows[i]["业主姓名"].ToString();
                                        ent.in_date = dt.Rows[i]["入住日期"].ToDateOrNullToNow();
                                        if (dt.Columns.Contains("登记日期"))
                                        {
                                            ent.sign_date = dt.Rows[i]["登记日期"].ToDateOrNullToNow();
                                        }
                                        if (dt.Columns.Contains("业主电话"))
                                        {
                                            ent.owner_tel = dt.Rows[i]["业主电话"].ToString();
                                        }
                                        if (dt.Columns.Contains("业主证件名称"))
                                        {
                                            ent.owner_cardtype = dt.Rows[i]["业主证件名称"].ToString();
                                        }
                                        if (dt.Columns.Contains("证件号码"))
                                        {
                                            ent.owner_cardno = dt.Rows[i]["证件号码"].ToString();
                                        }
                                        if (dt.Columns.Contains("同住人"))
                                        {
                                            ent.link1_name = dt.Rows[i]["同住人"].ToString();
                                        }
                                        if (dt.Columns.Contains("同住人电话"))
                                        {
                                            ent.link1_tel = dt.Rows[i]["同住人电话"].ToString();
                                        }
                                        if (dt.Columns.Contains("关系"))
                                        {
                                            ent.link1_mark = dt.Rows[i]["关系"].ToString();
                                        }
                                        if (dt.Columns.Contains("同住人2"))
                                        {
                                            ent.link2_name = dt.Rows[i]["同住人2"].ToString();
                                        }
                                        if (dt.Columns.Contains("同住人电话2"))
                                        {
                                            ent.link2_tel = dt.Rows[i]["同住人电话2"].ToString();
                                        }
                                        if (dt.Columns.Contains("关系2"))
                                        {
                                            ent.link2_mark = dt.Rows[i]["关系2"].ToString();
                                        }
                                        if (dt.Columns.Contains("备注"))
                                        {
                                            ent.remark = dt.Rows[i]["备注"].ToString();
                                        }

                                        list.Add(ent);
                                        #endregion
                                    }
                                }
                            }
                            if (list.Count > 0)
                            {
                                try
                                {
                                    service.BatchFrom(list);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            else
                            {
                                ret = "请上传填写有效的批量进户Excel文件！";
                            }
                        }
                        else
                        {
                            ret = "未找到该楼栋下空房间,不能进行进户操作！";
                        }
                    }
                    else
                    {
                        ret = "请按照下载模版填写批量进户Excel文件！";
                    }
                }
                else
                {
                    ret = "请上传有效的批量进户Excel文件！";
                }
            }
            else
            {
                ret = "请上传批量进户Excel文件！";
            }

            return ret;
        }
        #endregion
    }
}
