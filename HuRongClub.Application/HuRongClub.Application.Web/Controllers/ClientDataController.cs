using HuRongClub.Application.Busines.AuthorizeManage;
using HuRongClub.Application.Cache;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.AuthorizeManage;
using HuRongClub.Application.Entity.BaseManage;
using HuRongClub.Application.Entity.PersonnelManage;
using HuRongClub.Application.Entity.SystemManage.ViewModel;
using HuRongClub.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.09.01 13:32
    /// 描 述：客户端数据
    /// </summary>
    public class ClientDataController : MvcControllerBase
    {
        private DataItemCache dataItemCache = new DataItemCache();
        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentCache departmentCache = new DepartmentCache();
        private PostCache postCache = new PostCache();
        private RoleCache roleCache = new RoleCache();
        private UserGroupCache userGroupCache = new UserGroupCache();
        private UserCache userCache = new UserCache();
        private AuthorizeBLL authorizeBLL = new AuthorizeBLL();
        private EmployinfoCache employinfoCache = new EmployinfoCache();
        private HrDepartmentCache hrDepartmentCache = new HrDepartmentCache();

        #region 获取数据

        /// <summary>
        /// 批量加载数据给客户端（把常用数据全部加载到浏览器中 这样能够减少数据库交互）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetClientDataJson()
        {
            try
            {
                var jsonData = new
                {
                    dep = this.GetdepData(),                        //员工部门
                    organize = this.GetOrganizeData(),              //公司
                    department = this.GetDepartmentData(),          //部门
                    post = this.GetPostData(),                      //岗位
                    role = this.GetRoleData(),                      //角色
                    userGroup = this.GetUserGroupData(),            //用户组
                    user = this.GetUserData(),                      //用户
                    //dataItem = this.GetDataItem(),                  //字典
                    authorizeMenu = this.GetModuleData(),           //导航菜单
                    authorizeButton = this.GetModuleButtonData(),   //功能按钮
                    authorizeColumn = this.GetModuleColumnData(),   //功能视图
                };

                return ToJsonResult(jsonData);
            }
            catch (System.Exception ex)
            {
                Logger.Error(JsonConvert.SerializeObject(ex));
                return null;
            }
        }

        #endregion 获取数据

        #region 处理基础数据

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private object GetdepData()
        {
            var data = hrDepartmentCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (HrDepartmentEntity item in data)
                {
                    var fieldItem = new
                    {
                        FullName = item.deptname,
                        deptid = item.deptid
                    };
                    dictionary.Add(item.deptid.ToString(), fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取公司数据
        /// </summary>
        /// <returns></returns>
        private object GetOrganizeData()
        {
            var data = organizeCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (OrganizeEntity item in data)
                {
                    var fieldItem = new
                    {
                        EnCode = item.EnCode,
                        FullName = item.FullName
                    };
                    dictionary.Add(item.OrganizeId, fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <returns></returns>
        private object GetDepartmentData()
        {
            var data = departmentCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (DepartmentEntity item in data)
                {
                    var fieldItem = new
                    {
                        EnCode = item.EnCode,
                        FullName = item.FullName,
                        OrganizeId = item.OrganizeId
                    };
                    dictionary.Add(item.DepartmentId, fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetUserGroupData()
        {
            var data = userGroupCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (RoleEntity item in data)
                {
                    var fieldItem = new
                    {
                        EnCode = item.EnCode,
                        FullName = item.FullName
                    };
                    dictionary.Add(item.RoleId, fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetPostData()
        {
            var data = postCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (RoleEntity item in data)
                {
                    var fieldItem = new
                    {
                        EnCode = item.EnCode,
                        FullName = item.FullName
                    };
                    dictionary.Add(item.RoleId, fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <returns></returns>
        private object GetRoleData()
        {
            var data = roleCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (RoleEntity item in data)
                {
                    var fieldItem = new
                    {
                        EnCode = item.EnCode,
                        FullName = item.FullName
                    };
                    dictionary.Add(item.RoleId, fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        private object GetUserData()
        {
            var data = userCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            if (data != null)
            {
                foreach (UserEntity item in data)
                {
                    var fieldItem = new
                    {
                        EnCode = item.EnCode,
                        Account = item.Account,
                        RealName = item.RealName,
                        OrganizeId = item.OrganizeId,
                        DepartmentId = item.DepartmentId
                    };
                    dictionary.Add(item.UserId, fieldItem);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <returns></returns>
        private object GetDataItem()
        {
            var dataList = dataItemCache.GetDataItemList();
            var dataSort = dataList.Distinct(new Comparint<DataItemModel>("EnCode"));
            Dictionary<string, object> dictionarySort = new Dictionary<string, object>();
            if (dataList != null)
            {
                foreach (DataItemModel itemSort in dataSort)
                {
                    try
                    {
                        var dataItemList = dataList.Where(t => t.EnCode.Equals(itemSort.EnCode)).ToList();
                        Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                        if (dataItemList != null && dataItemList.Count() > 0)
                        {
                            foreach (DataItemModel itemList in dataItemList)
                            {
                                dictionaryItemList.Add(itemList.ItemValue, itemList.ItemName);
                            }

                            foreach (DataItemModel itemList in dataItemList)
                            {
                                dictionaryItemList.Add(itemList.ItemDetailId, itemList.ItemName);
                            }
                        }

                        dictionarySort.Add(itemSort.EnCode, dictionaryItemList);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Error(JsonConvert.SerializeObject(ex));
                    }
                }
            }

            return dictionarySort;
        }

        #endregion 处理基础数据

        #region 处理授权数据

        /// <summary>
        /// 获取功能数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleData()
        {
            return authorizeBLL.GetModuleList(SystemInfo.CurrentUserId);
        }

        /// <summary>
        /// 获取功能按钮数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleButtonData()
        {
            var data = authorizeBLL.GetModuleButtonList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleButtonEntity>("ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModule)
            {
                var buttonList = data.Where(t => t.ModuleId.Equals(item.ModuleId));
                dictionary.Add(item.ModuleId, buttonList);
            }
            return dictionary;
        }

        /// <summary>
        /// 获取功能视图数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleColumnData()
        {
            var data = authorizeBLL.GetModuleColumnList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleColumnEntity>("ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleColumnEntity item in dataModule)
            {
                var columnList = data.Where(t => t.ModuleId.Equals(item.ModuleId));
                dictionary.Add(item.ModuleId, columnList);
            }
            return dictionary;
        }

        #endregion 处理授权数据
    }
}