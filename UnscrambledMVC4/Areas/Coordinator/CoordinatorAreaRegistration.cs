using System.Web.Mvc;

namespace Unscrambled.Areas.Coordinator
{
    public class GameManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Coordinator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Coordinator_Default",
                "Coordinator/{controller}/{action}/{id}",
                new { controller = "Game", action = "MyGames", id = UrlParameter.Optional }
            );
        }
    }
}
