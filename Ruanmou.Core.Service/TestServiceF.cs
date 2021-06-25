using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceF : ITestServiceF
    {
        public void Show()
        {
            Console.WriteLine("This is  TestServiceF 继承接口：ITestServiceF");
        }
    }
}
