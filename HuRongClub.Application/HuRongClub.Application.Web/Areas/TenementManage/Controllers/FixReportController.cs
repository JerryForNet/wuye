using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;
using HuRongClub.Application.Code;
using System;
using HuRongClub.Util.Extension;
using HuRongClub.Application.Web.App_Start._01_Handler;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 15:13
    /// 描 述：报修维修
    /// </summary>
    [HandlerOperateLog]
    public class FixReportController : MvcControllerBase
    {
        private FixReportBLL fixreportbll = new FixReportBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FixReportIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FixReportForm()
        {
            ViewBag.propertyname= Utils.GetCookie("property_name");
            ViewBag.fixNumber_No= fixreportbll.fixNumber_No();
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
            string property_id = " ";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var watch = CommonHelper.TimerStart();
            var data = fixreportbll.GetPageList(pagination, property_id, queryJson);
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
            var data = fixreportbll.GetList(queryJson);
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
            var data = fixreportbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取维修材料列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFixList(string keyValue)
        {
            var data = fixreportbll.GetFixList(keyValue);
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
            fixreportbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, FixReportEntity entity)
        {
            entity.propertyid= Utils.GetCookie("property_id");
            entity.feetype = 1;
            entity.inputdate = DateTime.Now;
            if (!string.IsNullOrEmpty(OperatorProvider.Provider.Current().OldSystemUserID))
            {
                entity.userid = OperatorProvider.Provider.Current().OldSystemUserID.ToInt();
            }
            string FixReportID = fixreportbll.SaveForm(keyValue, entity);
            return Success("操作成功。", FixReportID);
        }

        /// <summary>
        /// 保存表单材料
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strEntity">实体对象</param>
        /// <param name="strChildEntitys">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SavesForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.Replace(" ", "").Replace("&nbsp;","").ToObject<FixReportEntity>();
            entity.FixReportID = keyValue;
            entity.propertyid = Utils.GetCookie("property_id");

            var entryList = strChildEntitys.ToList<FixmaterialEntity>();

            fixreportbll.SavesForm(entity, entryList);
            return Success("操作成功。");
        }

        #endregion
    }
}
