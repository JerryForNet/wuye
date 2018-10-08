using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Busines.SystemManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：超级管理员
    /// 日 期：2017-04-08 18:58
    /// 描 述：Inbill
    /// </summary>
    [HandlerOperateLog]
    public class InbillController : MvcControllerBase
    {
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private InbillBLL inbillbll = new InbillBLL();//主入库单
        private InbillItemBLL InbillItembll = new InbillItemBLL(); //入库单详情

        #region 视图功能

        #region 测试部分页面

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InbillIndex()
        {
            return View();
        }

        #endregion 测试部分页面

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            if (Request["keyValue"] == null)
            {
                //ViewBag.InbilCode = codeRuleBLL.GetBillCode("System", "", "10006");
            }
            return View();
        }

        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info()
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
            if (string.IsNullOrEmpty(queryJson))
            {
                return null;
            }

            var watch = CommonHelper.TimerStart();
            var data = inbillbll.GetPageList(pagination, queryJson);
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
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = inbillbll.GetEntity(keyValue);
            var childData = inbillbll.GetDetails(keyValue);
            var jsonData = new { entity = data, childEntity = childData };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = inbillbll.GetDetails(keyValue);
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
        public ActionResult RemoveForm(string keyValue)
        {
            inbillbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, InbillEntity entity, string strChildEntitys)
        {
            var childEntitys = strChildEntitys.ToList<InbillItemEntity>();
            inbillbll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }

        #endregion 提交数据

        #region 验证数据

        /// <summary>
        /// 验证是否有子表数据
        /// </summary>
        /// <param name="findid"></param>
        /// <returns></returns>
        public string CheckDel(string findid)
        {
            string check = inbillbll.CheckDel(findid);
            if (check != null && check != "")
            {
                return check;
            }
            else
            {
                return null;
            }
        }

        #endregion 验证数据
    }
}