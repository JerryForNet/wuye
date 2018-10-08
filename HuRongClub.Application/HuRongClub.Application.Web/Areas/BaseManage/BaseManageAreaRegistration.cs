using System.Web.Mvc;

namespace HuRongClub.Application.Web.Areas.BaseManage
{
    public class BaseManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BaseManage";
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
