using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Application.Service.PersonnelManage;
using HuRongClub.Util;
using HuRongClub.Util.Offices;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Busines.PersonnelManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:40
    /// 描 述：Payroll
    /// </summary>
    public class PayrollBLL
    {
        private PayrollIService service = new PayrollService();

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PayrollEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<PayrollEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PayrollEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        


        #endregion 获取数据

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
        public void SaveForm(string keyValue, string inport, PayrollEntity entity)
        {
            try
            {
                string searchColumnName = "员工编号";
                string totalColumnName = "总计";

                if (inport != "")
                {
                    // 1.导入功能
                    DataTable dt = ExcelHelper.ExcelImport(Utils.GetMapPath(inport));

                    // 2.取出所有得薪资项
                    PayitemService payservice = new PayitemService();
                    IEnumerable<PayitemEntity> payitemlist = payservice.GetList(w=>w.disable == "1");

                    #region 3.取出所有的人员信息

                    List<string> employIds = new List<string>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (dt.Columns.Contains(searchColumnName))
                        {
                            employIds.Add(row[searchColumnName].ToString());
                        }
                    }

                    EmployinfoService employservice = new EmployinfoService();
                    IEnumerable<EmployinfoEntity> employlist = employservice.GetAllList(employIds);

                    #endregion

                    #region 4.获取excel中需要遍历的薪资项

                    List<string> payitems = new List<string>();
                    foreach (PayitemEntity item in payitemlist)
                    {
                        if (dt.Columns.Contains(item.dispName))
                        {
                            payitems.Add(item.dispName);
                        }
                    }

                    #endregion

                    PayrollService payrollService = new PayrollService();
                    int PayrollId = payrollService.FindMaxID() + 1;

                    int employCount = 0; // 本次导入人数
                    decimal TotalAmount = 0; // 本次应发总金额

                    #region 5.新增子表

                    List<PaydetailEntity> detailList = new List<PaydetailEntity>();
                    PaydetailEntity detailEntity = null;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        PaydetailService detailService = new PaydetailService();
                        foreach (DataRow row in dt.Rows)
                        {
                            int? empid = 0; // 员工编号

                            #region 获取员工信息
                            if (row[searchColumnName] != null && row[searchColumnName].ToString().Length != 0)
                            {
                                EmployinfoEntity employEntity = employlist.Where(w => w.empid == Convert.ToInt32(row[searchColumnName])).FirstOrDefault();
                                if (employEntity != null)
                                {
                                    empid = employEntity.empid;
                                    employCount++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            #endregion

                            #region 总计列不存在，或者 总计 非数字类型，不记入数据库
                            if (row[totalColumnName] == null || row[totalColumnName].ToString().Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                try
                                {
                                    TotalAmount += decimal.Round(Convert.ToDecimal(row[totalColumnName]), 2, MidpointRounding.AwayFromZero);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            #endregion

                            #region 遍历行中的项，一项存一条数据
                            foreach (string item in payitems)
                            {
                                detailEntity = new PaydetailEntity();
                                detailEntity.empid = empid;

                                PayitemEntity itemEntity = payitemlist.Where(w => w.dispName == item).FirstOrDefault();
                                if (itemEntity != null)
                                {
                                    detailEntity.itemcode = itemEntity.itemcode;
                                    detailEntity.amount = Convert.ToDecimal(row[itemEntity.dispName]);
                                }

                                detailEntity.payrollid = PayrollId;

                                detailEntity.CreatorName = Code.OperatorProvider.Provider.Current().UserName; ;
                                detailEntity.CreateDate = DateTime.Now;

                                detailList.Add(detailEntity);
                            }
                            #endregion
                        }
                    }

                    #endregion

                    #region 6.新增主表

                    // 组合主表字段
                    PayrollEntity parollEntity = new PayrollEntity();
                    parollEntity.PayrollId = PayrollId;
                    parollEntity.period = entity.period;
                    parollEntity.status = 0;
                    parollEntity.employnum = employCount;
                    parollEntity.Totalamount = decimal.Round(TotalAmount, 2, MidpointRounding.AwayFromZero);

                    parollEntity.CreatorId = 0;
                    parollEntity.CreatorName = Code.OperatorProvider.Provider.Current().UserName;
                    parollEntity.CreateDate = DateTime.Now;

                    #endregion

                    payrollService.SaveUploadData(parollEntity, detailList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">当前状态</param>
        /// <returns></returns>
        public void UpdateStatus(string keyValue, int status)
        {
            try
            {
                PayrollEntity entity = new PayrollEntity();
                if (status == 0)
                {
                    entity.status = 1;
                }
                service.SaveForm(keyValue, entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        } 

        #endregion 提交数据
    }
}