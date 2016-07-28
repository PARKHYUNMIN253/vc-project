using System.Web;
using System.Web.Mvc;

namespace BizOneShot.Light.Web
{
    //r 권한 관리
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
