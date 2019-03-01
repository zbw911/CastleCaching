using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CastleTEST.Models;
using ClassLibrary1;

namespace CastleTEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPerson _person;

        public HomeController(IPerson person)
        {
            var i = 0;
            _person = person;
        }


        [Caching.Core.Interceptor.CachingAble]
        public IActionResult Index()
        {
            var result = _person.Go(1);

            Console.WriteLine(result);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
