using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceD_One : ITestServiceD
    {
        public void Show()
        {
            Console.WriteLine("This is  TestServiceD_One   继承接口：ITestServiceD");
        }
    }
}
