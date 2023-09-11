using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace RannaTask.Mvc.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        public UserMenuViewComponent()
        {

        }

        public ViewViewComponentResult Invoke()
        {

            return View();

        }
    }
}
