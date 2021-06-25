using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.Indexed;
using Autofac.Features.OwnedInstances;
using Autofac.Integration.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ruanmou.Core.Interface;
using Ruanmou.Core.Service;
using Ruanmou.Core30.Utility;

namespace Ruanmou.Core30
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }


        ///     1、 .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        ///     2、  public void ConfigureContainer(ContainerBuilder containerBuilder)
        ///     3、  指定允许允许在控制器中获取服务实例 
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //#region 指定控制器也由autofac 来进行实例获取 
            //var assembly = this.GetType().GetTypeInfo().Assembly;
            //var builder = new ContainerBuilder();
            //var manager = new ApplicationPartManager();
            //manager.ApplicationParts.Add(new AssemblyPart(assembly));
            //manager.FeatureProviders.Add(new ControllerFeatureProvider());
            //var feature = new ControllerFeature();
            //manager.PopulateFeature(feature);
            //builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            //builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();
            //#endregion

            #region 普通服务注册
            ////   指定了服务的注册
            // containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>();
            // containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();
            // containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>();
            // containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>();

            // IContainer container = containerBuilder.Build();
            // ITestServiceA testServiceA = container.Resolve<ITestServiceA>();
            // ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
            // ITestServiceC testServiceC = container.Resolve<ITestServiceC>();
            // ITestServiceD testServiceD = container.Resolve<ITestServiceD>();
            // testServiceA.Show();
            // testServiceB.Show();
            // testServiceC.Show();
            // testServiceD.Show(); 
            #endregion

            #region Autofac 生命周期

            //// Autofac生命周期： 六种生命周期
            //// 瞬时声明周期：注册之后呢，每次获取的服务实例都不一样
            //containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>().InstancePerDependency();
            ////单例：如果使用单例生命周期注册， 那么在整个容器中获取的服务实例都是同一个；
            //containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>().SingleInstance();
            ////作用域 生命周期，如果使用作用域生命周期注册，那么只有在相同作用域下获取到的服务实例才是相同的
            //containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>().InstancePerLifetimeScope();
            //// 作用域生命周期， 相比于上面一种，可以更加精确的指定到某一个作用域
            //containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>().InstancePerMatchingLifetimeScope("MyRequest");

            //////某个请求的生命周期  不同的请求获取的服务实例不一样
            ////containerBuilder.RegisterType<TestServiceF>().As<ITestServiceF>().InstancePerRequest();
            //////拥有隐士关系 类型的创建新的嵌套生命周期的作用域，可以使用每一个拥有实例的注册来依赖关系限定到拥有的实例 
            ////containerBuilder.RegisterType<TestServiceB>().InstancePerOwned<ITestServiceG>();

            //IContainer container = containerBuilder.Build();
            //ITestServiceA testServiceA1 = container.Resolve<ITestServiceA>();
            //ITestServiceA testServiceA2 = container.Resolve<ITestServiceA>();
            //Console.WriteLine($"testServiceA1和testServiceA2 是否相同：{object.ReferenceEquals(testServiceA1, testServiceA2)}");

            //ITestServiceB TestServiceB1 = container.Resolve<ITestServiceB>();
            //ITestServiceB TestServiceB2 = container.Resolve<ITestServiceB>();
            //Console.WriteLine($"TestServiceB1和TestServiceB2是否相同：{object.ReferenceEquals(TestServiceB1, TestServiceB2)}");

            //ITestServiceC TestServiceC1 = container.Resolve<ITestServiceC>();
            //ITestServiceC TestServiceC2 = container.Resolve<ITestServiceC>();
            //Console.WriteLine($"TestServiceC1和TestServiceC2是否相同：{object.ReferenceEquals(TestServiceC1, TestServiceC2)}");

            //var scope1 = container.BeginLifetimeScope();
            //ITestServiceC TestServiceC3 = scope1.Resolve<ITestServiceC>();
            //ITestServiceC TestServiceC4 = scope1.Resolve<ITestServiceC>();
            //Console.WriteLine($"TestServiceC3和TestServiceC4是否相同：{object.ReferenceEquals(TestServiceC3, TestServiceC4)}");

            //Console.WriteLine($"TestServiceC1和TestServiceC3是否相同：{object.ReferenceEquals(TestServiceC1, TestServiceC3)}");

            //using (var inner = container.BeginLifetimeScope("MyRequest"))
            //{
            //    ITestServiceD TestServiceD1 = inner.Resolve<ITestServiceD>();
            //    ITestServiceD TestServiceD2 = inner.Resolve<ITestServiceD>();
            //    Console.WriteLine($"TestServiceD1和TestServiceD2是否相同：{object.ReferenceEquals(TestServiceD1, TestServiceD2)}");

            //    using (var inner1 = container.BeginLifetimeScope("MyRequest"))
            //    {
            //        ITestServiceD TestServiceD3 = inner1.Resolve<ITestServiceD>();
            //        ITestServiceD TestServiceD4 = inner1.Resolve<ITestServiceD>();

            //        Console.WriteLine($"TestServiceD3和TestServiceD4是否相同：{object.ReferenceEquals(TestServiceD3, TestServiceD4)}");

            //        Console.WriteLine($"TestServiceD1和TestServiceD3是否相同：{object.ReferenceEquals(TestServiceD1, TestServiceD3)}");

            //    }
            //}
            #endregion

            #region Module  可以把服务注册部分隔离开来

            // 1、 添加一个 CustomAutofacModule 集成Autofac.Module
            // 2、 覆写Load 方法
            // 3、 把ConfigureContainer里的代码搬到 CustomAutofacModule 的Load方法里
            // 4、 在ConfigureContainer   containerBuilder.RegisterModule<CustomAutofacModule>();
            containerBuilder.RegisterModule<CustomAutofacModule>();
             
            #endregion 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Second}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
