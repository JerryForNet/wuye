using HuRongClub.Application.Busines.OaManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.OaManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.OaManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-28 20:16
    /// 描 述：通知公告
    /// </summary>
    [HandlerOperateLog]
    public class NoticeController : MvcControllerBase
    {
        private NoticeBLL noticebll = new NoticeBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            var UserName = OperatorProvider.Provider.Current().UserName;
            var mananger = Checkmanager(UserName);
            if (mananger != null || UserName == "超级管理员")
            {
                ViewBag.Checkmanger = "是管理员";
            }
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 显示详情
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult Detail(int keyValue)
        {
            NoticeEntity data = noticebll.GetEntity(keyValue);

            return View(data);
        }

        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 是否有管理身份
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Checkmanager(string username)
        {
            string check = noticebll.Checkmanager(username);
            if (check != null && check != "")
            {
                return check;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult GetTop5()
        {
            var data = noticebll.GetTop5();
            return ToJsonResult(data);
        }

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
            var data = noticebll.GetPageList(pagination, queryJson);
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
            var data = noticebll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = noticebll.GetEntity(keyValue);
            return ToJsonResult(data);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(int keyValue)
        {
            noticebll.RemoveForm(keyValue);
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
        //    [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, NoticeEntity entity)
        {
            entity.author = OperatorProvider.Provider.Current().Account;
            entity.create_time = DateTime.Now;
            noticebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}