using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Ruanmou.Core.Interface;
using Ruanmou.Core.Service;

namespace Ruanmou.Core30.Utility
{
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            #region 指定控制器也由autofac 来进行实例获取 
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();
            #endregion

            #region 指定了服务的注册
            ////   指定了服务的注册
            //containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>();
            //containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>() ;// 表示当前
            //containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>();
            //containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>();

            //通过反射程序集注册一个接口多个实现和获取指定服务
            //containerBuilder.RegisterAssemblyTypes(Assembly.Load("Ruanmou.Core.Service")).As<ITestServiceD>();
            //containerBuilder.RegisterAssemblyTypes(Assembly.Load("Ruanmou.Core.Service")).Where(a => a.Name.EndsWith("_One")).As<ITestServiceD>();
            //containerBuilder.RegisterAssemblyTypes(Assembly.Load("Ruanmou.Core.Service")).Except<TestServiceD_One>().Except<TestServiceD_Two>().Except<TestServiceD_Three>().As<ITestServiceD>();

            //containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>().PreserveExistingDefaults();
            //containerBuilder.RegisterType<TestServiceD_One>().As<ITestServiceD>().PreserveExistingDefaults();  // 当前非默认获取
            //containerBuilder.RegisterType<TestServiceD_Two>().As<ITestServiceD>().PreserveExistingDefaults();
            //containerBuilder.RegisterType<TestServiceD_Three>().As<ITestServiceD>();
            //--------------------------------------------------------------------------------------------

            //在注册一个接口多实现的时候，给注册的服务指定一个名称
            // 使用Named方法，注册的时候需要定义一个名字
            // 获取服务就需要使用ResolveNamed(),就需要把名称和注册的时候定义的名称对应起来 
            //containerBuilder.RegisterType<TestServiceD>().Named<ITestServiceD>("TestServiceD");
            //containerBuilder.RegisterType<TestServiceD_One>().Named<ITestServiceD>("TestServiceD_One");
            //containerBuilder.RegisterType<TestServiceD_Two>().Named<ITestServiceD>("TestServiceD_Two");
            //containerBuilder.RegisterType<TestServiceD_Three>().Named<ITestServiceD>("TestServiceD_Three"); 
            //IContainer container = containerBuilder.Build(); 
            //ITestServiceD testServiceD= container.ResolveNamed<ITestServiceD>("TestServiceD");
            //ITestServiceD testServiceD_One = container.ResolveNamed<ITestServiceD>("TestServiceD_One");
            //ITestServiceD testServiceD_Two = container.ResolveNamed<ITestServiceD>("TestServiceD_Two");
            //ITestServiceD testServiceD_Three = container.ResolveNamed<ITestServiceD>("TestServiceD_Three");

            // 通过键值的方式注册和获取服务 


            // 1、需要指定键值  是一个Object类型
            // 2、注册服务使用方法Keyed  参数为指定的键值中的值 （每一个服务的实现和键值要一一对应起来，这里不能重复）
            // 3、 获取服务： 直接通过ResolveKeyed() 获取服务无，方法需要传入 指定对应的键值
            //     先获取一个IIndex，再通过IInex 索引来获取服务的实例

            //containerBuilder.RegisterType<TestServiceD>().Keyed<ITestServiceD>(DeviceState.TestServiceD);
            //containerBuilder.RegisterType<TestServiceD_One>().Keyed<ITestServiceD>(DeviceState.TestServiceD_One);
            //containerBuilder.RegisterType<TestServiceD_Two>().Keyed<ITestServiceD>(DeviceState.TestServiceD_Two);
            //containerBuilder.RegisterType<TestServiceD_Three>().Keyed<ITestServiceD>(DeviceState.TestServiceD_Three);
            //IContainer container = containerBuilder.Build();

            ////ITestServiceD testServiceD = container.ResolveKeyed<ITestServiceD>(DeviceState.TestServiceD);
            ////ITestServiceD testServiceD_One = container.ResolveKeyed<ITestServiceD>(DeviceState.TestServiceD_One);
            ////ITestServiceD testServiceD_Two = container.ResolveKeyed<ITestServiceD>(DeviceState.TestServiceD_Two);
            ////ITestServiceD testServiceD_Three = container.ResolveKeyed<ITestServiceD>(DeviceState.TestServiceD_Three);

            //IIndex<DeviceState, ITestServiceD> index = container.Resolve<IIndex<DeviceState, ITestServiceD>>();

            //ITestServiceD testServiceD= index[DeviceState.TestServiceD];
            //ITestServiceD TestServiceD_One = index[DeviceState.TestServiceD_One];
            //ITestServiceD TestServiceD_Two = index[DeviceState.TestServiceD_Two];
            //ITestServiceD TestServiceD_Three = index[DeviceState.TestServiceD_Three];
            #endregion


            #region Autofac 基于配置文件的服务注册
            //JSON格式配置文件
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder(); // 配置文件的读取器 
            configurationBuilder.AddJsonFile("autofac.json");
            IConfigurationRoot root = configurationBuilder.Build();

            // 开始读取配置文件里的内容信息
            ConfigurationModule module = new ConfigurationModule(root);
            // 根据配置文件的内容注册服务
            containerBuilder.RegisterModule(module);

            //IContainer container= containerBuilder.Build();

            //ITestServiceA testServiceA = container.Resolve<ITestServiceA>(); 
            //ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
            //ITestServiceC testServiceC = container.Resolve<ITestServiceC>();
            //ITestServiceD testServiceD = container.Resolve<ITestServiceD>();
            #endregion


            #region Autofac 支持Aop

            // 1、 引入程序集 Autofac.Extras.DynamicProxy
            // 2、自定义Aop列 集成 IInterceptor  实现接口IInterceptor的方法
            // 3、把Aop类标记到接口或者实现上

            containerBuilder.Register(a => new CutomAutofacAop()) ; // 表示允许Autofac 使用Aop 
            containerBuilder.RegisterType<TestAutofac>().As<ITestAutofac>().EnableInterfaceInterceptors(); 
            #endregion



        }

    }
}
