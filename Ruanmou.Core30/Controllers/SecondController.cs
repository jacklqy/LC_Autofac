using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ruanmou.Core.Interface;
using Ruanmou.Core30.Models;
using Ruanmou.Core30.Utility;

namespace Ruanmou.Core30.Controllers
{
    public class SecondController : Controller
    {
        private readonly ITestAutofac _testAutofac = null;


        public SecondController(ITestAutofac testAutofac)
        {
            _testAutofac = testAutofac;
        }

        public void Index()
        {
            _testAutofac.Show("习大大", 1);
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
