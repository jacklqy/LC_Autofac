using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceD : ITestServiceD
    {
        public void Show()
        {
            Console.WriteLine("This is  TestServiceD 继承接口：ITestServiceD");
        }
    }
}
