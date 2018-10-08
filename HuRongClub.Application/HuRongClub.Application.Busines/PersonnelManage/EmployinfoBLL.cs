using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.IService.PersonnelManage;
using HuRongClub.Application.Service.PersonnelManage;
using HuRongClub.Cache.Factory;
using HuRongClub.Util.Offices;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HuRongClub.Application.Busines.PersonnelManage
{
    /// <summary>
    /// 版 本 6.1

    /// 创 建：王菲
    /// 日 期：2017-04-01 09:43
    /// 描 述：员工档案
    /// </summary>
    public class EmployinfoBLL
    {
        private EmployinfoIService service = new EmployinfoService();

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "EmployinfoCache";

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<EmployinfoEntity> GetList()
        {
            return service.GetList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EmployinfoListEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 列表(ALL)
        /// </summary>
        /// <returns></returns>
        public List<EmployinfoEntity> GetAllList()
        {
            return service.GetAllList().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmployinfoEntity GetEntity(int keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 员工档案导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="field">导出字段</param>
        /// <returns></returns>
        public int EmployinfoExport(string queryJson, string field)
        {
            //取出数据源
            DataTable exportTable = service.GetListInfo(queryJson);
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

                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "empid", ExcelColumn = "编号", Width = 10 });
                if (!string.IsNullOrEmpty(field))
                {
                    string[] fields = field.Split(',');
                    #region 字段判断
                    foreach (string item in fields)
                    {
                        switch (item)
                        {
                            case "empcode":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "empcode", ExcelColumn = "工号", Width = 10 });
                                break;
                            case "empname":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "empname", ExcelColumn = "姓名", Width = 15 });                                
                                break;
                            case "sex":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "sex", ExcelColumn = "性别", Width = 10 });
                                break;
                            case "deptname":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "deptname", ExcelColumn = "部门", Width = 20 });
                                break;
                            case "indate":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "indate", ExcelColumn = "进公司时间", Width = 15 });
                                break;
                            case "hiredate":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "hiredate", ExcelColumn = "合同开始日期", Width = 15 });
                                break;
                            case "firedate":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "firedate", ExcelColumn = "合同结束日期", Width = 15 });
                                break;
                            case "address":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "address", ExcelColumn = "家庭地址", Width = 20 });
                                break;
                            case "postcode":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "postcode", ExcelColumn = "邮编", Width = 10 });
                                break;
                            case "homephone":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "homephone", ExcelColumn = "家庭电话", Width = 15 });
                                break;
                            case "mobilephone":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "mobilephone", ExcelColumn = "手机", Width = 15 });
                                break;
                            case "identityno":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "identityno", ExcelColumn = "身份证号", Width = 20 });
                                break;
                            case "birthday":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "birthday", ExcelColumn = "出身日期", Width = 15 });
                                break;
                            case "p_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "p_name", ExcelColumn = "政治面貌", Width = 10 });
                                break;
                            case "c_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "c_name", ExcelColumn = "职务", Width = 10 });
                                break;
                            case "d_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "d_name", ExcelColumn = "学历", Width = 10 });
                                break;
                            case "du_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "du_name", ExcelColumn = "职称", Width = 10 });
                                break;
                            case "t_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "t_name", ExcelColumn = "技术等级", Width = 10 });
                                break;
                            case "j_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "j_name", ExcelColumn = "岗位", Width = 10 });
                                break;
                            case "ca_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ca_name", ExcelColumn = "职级", Width = 10 });
                                break;
                            case "co_name":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "co_name", ExcelColumn = "用工来源", Width = 10 });
                                break;
                            case "contracttype":
                                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "contracttype", ExcelColumn = "用工性质", Width = 10 });
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 全部
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "empcode", ExcelColumn = "工号", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "empname", ExcelColumn = "姓名", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "sex", ExcelColumn = "性别", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "deptname", ExcelColumn = "部门", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "indate", ExcelColumn = "进公司时间", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "hiredate", ExcelColumn = "合同开始日期", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "firedate", ExcelColumn = "合同结束日期", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "address", ExcelColumn = "家庭地址", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "postcode", ExcelColumn = "邮编", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "homephone", ExcelColumn = "家庭电话", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "mobilephone", ExcelColumn = "手机", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "identityno", ExcelColumn = "身份证号", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "birthday", ExcelColumn = "出身日期", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "p_name", ExcelColumn = "政治面貌", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "c_name", ExcelColumn = "职务", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "d_name", ExcelColumn = "学历", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "du_name", ExcelColumn = "职称", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "t_name", ExcelColumn = "技术等级", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "j_name", ExcelColumn = "岗位", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ca_name", ExcelColumn = "职级", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "co_name", ExcelColumn = "用工来源", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "contracttype", ExcelColumn = "用工性质", Width = 10 }); 
                    #endregion
                }

                
                //调用导出方法
                ExcelHelper.ExcelDownload(exportTable, excelconfig);

                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="deptid">部门编号</param>
        /// <returns></returns>
        public IEnumerable<EmployinfoEntity> GetList(int deptid)
        {
            return service.GetList(deptid);
        }
        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-离职；0-复职</param>
        public void UpdateState(string keyValue, Int16 State, DateTime? fireoutdate)
        {
            try
            {
                service.UpdateState(keyValue, State, fireoutdate);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

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
        public void SaveForm(string keyValue, EmployinfoEntity entity)
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
        /// 合同续费
        /// </summary>
        /// <param name="keyValue">续费员工编号</param>
        /// <param name="firedate">续费时间</param>
        public void ExpireFrom(string keyValue, DateTime? firedate)
        {
            try
            {
                service.ExpireFrom(keyValue, firedate);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion 提交数据
    }
}