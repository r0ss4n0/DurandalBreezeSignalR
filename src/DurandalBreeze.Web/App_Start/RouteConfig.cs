using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DurandalBreeze
{
    public class RouteConfig
    {
        public static string ControllerOnly = "ApiControllerOnly";
        public static string ControllerAndId = "ApiControllerAndId";
        public static string ControllerAction = "ApiControllerAction";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            // Register the default hubs route: ~/signalr
            routes.MapHubs();

            routes.MapHttpRoute(
                "apiRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
            
            // Go straight to the durandal folder when initialize
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Durandal", action = "Index", id = UrlParameter.Optional }
            );

            #region NOT IN USE once Breeze is handling the web api

            routes.MapHttpRoute(
                name: ControllerOnly,
                routeTemplate: "api/{controller}"
                );

            // This is the default route that a "File | New MVC 4 " project creates.
            // (I changed the name, removed the defaults, and added the constraints)
            //
            // This controller-per-type route lets us fetch a single resource by numeric id
            // It finds the appropriate method GetById method
            // on the controller using WebAPI conventions
            // The {id} is not optional, must be an integer, and 
            // must match a method with a parameter named "id" (case insensitive)
            //
            //  ex: api/bookingreservations/1
            routes.MapHttpRoute(
                name: ControllerAndId,
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            /********************************************************
            * The integer id constraint is necessary to distinguish 
            * the {id} route above from the {action} route below.
            * For example, the route above handles
            *     "api/bookingreservations/1" 
            * whereas the route below handles
            *     "api/bookingreservations/duplicated"
            ********************************************************/

            // This RPC style route is great for lookups and custom calls
            // It matches the {action} to a method on the controller 
            //
            // ex: api/bookingreservations/duplicated
            routes.MapHttpRoute(
                name: ControllerAction,
                routeTemplate: "api/{controller}/{action}"
                );

            #endregion
        }
    }
}