using HuRongClub.Application.Busines.OaManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.OaManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.OaManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-26 16:05
    /// 描 述：工作周记
    /// </summary>
    [HandlerOperateLog]
    public class WeekplanController : MvcControllerBase
    {
        private WeekplanBLL weekplanbll = new WeekplanBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            var UserName = OperatorProvider.Provider.Current().OldSystemUserID; //当前登录老用户的id
            var Name = OperatorProvider.Provider.Current().UserName; //当前登录真姓名
            var manangerName = Checkmanager(Name);
            if (manangerName != null || Name == "超级管理员")
            {
                ViewBag.Checkmanger = "管理者";
            }
            ViewBag.username = UserName;
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        ///详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //   [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Info()
        {
            return View();
        }

        /// <summary>
        /// 批注页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //   [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Check()
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
            var data = weekplanbll.GetPageList(pagination, queryJson);
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
            var data = weekplanbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 是否有管理身份
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string Checkmanager(string username)
        {
            string check = weekplanbll.Checkmanager(username);
            if (check != null && check != "")
            {
                return check;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = weekplanbll.GetEntity(keyValue);
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
        //   [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            weekplanbll.RemoveForm(keyValue);
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
        [ValidateInput(false)]
        //  [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, WeekplanEntity entity)
        {
            if (keyValue == null || keyValue == "")
            {
                entity.ifcheck = "0"; //1  //默认添加是未批注
                weekplanbll.SaveForm(keyValue, entity);
                return Success("添加成功。");
            }
            else
            {
                string id = OperatorProvider.Provider.Current().OldSystemUserID; //当前登录老用户的id
                //如果当前登录的老id和要修改的id一致, 则批注状态不可改为已批注
                if (id == entity.userid)
                {
                    entity.ifcheck = "0"; //0 未批注
                }
                else
                {
                    entity.ifcheck = "1"; //1 已批注
                }
                weekplanbll.SaveForm(keyValue, entity);
                return Success("操作成功。");
            }
        }

        #endregion 提交数据
    }
}