using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 日常维护
    /// </summary>
    [HandlerOperateLog]
    public class DailyMgrController : MvcControllerBase
    {
        private OwnerBLL ownerbll = new OwnerBLL();

        #region 视图功能

        /// <summary>
        /// 物业管理-日常管理-业主出户
        /// </summary>
        /// <returns></returns>
        public ActionResult OwnerHouseOut()
        {
            return View();
        }

        /// <summary>
        /// 物业管理-日常管理-业主进户
        /// </summary>
        /// <returns></returns>
        public ActionResult OwnerHouseIn()
        {
            return View();
        }

        /// <summary>
        /// 物业管理-日常管理-报修维修
        /// </summary>
        /// <returns></returns>
        public ActionResult OwnerMaintain()
        {
            return View();
        }

        #endregion
    }
}