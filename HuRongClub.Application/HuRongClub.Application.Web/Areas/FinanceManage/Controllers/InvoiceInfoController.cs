using HuRongClub.Application.Busines.FinanceManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.FinanceManage;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.FinanceManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：Jerry.Li
    /// 日 期：2019-01-16 21:36
    /// 描 述：InvoiceInfo
    /// </summary>
    public class InvoiceInfoController : MvcControllerBase
    {
        private InvoiceInfoBLL invoiceinfobll = new InvoiceInfoBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult InvoiceInfoIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult InvoiceInfoForm()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string keyword)
        {
            var data = invoiceinfobll.GetList(keyword);
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
            var data = invoiceinfobll.GetEntity(keyValue);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            invoiceinfobll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, InvoiceInfoEntity entity)
        {
            invoiceinfobll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion
    }
}