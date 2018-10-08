﻿using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.OaManage
{
    public class OaManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "OaManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "HuRongClub.Application.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}