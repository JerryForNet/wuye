using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using HuRongClub.Application.Code;
using HuRongClub.Application.Web.App_Start._01_Handler;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 11:58
    /// 描 述：DevicePart
    /// </summary>
    [HandlerOperateLog]
    public class DevicePartController : MvcControllerBase
    {
        private DevicePartBLL devicepartbll = new DevicePartBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DevicePartIndex()
        {
            return View();
        }
        /// <summary>
        /// DeviceCopy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DeviceCopy()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DevicePartForm()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DevicePartAdd()
        {
            return View();
        }
        #endregion

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
            var data = devicepartbll.GetPageList(pagination, queryJson);
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
            var data = devicepartbll.GetList(queryJson);
            return ToJsonResult(data);
        }
      /// <summary>
      /// 获取年份
      /// </summary>
      /// <returns></returns>
        [HttpGet]
        public ActionResult GetYearListJson(int startyear,int year)
        {
            List<YearModel> ylist = new List<YearModel>();
            for (int i = startyear; i <= (DateTime.Now.Year + year); i++)
            {
                YearModel model = new YearModel();
                model.ytext = i.ToString();
                model.yvalue = i.ToString();
                ylist.Add(model);
            }
            return ToJsonResult(ylist);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = devicepartbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

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
            devicepartbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, DevicePartEntity entity)
        {
            devicepartbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult CopyDevicePlan(int fromyear,int toyear)
        {
            devicepartbll.CopyDevicePlan(fromyear, toyear);
            return Success("操作成功。");
        }
        #endregion
    }
}
