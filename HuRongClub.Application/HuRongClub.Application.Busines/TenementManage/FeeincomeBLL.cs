using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.IService.TenementManage;
using HuRongClub.Application.Service.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.Offices;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Busines.TenementManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-05 10:36
    /// 描 述：Feeincome
    /// </summary>
    public class FeeincomeBLL
    {
        private FeeincomeIService service = new FeeincomeService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeincomeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeincomeEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeeincomeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="owner_id">业主编号</param>
        /// <param name="property_id">物业名称</param>
        /// <param name="type">1已缴费 2欠缴费</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeeincomeListEntity> GetPageList(Pagination pagination, string owner_id, string property_id, int type)
        {
            return service.GetPageList(pagination, owner_id, property_id, type);
        }

        /// <summary>
        /// 获取列表主键集
        /// </summary>
        /// <param name="keyValues">主键集</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeeincomeEntity> GetLists(string keyValues)
        {
            return service.GetLists(keyValues);
        }

        /// <summary>
        /// 获取租户缴费情况
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="contractid">租赁合同ID</param>
        /// <returns></returns>
        public IEnumerable<FeeincomeListEntity> GetPageLists(Pagination pagination, string contractid)
        {
            return service.GetPageLists(pagination, contractid);
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="feeitem_id">费用科目ID</param>
        /// <param name="room_id">房间编号/租赁合同编号</param>
        /// <param name="fee_year">应收日期年</param>
        /// <param name="fee_month">应收日期月</param>
        /// <param name="type">1业主 2租户</param>
        /// <returns></returns>
        public IEnumerable<FeeincomeEntity> GetList(string feeitem_id, string room_id, int fee_year, int fee_month, int type)
        {
            return service.GetList(feeitem_id, room_id, fee_year, fee_month, type);
        }

        /// <summary>
        /// 实收查询列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="Mode">1实收 2 应收</param>
        /// <returns></returns>
        public IEnumerable<ActualEntity> GetActualList(Pagination pagination, string property_id, string queryJson, int Mode)
        {
            try
            {
                DataTable dt = new DataTable();
                if (Mode == 2)
                {
                    dt = service.GetActualList(pagination, property_id, queryJson);
                }
                else
                {
                    dt = service.GetReceiveList(pagination, property_id, queryJson);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    return DataHelper.DataTableToIList<ActualEntity>(dt);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }

        /// <summary>
        /// 是否欠费
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="owner_id"></param>
        /// <param name="Type">1业主 2合同</param>
        /// <returns></returns>
        public int IsQianFei(string room_id, string owner_id, int Type)
        {
            return service.IsQianFei(room_id, owner_id, Type);
        }

        /// <summary>
        /// 获取小台帐
        /// </summary>
        /// <param name="property_id">物业编号</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public List<AccountTZEntity> GetAccountList(string property_id, string queryJson)
        {
            IEnumerable<AccountTZEntity> list = service.GetAccountList(property_id, queryJson);
            if (list != null && list.Count() > 0)
            {
                //物业单元  住户
                DataTable dt = new DataTable();
                dt.Columns.Add("building_name", typeof(string));
                dt.Columns.Add("owner_name", typeof(string));
                for (int i = 1; i < 13; i++)
                {
                    dt.Columns.Add("fee_income_" + i, typeof(string));
                    dt.Columns.Add("fee_already_" + i, typeof(string));
                }

                var list_gb = from acc in list
                              group acc by new { acc.room_id, acc.building_name, acc.room_name, acc.owner_name } into gb
                              select new
                              {
                                  room_id = gb.Key.room_id,
                                  building_name = gb.Key.building_name,
                                  room_name = gb.Key.room_name,
                                  owner_name = gb.Key.owner_name
                              };
                foreach (var item in list_gb)
                {
                    DataRow dr = dt.NewRow();
                    dr["building_name"] = item.building_name + "/" + item.room_name;
                    dr["owner_name"] = item.owner_name;
                    for (int i = 1; i < 13; i++)
                    {
                        if (!string.IsNullOrEmpty(item.room_id))
                        {
                            IEnumerable<AccountTZEntity> list_a = list.Where(t => t.fee_month == i && t.room_id == item.room_id);
                            if (list_a != null && list_a.Count() > 0)
                            {
                                dr["fee_income_" + i] = string.Format("{0:0.##}", list_a.First().fee_income);
                                dr["fee_already_" + i] = string.Format("{0:0.##}", list_a.First().fee_already);
                            }
                            else
                            {
                                dr["fee_income_" + i] = 0;
                                dr["fee_already_" + i] = 0;
                            }
                        }
                        else
                        {
                            dr["fee_income_" + i] = 0;
                            dr["fee_already_" + i] = 0;
                        }
                    }
                    dt.Rows.Add(dr);
                }

                if (dt.Rows.Count > 0)
                {
                    return DataHelper.DataTableToIList<AccountTZEntity>(dt).ToList();
                }
            }

            return null;
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="list">日志实体集合</param>
        public void RemoveForm(string keyValue, List<FeechangelogEntity> list)
        {
            try
            {
                service.RemoveForm(keyValue, list);
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
        public void SaveForm(string keyValue, FeeincomeEntity entity)
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
        /// 生成费用
        /// </summary>
        /// <param name="roomid">单元编号</param>
        /// <param name="enddate">时间</param>
        /// <param name="type">1业主 2租户</param>
        public void Generate(string roomid, DateTime enddate, int type)
        {
            try
            {
                service.Generate(roomid, enddate, type);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 调整
        /// </summary>
        /// <param name="list">主键值</param>
        /// <param name="list_f">日志实体对象</param>
        /// <param name="list_c">费用减免记录实体对象</param>
        /// <returns></returns>
        public void SavesForm(List<FeeincomeEntity> list, List<FeechangelogEntity> list_f, List<FeeincomeCutEntity> list_c)
        {
            try
            {
                service.SavesForm(list, list_f, list_c);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 缴费确定
        /// </summary>
        /// <param name="list">费用应收实体对象</param>
        /// <param name="ent_ft">发票领用实体对象</param>
        /// <param name="list_fe">费用实收实体对象</param>
        /// <param name="list_fK">收费核销实体对象</param>
        public void FixedCost(List<FeeincomeEntity> list, FeeticketEntity ent_ft, List<FeereceiveEntity> list_fe, List<FeecheckEntity> list_fK)
        {
            try
            {
                service.FixedCost(list, ent_ft, list_fe, list_fK);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 导入费用
        /// </summary>
        /// <param name="list"></param>
        public void FeeManage(List<FeeincomeEntity> list)
        {
            try
            {
                service.FeeManage(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 处理数据

        /// <summary>
        /// 导出应收、实收 列表
        /// </summary>
        /// <returns></returns>
        public void GetExportList(string property_id, string queryJson, int mode)
        {
            //取出数据源
            IEnumerable<ActualEntity> list = GetActualList(null, property_id, queryJson, mode);
            DataTable exportTable = DataHelper.ListToDataTable<ActualEntity>((List<ActualEntity>)list);

            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.IsAllSizeColumn = true;
            excelconfig.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";

            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "owner_name", ExcelColumn = "业主/客户" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "feeitem", ExcelColumn = "费用科目" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "incomedate", ExcelColumn = "计费年月" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "pay_enddate", ExcelColumn = "计费日期" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fee_date", ExcelColumn = "实收日期" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "fee_income", ExcelColumn = "收费金额" });

            //调用导出方法
            ExcelHelper.ExcelDownload(exportTable, excelconfig);
        }

        #endregion 处理数据
    }
}