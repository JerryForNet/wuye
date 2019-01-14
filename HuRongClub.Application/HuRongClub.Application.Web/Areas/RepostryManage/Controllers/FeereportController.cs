using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 统计中心
    /// </summary>
    [HandlerOperateLog]
    public class FeereportController : MvcControllerBase
    {
        private FeereportBLL FeereportBLL = new FeereportBLL();
        private GoodsinfoBLL goodsinfobll = new GoodsinfoBLL();

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
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DistinctIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PercentIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页面--欠费统计
        /// </summary>
        /// <returns></returns>
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DelayIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        ////[HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ChargeList()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult zhanglin()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ArrearsIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ArrearsDetail()
        {
            return View();
        }

        /// <summary>
        /// 租户月收费中心
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Rentreport()
        {
            return View();
        }

        /// <summary>
        /// 小区台帐
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Account()
        {
            return View();
        }


        /// <summary>
        /// 物资库存报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FeeReportGoods()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDistinctPageListJson(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                var data = FeereportBLL.GetqianfeiDistrictListJson(queryJson);
                return ToJsonResult(data);
            }
        }

        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPercentIndexListJson(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                var data = FeereportBLL.GetPercentListJson(queryJson);
                return ToJsonResult(data);
            }
        }

        /// <summary>
        /// 获取应收费用列表（筛选）
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDelayIndexListJson(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                var data = FeereportBLL.GetDelayListJson(queryJson);
                return ToJsonResult(data);
            }
        }

        /// <summary>
        /// 收费统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetChargeListJson(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                var data = FeereportBLL.GetChargeListJson(queryJson);
                return ToJsonResult(data);
            }
        }

        /// <summary>
        /// 账龄统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetzhanglingListJson(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                var data = FeereportBLL.GetzhanglingListJson(queryJson);
                return ToJsonResult(data);
            }
        }

        /// <summary>
        /// 账龄统计
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetArrearslistJson(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                dynamic json = JsonConvert.DeserializeObject<dynamic>(queryJson);
                json.propertyID = Utils.GetCookie("property_id");

                var data = FeereportBLL.GetArrearslistJson(JsonConvert.SerializeObject(json));
                return ToJsonResult(data);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArrearsDetailJson(string queryJson, Pagination pagination)
        {
            var data = FeereportBLL.GetArrearsDetailJson(queryJson, pagination);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 租户月收费中心
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRentreportListJson(string queryJson)
        {
            if (!string.IsNullOrEmpty(queryJson))
            {
                string property_id = "";
                if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
                {
                    property_id = Utils.GetCookie("property_id");
                }
                var data = FeereportBLL.GetRentreportList(queryJson, property_id);

                return ToJsonResult(data);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 小区台帐
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAccountListJson(string queryJson)
        {
            if (!string.IsNullOrEmpty(queryJson))
            {
                string property_id = "";
                if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
                {
                    property_id = Utils.GetCookie("property_id");
                }
                Busines.TenementManage.FeeincomeBLL bll = new Busines.TenementManage.FeeincomeBLL();
                var watch = CommonHelper.TimerStart();
                var data = bll.GetAccountList(property_id, queryJson);
                var jsonData = new
                {
                    rows = data,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                return ToJsonResult(jsonData);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 欠费导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExportArrears(string queryJson)
        {
            int ret = FeereportBLL.ExportArrears(queryJson);
            if (ret == 0)
            {
                return Error("未找到需导出信息！");
            }
            else
            {
                return Success("导出成功。");
            }
        }

        /// <summary>
        /// 导出列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportList(string queryJson)
        {
            if (string.IsNullOrEmpty(queryJson) || queryJson.IndexOf("undefined") > -1)
            {
                return null;
            }
            else
            {
                var data = FeereportBLL.GetChargeListJson(queryJson);

                FeereportBLL.GetExportList(queryJson);
                return Success("导出成功。");
            }
        }



        /// <summary>
        /// 物质库存统计报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFeeReportGoodsJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = goodsinfobll.GetReportGoods(pagination, queryJson);
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

        #endregion
    }
}