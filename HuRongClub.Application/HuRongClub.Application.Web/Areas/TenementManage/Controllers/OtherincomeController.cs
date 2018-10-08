using HuRongClub.Application.Busines.TenementManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.TenementManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.TenementManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：姜良福
    /// 日 期：2017-04-06 09:43
    /// 描 述：Otherincome
    /// </summary>
    [HandlerOperateLog]
    public class OtherincomeController : MvcControllerBase
    {
        private OtherincomeBLL otherincomebll = new OtherincomeBLL();

        #region 视图功能

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OtherincomeIndex()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OtherincomeForm()
        {
            return View();
        }

        /// <summary>
        /// 列表页面--其他收费
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表单页面--其他收费
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            ViewBag.property_name = Utils.GetCookie("property_name");
            return View();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintFrom(string keyValue)
        {
            ViewBag.UserName = Code.OperatorProvider.Provider.Current().UserName;

            DateTime time = DateTime.Now;
            ViewBag.year = time.Year.ToString();

            ViewBag.month = (time.Month < 10 ? ("0" + time.Month.ToString()) : time.Month.ToString());
            ViewBag.day = (time.Day < 10 ? ("0" + time.Day.ToString()) : time.Day.ToString());

            string property_id = "";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }

            OtherincomeEntity ent_other = otherincomebll.GetEntity(keyValue);
            ViewBag.customer = ent_other.customer;
            string ticketid = ent_other.ticketid;

            string strType = ticketid.Substring(0, 2);
            ViewBag.ticType = (strType == "00" ? "收据" : (strType == "01" ? "发票" : (strType == "02" ? "停车发票" : (strType == "04" ? "虹联发票" : (strType == "05" ? "虹联收据" : "新厦发票")))));
            ViewBag.ticketCode = ticketid.Substring(2, ticketid.Length - 2);

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
            var data = otherincomebll.GetList(pagination, property_id, queryJson);
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
            var data = otherincomebll.GetList(queryJson);
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
            var data = otherincomebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取客户收费单
        /// </summary>
        /// <param name="receive_id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormsJson(string receive_id, int type)
        {
            var data = otherincomebll.GetEntitys(receive_id, type);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表--应收收入明细
        /// </summary>
        /// <param name="receive_id">查询参数</param>
        /// <param name="type"></param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListOtherJson(string receive_id, int type)
        {
            var data = otherincomebll.GetListByOther(receive_id, type);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表--其他收入列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
        public ActionResult GetPageListOtherJson(Pagination pagination, string queryJson)
        {
            string property_id = " ";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }
            var watch = CommonHelper.TimerStart();
            var data = otherincomebll.GetOtherList(pagination, property_id, queryJson);
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
        /// <param name="incomeid">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string incomeid)
        {
            var data = otherincomebll.GetOtherDetailList(incomeid);
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
            otherincomebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, OtherincomeEntity entity)
        {
            otherincomebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
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
            var entity = strEntity.Replace(" ", "").ToObject<OtherincomeEntity>();
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                entity.property_id = Utils.GetCookie("property_id");
            }
            entity.operateuser = OperatorProvider.Provider.Current().UserName;
            entity.inputtime = DateTime.Now;

            var entryList = strChildEntitys.ToList<OtherincomeitemEntity>();

            double dl = 0;
            foreach (OtherincomeitemEntity item in entryList)
            {
                if (!string.IsNullOrEmpty(item.fee_income.ToString()))
                {
                    dl += item.fee_income.ToDouble();
                }
            }
            entity.feemoney = dl;

            string incomeid = otherincomebll.SavesForm(keyValue, entity, entryList);
            return Success("操作成功。", incomeid);
        }

        #endregion
    }
}