using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace RannaTask.MVC.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        public AdminMenuViewComponent()
        {

        }
        public ViewViewComponentResult Invoke()
        {
            return View();
        }
    }
}
