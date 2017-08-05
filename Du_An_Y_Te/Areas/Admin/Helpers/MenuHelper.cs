using System;
using System.Linq;
using System.Web.Mvc;

namespace Du_An_Y_Te.Areas.Admin.Helpers
{
    public static class MenuHelper
    {
        public static string IsSelected_Open(this HtmlHelper html, string controller = null, string action = null)
        {
            const string cssClass = "open";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller.Split(';').Contains(currentController) && action.Split(';').Contains(currentAction) ?
                cssClass : String.Empty;
        }
        public static string IsSelected_Active(this HtmlHelper html, string controller = null, string action = null)
        {
            const string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller.Split(';').Contains(currentController) && action.Split(';').Contains(currentAction) ?
                cssClass : String.Empty;
        }
    }
}