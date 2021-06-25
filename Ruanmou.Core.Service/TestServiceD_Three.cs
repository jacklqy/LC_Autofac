using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceD_Three : ITestServiceD
    {
        public void Show()
        {
            Console.WriteLine("This is  TestServiceD_Three   继承接口：ITestServiceD");
        }
    }
}
