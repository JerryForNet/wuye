using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-25 15:16
    /// 描 述：物品info信息
    /// </summary>
    [HandlerOperateLog]
    public class GoodsinfoController : MvcControllerBase
    {
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
        /// 选择界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OptionIndex()
        {
            return View();
        }
        /// <summary>
        /// 库存打印
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PrintForm()
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
            var data = goodsinfobll.GetPageList(pagination, queryJson);
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
        /// <param name="fgoodsid">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string fgoodsid)
        {
            var data = goodsinfobll.GetList(fgoodsid);
            var treeList = new List<TreeEntity>();
            foreach (GoodsinfoEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;//data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.fgoodsid;
                tree.text = string.IsNullOrEmpty(item.fname) ? item.fname : item.fname.Replace("\"", "\\\"");
                tree.value = item.fgoodsid;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;

                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = goodsinfobll.GetEntity(keyValue);
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
            goodsinfobll.RemoveForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tid">类型</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue,string tid, GoodsinfoEntity entity)
        {
            goodsinfobll.SaveForm(keyValue, tid, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult ExportGoodsInfo(string queryJson)
        {
            int ret = goodsinfobll.ExportGoodsInfo(queryJson);
            if (ret == 0)
            {
                return Error("未找到需导出信息！");
            }
            else
            {
                return Success("导出成功。");
            }
        }

        #endregion 提交数据
    }
}