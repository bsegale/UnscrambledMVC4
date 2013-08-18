using System.Web.Mvc;

namespace UnscrambledMVC4.Areas.Gamezone
{
    public class GamezoneAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Gamezone";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Gamezone_default",
                "Gamezone/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
