using Microsoft.AspNetCore.Mvc;
using System;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Status( string id)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));

            switch (id)
            {
                default: return Content($"Status code - {id}");
                case "404": return View("Error");
            }
        }
    }
}
