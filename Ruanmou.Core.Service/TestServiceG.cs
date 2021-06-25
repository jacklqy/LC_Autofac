using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceG : ITestServiceG
    {

        private ITestServiceB testServiceB = null;
         
        public TestServiceG(ITestServiceB iTestServiceB)
        {
            testServiceB = iTestServiceB;
        }
        public void Show()
        {
            Console.WriteLine("This is  TestServiceG 继承接口：TestServiceG");
        }
    }
}
