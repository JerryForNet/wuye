using HuRongClub.Application.Busines.PersonnelManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.PersonnelManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 09:43
    /// 描 述：员工档案
    /// </summary>
    [HandlerOperateLog]
    public class EmployinfoController : MvcControllerBase
    {
        private EmployinfoBLL employinfobll = new EmployinfoBLL();
        private EmployinfoCache employinfoCache = new EmployinfoCache();

        #region 视图功能

        /// <summary>
        /// 员工列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult EmployinfoIndex()
        {
            return View();
        }

        /// <summary>
        /// 员工档案明细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult EmployinfoForm()
        {
            return View();
        }

        /// <summary>
        /// 离职员工
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DimissionFrom()
        {
            return View();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult EmployinfoExport()
        {
            return View();
        }

        /// <summary>
        /// 合同到期处理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult EmployinfoExpire()
        {
            return View();
        }

        /// <summary>
        /// 选择姓名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmployinfoRealName()
        {
            return View();
        }

        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListJson(int deptid)
        {
            var data = employinfoCache.GetList(deptid);
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
            var data = employinfobll.GetPageList(pagination, queryJson);
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
        public ActionResult GetFormJson(int keyValue)
        {
            var data = employinfobll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="queryJson"></param>
        /// <param name="field">导出字段</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEmployinfoExport(string queryJson, string field)
        {
            int ret = employinfobll.EmployinfoExport(queryJson, field);
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
        /// 获取员工下拉列表
        /// </summary>
        /// <param name="type">1常规JSON 2 树JSON</param>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelectJson(int deptid, int type = 1)
        {
            var data = employinfobll.GetList(deptid).OrderBy(t => t.empname);
            if (type == 2)
            {
                var treeList = new List<TreeEntity>();
                foreach (EmployinfoEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = false;
                    tree.id = item.empid.ToString();
                    tree.text = item.empname;
                    tree.value = item.empid.ToString();
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
        /// 离职员工
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="fireoutdate"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Dimission(string keyValue, DateTime? fireoutdate)
        {
            if (keyValue == "System")
            {
                throw new Exception("当前账户不禁用");
            }
            employinfobll.UpdateState(keyValue, 1, fireoutdate);
            return Success("账户离职成功");
        }

        /// <summary>
        /// 复职员工
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Reinstatement(string keyValue)
        {
            employinfobll.UpdateState(keyValue, 0, null);
            return Success("账户复职成功");
        }

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
            employinfobll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, EmployinfoEntity entity)
        {
            employinfobll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 合同续费
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="firedate">合同续费时间</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult ExpireFrom(string keyValue, DateTime? firedate)
        {
            if (firedate != null && !string.IsNullOrEmpty(firedate.ToString()))
            {
                if (keyValue != null && !string.IsNullOrEmpty(keyValue))
                {
                    employinfobll.ExpireFrom(keyValue, firedate);
                    return Success("操作成功。");
                }
                else
                {
                    return Error("请选择合同续签员工！");
                }
            }
            else
            {
                return Error("输入的日期格式无效！");
            }
        }

        #endregion 提交数据
    }
}