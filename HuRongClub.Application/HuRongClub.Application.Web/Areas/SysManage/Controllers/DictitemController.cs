using HuRongClub.Application.Busines.SysManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.SysManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-12 20:55
    /// 描 述：字典详情
    /// </summary>
    [HandlerOperateLog]
    public class DictitemController : MvcControllerBase
    {
        private DictitemBLL dictitembll = new DictitemBLL();
        private DictitemCache dictitemCache = new DictitemCache();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
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

        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 获取对应字典项详情信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageListByDid(string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            DictitemBLL bll = new DictitemBLL();
            var data = bll.GetPageList( queryJson);
            return ToJsonResult(data);
         }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword, string dictid)
        {
            var data = dictitemCache.GetList(dictid).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.dictid.ToString().Contains(keyword), "dictid");
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = dictitembll.GetList(queryJson);
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
            var data = dictitembll.GetEntity(keyValue);
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
        public ActionResult RemoveForm(string keyValue)
        {
            dictitembll.RemoveForm(keyValue);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, DictitemEntity entity)
        {
            dictitembll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}