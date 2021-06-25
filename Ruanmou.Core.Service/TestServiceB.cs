using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceB : ITestServiceB
    {
        
        public TestServiceB(ITestServiceA iTestService)
        {
          
        }


        public void Show()
        {
            Console.WriteLine("This is  TestServiceB  继承接口：ITestServiceB"); 
        }
    }
}
