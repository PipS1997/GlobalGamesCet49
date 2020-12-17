using GlobalGamesCet49.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GlobalGamesCet49.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {

            return View();
        }

        public IActionResult Servicos()
        {

            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
