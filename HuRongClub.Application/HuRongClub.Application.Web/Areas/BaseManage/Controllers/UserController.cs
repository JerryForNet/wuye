using HuRongClub.Application.Busines.AuthorizeManage;
using HuRongClub.Application.Busines.BaseManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.AuthorizeManage;
using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Web.App_Start._01_Handler;
using HuRongClub.Util;
using HuRongClub.Util.Extension;
using HuRongClub.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    [HandlerOperateLog]
    public class UserController : MvcControllerBase
    {
        private UserBLL userBLL = new UserBLL();
        private UserCache userCache = new UserCache();
        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentCache departmentCache = new DepartmentCache();
        private ModuleFormInstanceBLL moduleFormInstanceBll = new ModuleFormInstanceBLL();

        #region 视图功能

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RevisePassword()
        {
            return View();
        }
        /// <summary>
        /// 管辖物业
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult PropertyMag()
        {
            return View();
        }
        /// <summary>
        /// 管辖部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DepartmentMag()
        {
            return View();
        }
        #endregion 视图功能

        #region 获取数据

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回机构+部门+用户树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var organizedata = organizeCache.GetList();
            var departmentdata = departmentCache.GetList();
            var userdata = userCache.GetList();
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                #region 机构

                TreeEntity tree = new TreeEntity();
                bool hasChildren = organizedata.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == item.OrganizeId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                tree.id = item.OrganizeId;
                tree.text = item.FullName;
                tree.value = item.OrganizeId;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Organize";
                treeList.Add(tree);

                #endregion 机构
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                #region 部门

                TreeEntity tree = new TreeEntity();
                tree.id = item.DepartmentId;
                tree.text = item.FullName;
                tree.value = item.DepartmentId;
                if (item.ParentId == "0")
                {
                    tree.parentId = item.OrganizeId;
                }
                else
                {
                    tree.parentId = item.ParentId;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Department";
                treeList.Add(tree);

                #endregion 部门
            }
            foreach (UserEntity item in userdata)
            {
                #region 用户

                TreeEntity tree = new TreeEntity();
                tree.id = item.UserId;
                tree.text = item.RealName;
                tree.value = item.Account;
                tree.parentId = item.DepartmentId;
                tree.title = item.RealName + "（" + item.Account + "）";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.Attribute = "Sort";
                tree.AttributeValue = "User";
                tree.img = "fa fa-user";
                treeList.Add(tree);

                #endregion 用户
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns>返回用户列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string departmentId)
        {
            var data = userCache.GetList(departmentId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = userBLL.GetPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }

        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取下拉列表
        /// </summary>
        /// <param name="type">1 树行 2 下拉列表</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListSel(int type)
        {
            string property_id = " ";
            if (!string.IsNullOrEmpty(Utils.GetCookie("property_id")))
            {
                property_id = Utils.GetCookie("property_id");
            }

            var data = userBLL.GetListSel(property_id);
            if (type == 1)
            {
                var treeList = new List<TreeEntity>();
                foreach (var item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    tree.id = item.UserId;
                    tree.text = item.RealName;
                    tree.value = item.UserId;
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

        #region 验证数据

        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="Account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistAccount(string Account, string keyValue)
        {
            bool IsOk = userBLL.ExistAccount(Account, keyValue);
            return Content(IsOk.ToString());
        }

        #endregion 验证数据

        #region 提交数据

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            if (keyValue == "System")
            {
                throw new Exception("当前账户不能删除");
            }
            userBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strUserEntity, string FormInstanceId, string strModuleFormInstanceEntity)
        {
            UserEntity userEntity = strUserEntity.ToObject<UserEntity>();
            if (userEntity.Birthday == null || string.IsNullOrEmpty(userEntity.Birthday.ToString()))
            {
                userEntity.Birthday = "1900-01-01".ToDateOrNull();
            }

            ModuleFormInstanceEntity moduleFormInstanceEntity = strModuleFormInstanceEntity.ToObject<ModuleFormInstanceEntity>();
            string objectId = userBLL.SaveForm(keyValue, userEntity);
            moduleFormInstanceEntity.ObjectId = objectId;
            moduleFormInstanceBll.SaveEntity(FormInstanceId, moduleFormInstanceEntity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存重置修改密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveRevisePassword(string keyValue, string Password)
        {
            if (keyValue == "System")
            {
                throw new Exception("当前账户不能重置密码");
            }
            userBLL.RevisePassword(keyValue, Password);
            return Success("密码修改成功，请牢记新密码。");
        }

        /// <summary>
        /// 禁用账户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DisabledAccount(string keyValue)
        {
            if (keyValue == "System")
            {
                throw new Exception("当前账户不禁用");
            }
            userBLL.UpdateState(keyValue, 0);
            return Success("账户禁用成功。");
        }

        /// <summary>
        /// 启用账户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult EnabledAccount(string keyValue)
        {
            userBLL.UpdateState(keyValue, 1);
            return Success("账户启用成功。");
        }
        /// <summary>
        /// 修改用户管辖物业
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UserProperty">管辖物业</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateUserProperty(string keyValue, string UserProperty)
        {
            UserEntity entity = new UserEntity();
            entity.UserId = keyValue;
            entity.UserProperty = UserProperty;

            userBLL.UpdateUserInfo(entity);

            return Success("操作成功。");
        }
        /// <summary>
        /// 修改用户管辖部门
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="ManageDepartment">管辖部门</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateManageDepartment(string keyValue, string ManageDepartment)
        {
            UserEntity entity = new UserEntity();
            entity.UserId = keyValue;
            entity.ManageDepartment = ManageDepartment;

            userBLL.UpdateUserInfo(entity);

            return Success("操作成功。");
        }
        #endregion 提交数据

        #region 数据导出

        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportUserList()
        {
            userBLL.GetExportList();
            return Success("导出成功。");
        }

        #endregion 数据导出
    }
}