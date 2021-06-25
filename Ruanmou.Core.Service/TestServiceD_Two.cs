using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceD_Two : ITestServiceD
    {
        public void Show()
        {
            Console.WriteLine("This is  TestServiceD_Two   继承接口：ITestServiceD");
        }
    }
}
