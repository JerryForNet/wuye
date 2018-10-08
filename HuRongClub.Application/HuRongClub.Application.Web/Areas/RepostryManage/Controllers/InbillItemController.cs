using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-26 13:42
    /// 描 述：入库汇总
    /// </summary>
    [HandlerOperateLog]
    public class InbillItemController : MvcControllerBase
    {
        private InbillItemBLL inbillitembll = new InbillItemBLL();

        #region 视图功能

        /// <summary>
        /// 入库汇总页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult InbillItemIndex()
        {
            return View();
        }

        /// <summary>
        /// 入库统计页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult InbillStatistics()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult InbillItemForm()
        {
            return View();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Form()
        {
            if (Request["keyValue"] == null) //为添加
            {
                ViewBag.OrderCode = "添加";
            }
            return View();
        }

        /// <summary>
        /// 单据删除列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteIndex()
        {
            return View();
        }
        /// <summary>
        /// 入库统计打印
        /// </summary>
        /// <returns></returns>
        public ActionResult previewInbillStatiscs()
        {
            return View();
        }
        /// <summary>
        /// 入库汇总打印
        /// </summary>
        /// <returns></returns>
        public ActionResult previewInbillItemIndex()
        {
            return View();
        }
        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 入库汇总列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(Pagination pagination, string queryJson)
        {
            var data = inbillitembll.GetList(pagination, queryJson);
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
            var data = inbillitembll.GetPageList(pagination, queryJson);
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
        /// 获取实体
        /// 入库统计列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetStatisticsJson(Pagination pagination, string queryJson)
        {
            var data = inbillitembll.GetStatisticsList(pagination, queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体         /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJsonByfinbillid(string queryJson)
        {
            var data = inbillitembll.GetListJsonByfinbillid(queryJson);
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
            var data = inbillitembll.GetEntity(keyValue);
            var bigtype = "";
            var smalltype = "";
            if (keyValue != "" && keyValue != null)
            {
                string st = data.fgoodsid.ToString();
                int start = 1, length = 3;
                bigtype = st.Substring(start - 1, length);
                int beg = 1, all = 6;
                smalltype = st.Substring(beg - 1, all);
            }
            var jsonData = new
            {
                data = data,
                bigtype = bigtype,
                smalltype = smalltype
            };
            return ToJsonResult(jsonData);
        }

        #endregion 获取数据

        #region 提交数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //  [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue,string type)
        {
            inbillitembll.RemoveForm(keyValue, type);
            return Success("删除成功。");
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="type">inid 入库单  outid 出库单</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveFormAll(string keyValue, string type)
        {
            inbillitembll.RemoveFormAll(keyValue, type);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="Type">价格类型</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, InbillItemEntity entity, string Type)
        {
            //默认金额传过来的是单价    type=-1 是单价  type=-2是总价
            if (Type == "-2")
            {
                decimal price = entity.fprice; //获取到金额
                entity.fmoney = price;// 总价为传过来的金额;
                entity.fprice = entity.fmoney / Convert.ToDecimal(entity.fnumber);  //单价就为总价除数量
            }
            else if (Type == "-1")
            {
                //为单价类型的话
                entity.fmoney = entity.fprice * Convert.ToDecimal(entity.fnumber); //总价为单价乘数量
            }
            else
            {
                //为空不做操作
            }
            inbillitembll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}