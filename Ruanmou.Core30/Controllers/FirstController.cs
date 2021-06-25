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
    public class FirstController : Controller
    {
        private readonly ITestServiceA _testServiceA = null;
        private readonly IEnumerable<ITestServiceD> _TestServiceDs = null;

        public FirstController(IEnumerable<ITestServiceD> testServiceDs, ITestServiceA testServiceA)
        {
            _TestServiceDs = testServiceDs;
            _testServiceA = testServiceA;
        }
         
        public void Index()
        {
            foreach (var item in _TestServiceDs)
            {
                item.Show();
            }
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
