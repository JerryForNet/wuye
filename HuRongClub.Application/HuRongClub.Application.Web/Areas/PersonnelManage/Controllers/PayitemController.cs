using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.Busines.PersonnelManage;
using HuRongClub.Application.Code;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using HuRongClub.Application.Entity.SystemManage;
using HuRongClub.Application.Busines.SystemManage;
using HuRongClub.Application.Web.App_Start._01_Handler;


namespace HuRongClub.Application.Web.Areas.PersonnelManage.Controllers
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：蔡小龙
    /// 日 期：2017-04-27 18:06
    /// 描 述：Payitem
    /// </summary>
    [HandlerOperateLog]
    public class PayitemController : MvcControllerBase
    {
        private PayitemBLL payitembll = new PayitemBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PayitemIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PayitemForm()
        {
            return View();
        }
        #endregion

        #region 获取数据



        /// <summary>
        /// 分类列表 
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            // HuRongClub.Application.Busines.SystemManage.DataItemBLL dataItemBLL = new HuRongClub.Application.Busines.SystemManage.DataItemBLL();
            // var data = dataItemBLL.GetList().ToList();

            //var treeList = new List<TreeEntity>();
            //foreach (DataItemEntity item in data)
            //{
            //    TreeEntity tree = new TreeEntity();
            //    bool hasChildren = data.Count(t => t.ParentId == item.ItemId) == 0 ? false : true;
            //    tree.id = item.ItemId;
            //    tree.text = item.ItemName;
            //    tree.value = item.ItemCode;
            //    tree.parentId = item.ParentId;
            //    tree.isexpand = true;
            //    tree.complete = true;
            //    tree.Attribute = "isTree";
            //    tree.AttributeValue = item.IsTree.ToString();
            //    tree.hasChildren = hasChildren;
            //    treeList.Add(tree);
            //}
            //return Content(treeList.TreeToJson());
            DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
            var data = dataItemDetailBLL.GetList("fc2ada25-f18c-4fd7-beb7-2df8e19f64b1").ToList();

            var TreeList = new List<TreeEntity>();
            foreach (DataItemDetailEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;
                tree.id = item.ItemDetailId;
                tree.parentId = "0";
                tree.text = item.ItemName;
                tree.value = item.ItemCode;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
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
            var data = payitembll.GetPageList(pagination, queryJson);
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
            var data = payitembll.GetList(queryJson);
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
            var data = payitembll.GetEntity(keyValue);
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
            payitembll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, PayitemEntity entity)
        {
            payitembll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateDisable(string keyValue, string disable)
        {
            payitembll.UpdateDisable(keyValue, disable);
            return Success("操作成功。");
        }

        #endregion
    }
}
