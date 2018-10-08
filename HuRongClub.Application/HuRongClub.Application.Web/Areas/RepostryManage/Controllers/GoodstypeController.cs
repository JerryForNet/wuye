using HuRongClub.Application.Busines.RepostryManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.RepostryManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.RepostryManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 13:28
    /// 描 述：物品类别
    /// </summary>
    [HandlerOperateLog]
    public class GoodstypeController : MvcControllerBase
    {
        private GoodstypeBLL goodstypebll = new GoodstypeBLL();
        private GoodstypeCache goodstypeCache = new GoodstypeCache();

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

        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 物品列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword, int flayer)
        {
            var data = goodstypeCache.GetList().ToList();

            if (flayer == 0)
            {
                var d = data.Where(t => t.flayer == flayer).ToList<GoodstypeEntity>();
                return Content(d.ToJson());
            }
            else
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.TreeWhere(t => t.ftypename.Contains(keyword), "ftypecode");
                }
                var treeList = new List<TreeEntity>();
                foreach (GoodstypeEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = data.Count(t => t.frootid == item.ftypecode) == 0 ? false : true;
                    tree.id = item.ftypecode;
                    tree.text = item.ftypename;
                    tree.value = item.ftypecode;
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = hasChildren;
                    tree.parentId = item.frootid;
                    treeList.Add(tree);
                }

                return Content(treeList.TreeToJson());
            }
        }

        /// <summary>
        /// 物品列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string queryJson)
        {
            var data = goodstypebll.GetList(queryJson).ToList();
            if (data.Count <= 1)
            {
                return Content(data.ToJson());
            }
            else
            {
                var treeList = new List<TreeGridEntity>();
                foreach (GoodstypeEntity item in data)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    bool hasChildren = data.Count(t => t.frootid == item.ftypecode) == 0 ? false : true;
                    tree.id = item.ftypecode;
                    tree.hasChildren = hasChildren;
                    tree.parentId = item.fparentcode;
                    tree.expanded = true;
                    tree.entityJson = item.ToJson();
                    treeList.Add(tree);
                }
                return Content(treeList.TreeJson());
            }
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
            var data = goodstypebll.GetPageList(pagination, queryJson);
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
        public ActionResult GetListJson(string fparentcode)
        {
            var data = goodstypebll.GetListJson(fparentcode);
            var treeList = new List<TreeEntity>();
            foreach (GoodstypeEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;//data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.ftypecode;
                tree.text = item.ftypename;
                tree.value = item.ftypecode;
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
            var data = goodstypebll.GetEntity(keyValue);
            return Content(data.ToJson());
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
            goodstypebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, GoodstypeEntity entity)
        {
            if (entity.frootid != "" || entity.frootid != null)  //父级不为空
            {
                entity.ftypecode = entity.frootid + entity.ftypecode; //组成新的code
            }
            if (entity.flayer == 0 && entity.fparentcode == "")
            {
                entity.fparentcode = "0";
            }
            else
            {
                entity.fparentcode = entity.frootid;
            }
            goodstypebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}