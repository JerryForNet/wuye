using HuRongClub.Application.Busines.SystemManage;
using HuRongClub.Application.Code;
using HuRongClub.Application.Entity.SystemManage;
using HuRongClub.Util.Attributes;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.App_Start._01_Handler
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class HandlerOperateLogAttribute : ActionFilterAttribute
    {
        private OperationType _operationType;

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;

            switch (actionName)
            {
                case "SaveForm":
                    _operationType = OperationType.Submit;
                    break;

                case "RemoveForm":
                    _operationType = OperationType.Delete;
                    break;

                case "GetListJson":
                case "GetPageListJson":
                    _operationType = OperationType.Get;
                    break;

                case "Index":
                case "Form":
                case "Detail":
                    _operationType = OperationType.Visit;
                    break;

                default:
                    _operationType = OperationType.Other;
                    break;
            }

            if (_operationType == OperationType.Other)
            {
                if (actionName.IndexOf("Get") != -1)
                {
                    _operationType = OperationType.Get;
                }
            }

            if (_operationType != OperationType.Visit)
            {
                LogEntity logEntity = new LogEntity();

                logEntity.CategoryId = 3;
                logEntity.OperateTypeId = ((int)OperationType.Exception).ToString();
                logEntity.OperateType = EnumAttribute.GetDescription(_operationType);
                logEntity.OperateAccount = OperatorProvider.Provider.Current().Account + "（" + OperatorProvider.Provider.Current().UserName + "）";
                logEntity.OperateUserId = OperatorProvider.Provider.Current().UserId;
                logEntity.ExecuteResult = 0;
                logEntity.ExecuteResultJson = "QueryString:" + request.QueryString.ToString() + "|postJson:" + request.Form.ToString();
                logEntity.Module = controllerName + "/" + actionName;

                logEntity.WriteLog();
            }
        }
    }
}