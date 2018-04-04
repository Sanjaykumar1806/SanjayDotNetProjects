using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeerWiki
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BeerDetails",
                url: "{controller}/{action}/{Id}",
                defaults: new { controller = "Home", action = "BeerDetails", beerId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BeerGridList",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "BeerGridList", beerId = UrlParameter.Optional }
            );
        }
    }
}
