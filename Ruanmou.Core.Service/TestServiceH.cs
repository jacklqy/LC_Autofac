using Ruanmou.Core.Interface;
using System;

namespace Ruanmou.Core.Service
{
    public class TestServiceH : ITestServiceH
    {

        private string _name = null;
        private int? _id = null;

        public TestServiceH()
        {

        } 

        public TestServiceH(string name)
        {
            _name = name;
        }

        public TestServiceH(string name, int? id)
        {
            _name = name;
            _id = id;
        }

        public void Show()
        {
            Console.WriteLine("This is  TestServiceH 继承接口：ITestServiceH");
        }
    }
}
