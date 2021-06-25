using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceA : ITestServiceA
    {
        public void Show()
        {
            Console.WriteLine("This is  TestServiceA 继承接口：ITestServiceA");
        }
    }
}
