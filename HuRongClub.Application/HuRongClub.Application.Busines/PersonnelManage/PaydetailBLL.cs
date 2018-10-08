using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Application.Service.PersonnelManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace HuRongClub.Application.Busines.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-05-02 18:41
    /// 描 述：Paydetail
    /// </summary>
    public class PaydetailBLL
    {
        private PaydetailIService service = new PaydetailService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PaydetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PaydetailEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取datatable
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public DataTable GetPageListToTable(Pagination pagination, string queryJson, string keyValue)
        {
            DataTable rsule = new DataTable();
            rsule.Columns.Add(new DataColumn("Id"));
            rsule.Columns.Add(new DataColumn("empid"));
            rsule.Columns.Add(new DataColumn("empname"));
            rsule.Columns.Add(new DataColumn("deptname"));
            rsule.Columns.Add(new DataColumn("payrollid"));

            DataTable itemDT = service.GetPageListToTable(queryJson, keyValue);
            if (itemDT != null && itemDT.Rows.Count > 0)
            {
                #region 整合数据

                // 1. 获取所有薪资项
                string[] items = CommonHelper.GetNamesFromDataTable(itemDT, "dispName");
                for (int i = 0; i < items.Length; i++)
                {
                    rsule.Columns.Add(new DataColumn(items[i]));
                }

                // 2. 获取所有人员信息
                string[] userids = CommonHelper.GetNamesFromDataTable(itemDT, "empid");

                // 3. 遍历并初始化 DataTable
                foreach (string uid in userids)
                {
                    DataRow[] rows = itemDT.Select("empid='" + uid + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        DataRow row = rsule.NewRow();
                        row["Id"] = rows[0][0].ToString();
                        row["empid"] = rows[0][1].ToString();
                        row["empname"] = rows[0][2].ToString();
                        row["deptname"] = rows[0][3].ToString();
                        row["payrollid"] = rows[0][4].ToString();

                        for (int i = 0; i < items.Length; i++)
                        {
                            DataRow[] dispnames = itemDT.Select("empid='" + uid + "' and dispName = '" + items[i] + "'");
                            if (dispnames != null && dispnames.Length > 0)
                            {
                                row[items[i]] = dispnames[0]["amount"];
                            }
                        }

                        rsule.Rows.Add(row);
                    }
                }

                #endregion
            }
            if (pagination != null)
            {
                pagination.records = rsule.Rows.Count;
                return DataHelper.GetPagedTable(rsule, pagination.page, pagination.rows);
            }
            else
            {
                return rsule;
            }
            
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PaydetailEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(int keyValue, int payrollid)
        {
            try
            {
                service.Delete(keyValue, payrollid);
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
        public void SaveForm(string keyValue, PaydetailEntity entity)
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

        #endregion 提交数据
    }
}