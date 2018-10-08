using HuRongClub.Application.Busines.PersonnelManage;
using HuRongClub.Application.Busines.SystemManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Offices;
using HuRongClub.Util.WebControl;
using System;
using System.Data;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.PersonnelManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:40
    /// 描 述：Payroll
    /// </summary>
    [HandlerOperateLog]
    public class PayrollController : MvcControllerBase
    {
        private PayrollBLL payrollbll = new PayrollBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PayrollIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PayrollForm()
        {
            return View();
        }

        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = payrollbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = payrollbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = payrollbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取薪资区间下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPeriodMedel()
        {
            DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
            // 后期改成根据 code 获取
            var data = dataItemDetailBLL.GetList("5cbb414b-826a-458f-92e5-659573c4ec5b");
            return Content(data.ToJson());
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        public ActionResult ExportCashData_New(string keyValue)
        {
            //获取数据
            PaydetailBLL paydetailbll = new PaydetailBLL();

            DataTable dt_list = paydetailbll.GetPageListToTable(null,null,keyValue);

            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "薪资明细";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new System.Collections.Generic.List<ColumnEntity>();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { ExcelColumn = "员工编号", Column = "empid", Width = 10, Alignment = "right" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { ExcelColumn = "姓名", Column = "empname", Width = 10 });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { ExcelColumn = "部门", Column = "deptname", Width = 14 });

            for (int i = 0; i < dt_list.Columns.Count; i++)
            {
                if (i > 4)
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { ExcelColumn = dt_list.Columns[i].ColumnName, Column = dt_list.Columns[i].ColumnName, Width = 10, Alignment = "right" });
            }

            ExcelHelper.ExcelDownload(dt_list, excelconfig);

            return Success("导出成功！");
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            payrollbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string inport, PayrollEntity entity)
        {
            payrollbll.SaveForm(keyValue, inport, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateStatus(string keyValue, int status)
        {
            if (status == 1)
            {
                return Success("已经审核过了！");
            }
            else
            {
                payrollbll.UpdateStatus(keyValue, status);
                return Success("审核成功！");
            }
        }

        #endregion 提交数据
    }
}