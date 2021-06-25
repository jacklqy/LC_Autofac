using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ruanmou.Core.Interface;
using Ruanmou.Core30.Models;

namespace Ruanmou.Core30.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestServiceA _TestServiceA = null;
        private readonly ITestServiceB _TestServiceB = null;
        private readonly ITestServiceC _TestServiceC = null;
        private readonly ITestServiceD _TestServiceD = null;

        public HomeController(ITestServiceA testServiceA, ITestServiceB testServiceB, ITestServiceC testServiceC, ITestServiceD testServiceD)
        {
            _TestServiceA = testServiceA;
            _TestServiceB = testServiceB;
            _TestServiceC = testServiceC;
            _TestServiceD = testServiceD;
        }
         
        public IActionResult Index()
        {
            _TestServiceA.Show();
            _TestServiceB.Show();
            _TestServiceC.Show();
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
