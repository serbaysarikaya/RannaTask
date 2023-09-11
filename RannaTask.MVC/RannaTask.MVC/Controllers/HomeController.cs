using Microsoft.AspNetCore.Mvc;

namespace RannaTask.MVC.Controllers
{
	public class HomeController : Controller
	{



		public IActionResult Index()
		{
			return View();
		}

	}
}