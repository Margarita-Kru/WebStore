using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        public IActionResult Status( string id)
        {
            switch(id)
            {
                default: return Content($"Status code - {id}");
                case "404": return View("Error");
            }
        }
    }
}
