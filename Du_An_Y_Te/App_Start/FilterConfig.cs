using System.Web;
using System.Web.Mvc;

namespace Du_An_Y_Te
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
