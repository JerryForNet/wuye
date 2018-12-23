using HuRongClub.Application.Busines.FinanceManage;
using HuRongClub.Util;
using HuRongClub.Util.WebControl;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.App_Start._01_Handler
{
    /// <summary>
    /// 校验当前财务账是否关闭
    /// </summary>
    public class HandlerCheckFeeCloseAttribute : ActionFilterAttribute
    {
        private static FeeCloseBLL feeclosebll = new FeeCloseBLL();

        /// <summary>
        /// 校验开关账状态(仅限当月)
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string propertyId = Utils.GetCookie("property_id");
            bool fstatus = feeclosebll.GetCurrentStatus(propertyId);
            if (!fstatus)
            {
                string requestType = filterContext.HttpContext.Request.RequestType.ToLower();
                if (requestType.Equals("post"))
                {
                    ContentResult Content = new ContentResult();
                    //Content.Content = "<script type='text/javascript'>alert('很抱歉！财务账单被关闭，操作被拒绝！');top.Loading(false);</script>";
                    Content.Content = new AjaxResult { type = ResultType.error, message = "很抱歉！财务账单被关闭，操作被拒绝！" }.ToJson();
                    
                    filterContext.Result = Content;
                    return;
                }
            }
        }
    }
}