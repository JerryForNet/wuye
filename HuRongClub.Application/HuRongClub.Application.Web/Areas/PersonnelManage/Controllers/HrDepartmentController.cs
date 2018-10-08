using HuRongClub.Application.Busines.PersonnelManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.PersonnelManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 10:41
    /// 描 述：部门信息
    /// </summary>
    [HandlerOperateLog]
    public class HrDepartmentController : MvcControllerBase
    {
        private HrDepartmentBLL departmentbll = new HrDepartmentBLL();
        private HrDepartmentCache hrDepartmentCache = new HrDepartmentCache();

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
        /// 列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var data = hrDepartmentCache.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.deptname.Contains(keyword), "deptid");
            }
            return Content(data.ToJson());
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
            var data = departmentbll.GetPageList(pagination, queryJson);
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
            var data = departmentbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = departmentbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取部门下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelect(int type=1)
        {
            var data = departmentbll.GetList("").OrderBy(t=>t.deptname);
            if (type == 2)
            {
                var treeList = new List<TreeEntity>();
                foreach (HrDepartmentEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = false;
                    tree.id = item.deptid.ToString();
                    tree.text = item.deptname;
                    tree.value = item.deptid.ToString();
                    tree.parentId = "0";
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }
                return Content(treeList.TreeToJson());
            }
            else
            {
                return Content(data.ToJson());
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
            departmentbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, HrDepartmentEntity entity)
        {
            departmentbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        #endregion 提交数据
    }
}