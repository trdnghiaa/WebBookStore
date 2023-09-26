using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanSach
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WebsiteBanSach.Controllers" }
            );
            routes.MapRoute(
                name: "Admin",
                url: "admin/homeAdmin/{action}/{id}",
                defaults: new { controller = "homeAdmin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
