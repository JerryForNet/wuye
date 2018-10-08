using HuRongClub.Application.Busines.SystemManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.SystemManage;
using HuRongClub.Application.Entity.TenementManage.ViewModel;
using HuRongClub.Util;
using HuRongClub.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : Controller
    {
        #region 视图功能

        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {
            return View();
        }

        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminBeyond()
        {
            return View();
        }

        /// <summary>
        /// 我的桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult Desktop()
        {
            return View();
        }

        #endregion 视图功能

        #region 提交数据

        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 2;
            logEntity.OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.OperateAccount = OperatorProvider.Provider.Current().Account + "（" + OperatorProvider.Provider.Current().UserName + "）";
            logEntity.OperateUserId = OperatorProvider.Provider.Current().UserId;
            logEntity.ModuleId = moduleId;
            logEntity.Module = moduleName;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Content(moduleId);
        }

        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }

        #endregion 提交数据

        #region 获取数据

        /// <summary>
        /// 获取小区列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPropertyJson()
        {
            try
            {
                Busines.TenementManage.PropertyBLL bll = new Busines.TenementManage.PropertyBLL();
                string property_id = "";
                if (OperatorProvider.Provider.Current().IsSystem)
                {
                    property_id = "System";
                }
                else
                {
                    property_id = OperatorProvider.Provider.Current().UserProperty;
                }
                var data = bll.GetListBySel(property_id);
                if (data.Count() > 0)
                {
                    foreach (Entity.TenementManage.PropertyEntity item in data)
                    {
                        Util.Utils.WriteCookie("property_id", item.property_id);
                        Util.Utils.WriteCookie("property_name", item.property_name);
                        break;
                    }
                }
                return Content(data.ToJson());

            }
            catch (Exception ex)
            {
                LogEntity logEntity = new LogEntity();
                logEntity.CategoryId = 1;
                logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
                logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
                logEntity.OperateAccount = OperatorProvider.Provider.Current().Account + "（" + OperatorProvider.Provider.Current().UserName + "）";
                logEntity.OperateUserId = OperatorProvider.Provider.Current().UserId;
                logEntity.Module = Config.GetValue("SoftName");
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Content("");
            }
        }

        /// <summary>
        /// 获取出入库图标数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetInOutJson()
        {
            Busines.TenementManage.PropertyBLL bll = new Busines.TenementManage.PropertyBLL();
            List<OutInModel> list = bll.GetInList().ToList();
            string categories = "";
            DateTime time = DateTime.Now.AddMonths(-12);
            int[] monthList = new int[12];
            int j = 1;
            for (int i = 0; i < 12; i++)
            {
                monthList[i] = time.AddMonths(i).Month;
                categories += "," + time.AddMonths(i).Year + "年" + time.AddMonths(i).Month + "月";
                j++;
            }
            categories = categories.Substring(1);
            string inJson = GetData(list, "入库", monthList);
            list = bll.GetOutList().ToList();
            string outJson = GetData(list, "出库", monthList);
            string json = "";
            if (inJson != "")
            {
                json += "," + inJson;
            }
            if (outJson != "")
            {
                json += "," + outJson;
            }
            if (json != "")
            {
                json = "[" + json.Substring(1) + "]";
            }
            var data = new
            {
                series = json,
                categories = categories
            };
            return Content(data.ToJson());
        }

        private string GetData(List<OutInModel> list, string type, int[] monthList)
        {
            string json = "";
            if (list != null && list.Count > 0)
            {
                decimal[] array = new decimal[12];
                int i = 0;
                for (int j = 0; j < monthList.Length; j++)
                {
                    foreach (var item in list)
                    {
                        if (item.months == monthList[j])
                        {
                            array[j] = item.amount;
                            i = 1;
                            break;
                        }
                    }
                    if (i != 1)
                    {
                        array[j] = 0;
                        i = 0;
                    }
                }
                json = "{ name:'" + type + "',data:" + array.ToJson() + " }";
            }
            return json;
        }

        #endregion 获取数据
    }
}