using System.Web;
using System.Web.Mvc;

namespace Marvel_of_the_Universe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
