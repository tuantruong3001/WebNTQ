using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebNTQ
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/{slug}-{id}",
               defaults: new { controller = "ProductUser", action = "Detail", id = UrlParameter.Optional },
              namespaces: new[] { "WebNTQ.Areas.Admin.Controllers" }

               );
            routes.MapRoute(
              name: "Product Detail Home",
              url: "chi-tiet/{slug}-{id}",
              defaults: new { controller = "Home", action = "Detail", id = UrlParameter.Optional },
             namespaces: new[] { "WebNTQ.Controllers" }

              );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                /*namespaces: new[] { "WebNTQ.Controllers" }*/
            );
        }
    }
}
