using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.Core30.Utility
{
    public class CutomAutofacAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {

            Console.WriteLine($"被标记方法执行前执行  invocation.Arguments:{invocation.Arguments[0]};{invocation.Arguments[1]}");

            invocation.Proceed();// 可以理解成一个占位符   调用原来的方法去了  

            Console.WriteLine($"被标记方法执行后执行 invocation.Arguments:{invocation.Arguments[0]};{invocation.Arguments[1]}");

        }
    }

    [Intercept(typeof(CutomAutofacAop))] // Autofac 必须标注服务实例上面
    public interface ITestAutofac
    {
        public void Show(string name, int id);
    }


  
    public class TestAutofac : ITestAutofac
    {
        public void Show(string name, int id)
        {
            Console.WriteLine("当前方法的正常执行");
        }
    }

}
