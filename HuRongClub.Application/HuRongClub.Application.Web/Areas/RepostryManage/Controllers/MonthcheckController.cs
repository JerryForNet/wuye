using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-27 14:25
    /// 描 述：月结账
    /// </summary>
    [HandlerOperateLog]
    public class MonthcheckController : MvcControllerBase
    {
        private MonthcheckBLL monthcheckbll = new MonthcheckBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 打印界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Print()
        {
            return View();
        }

        /// <summary>
        /// 月库存明细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MonthDetailList()
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
            var data = monthcheckbll.GetPageList(pagination, queryJson);
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
            var data = monthcheckbll.GetList(queryJson);
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
            var data = monthcheckbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取月库存明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMonthDetailListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = monthcheckbll.GetMonthDetailListJson(pagination, queryJson);
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

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 月结账操作
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult OKMonth(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson))
            {
                return Error("请选择结算月份！");
            }
            var queryParam = queryJson.ToJObject();
            if (queryParam["Statrmonths"].IsEmpty() || queryParam["Eedmonths"].IsEmpty())
            {
                return Error("请选择结算月份！");
            }
            else
            {
                DateTime time = new DateTime(queryParam["Statrmonths"].ToInt(), queryParam["Eedmonths"].ToInt(), 10).AddMonths(-1);
                if (!monthcheckbll.ExistsLastMonth(time.Year, time.Month))
                {
                    return Error("对不起，您上个月帐尚未结算，请先进行上月结算操作！！");
                }
                else
                {
                    monthcheckbll.OKMonth(queryJson);
                    return Success("已结算成功。");
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //  [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            monthcheckbll.RemoveForm(keyValue);
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
        //  [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, MonthcheckEntity entity)
        {
            monthcheckbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}