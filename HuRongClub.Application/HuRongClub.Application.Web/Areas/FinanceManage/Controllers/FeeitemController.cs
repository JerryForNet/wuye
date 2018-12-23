using HuRongClub.Application.Busines.FinanceManage;
using HuRongClub.Application.Entity.FinanceManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.FinanceManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 17:11
    /// 描 述：费用科目表
    /// </summary>
    [HandlerOperateLog]
    [HandlerCheckFeeClose]
    public class FeeitemController : MvcControllerBase
    {
        private FeeitemBLL feeitembll = new FeeitemBLL();

        #region 视图功能

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
            return View();
        }

        /// <summary>
        /// 选择界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FeeitemIndex()
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
            var data = feeitembll.GetPageList(pagination, queryJson);
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
            var data = feeitembll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取列表（内部费用明细表）
        /// </summary>
        /// <param name="group_id">group_id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListg()
        {
            var data = feeitembll.GetListg();
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
            var data = feeitembll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="group_id">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetSelectJson(string group_id, int type = 0)
        {
            var data = feeitembll.GetListSel(group_id);
            if (type == 1)
            {
                var treeList = new List<TreeEntity>();
                foreach (var item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    tree.id = item.feeitem_id.ToString();
                    tree.text = item.feeitem_name;
                    tree.value = item.feeitem_id.ToString();
                    tree.parentId = "0";
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = false;
                    treeList.Add(tree);
                }
                return Content(treeList.TreeToJson());
            }
            else
            {
                return ToJsonResult(data);
            }
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
            feeitembll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, FeeitemEntity entity)
        {
            entity.taxrate = entity.taxrate + "%";
            feeitembll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}