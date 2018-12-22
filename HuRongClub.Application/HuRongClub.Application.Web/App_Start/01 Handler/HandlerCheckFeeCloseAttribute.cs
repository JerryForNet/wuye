using HuRongClub.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HuRongClub.Application.Web.App_Start._01_Handler
{
    public class HandlerCheckFeeCloseAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 校验开关账状态
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string current = Utils.GetCookie("property_id");

        }
    }
}