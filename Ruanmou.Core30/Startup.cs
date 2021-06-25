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


        ///     1�� .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        ///     2��  public void ConfigureContainer(ContainerBuilder containerBuilder)
        ///     3��  ָ�����������ڿ������л�ȡ����ʵ�� 
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //#region ָ��������Ҳ��autofac ������ʵ����ȡ 
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

            #region ��ͨ����ע��
            ////   ָ���˷����ע��
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

            #region Autofac ��������

            //// Autofac�������ڣ� ������������
            //// ˲ʱ�������ڣ�ע��֮���أ�ÿ�λ�ȡ�ķ���ʵ������һ��
            //containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>().InstancePerDependency();
            ////���������ʹ�õ�����������ע�ᣬ ��ô�����������л�ȡ�ķ���ʵ������ͬһ����
            //containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>().SingleInstance();
            ////������ �������ڣ����ʹ����������������ע�ᣬ��ôֻ������ͬ�������»�ȡ���ķ���ʵ��������ͬ��
            //containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>().InstancePerLifetimeScope();
            //// �������������ڣ� ���������һ�֣����Ը��Ӿ�ȷ��ָ����ĳһ��������
            //containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>().InstancePerMatchingLifetimeScope("MyRequest");

            //////ĳ���������������  ��ͬ�������ȡ�ķ���ʵ����һ��
            ////containerBuilder.RegisterType<TestServiceF>().As<ITestServiceF>().InstancePerRequest();
            //////ӵ����ʿ��ϵ ���͵Ĵ����µ�Ƕ���������ڵ������򣬿���ʹ��ÿһ��ӵ��ʵ����ע����������ϵ�޶���ӵ�е�ʵ�� 
            ////containerBuilder.RegisterType<TestServiceB>().InstancePerOwned<ITestServiceG>();

            //IContainer container = containerBuilder.Build();
            //ITestServiceA testServiceA1 = container.Resolve<ITestServiceA>();
            //ITestServiceA testServiceA2 = container.Resolve<ITestServiceA>();
            //Console.WriteLine($"testServiceA1��testServiceA2 �Ƿ���ͬ��{object.ReferenceEquals(testServiceA1, testServiceA2)}");

            //ITestServiceB TestServiceB1 = container.Resolve<ITestServiceB>();
            //ITestServiceB TestServiceB2 = container.Resolve<ITestServiceB>();
            //Console.WriteLine($"TestServiceB1��TestServiceB2�Ƿ���ͬ��{object.ReferenceEquals(TestServiceB1, TestServiceB2)}");

            //ITestServiceC TestServiceC1 = container.Resolve<ITestServiceC>();
            //ITestServiceC TestServiceC2 = container.Resolve<ITestServiceC>();
            //Console.WriteLine($"TestServiceC1��TestServiceC2�Ƿ���ͬ��{object.ReferenceEquals(TestServiceC1, TestServiceC2)}");

            //var scope1 = container.BeginLifetimeScope();
            //ITestServiceC TestServiceC3 = scope1.Resolve<ITestServiceC>();
            //ITestServiceC TestServiceC4 = scope1.Resolve<ITestServiceC>();
            //Console.WriteLine($"TestServiceC3��TestServiceC4�Ƿ���ͬ��{object.ReferenceEquals(TestServiceC3, TestServiceC4)}");

            //Console.WriteLine($"TestServiceC1��TestServiceC3�Ƿ���ͬ��{object.ReferenceEquals(TestServiceC1, TestServiceC3)}");

            //using (var inner = container.BeginLifetimeScope("MyRequest"))
            //{
            //    ITestServiceD TestServiceD1 = inner.Resolve<ITestServiceD>();
            //    ITestServiceD TestServiceD2 = inner.Resolve<ITestServiceD>();
            //    Console.WriteLine($"TestServiceD1��TestServiceD2�Ƿ���ͬ��{object.ReferenceEquals(TestServiceD1, TestServiceD2)}");

            //    using (var inner1 = container.BeginLifetimeScope("MyRequest"))
            //    {
            //        ITestServiceD TestServiceD3 = inner1.Resolve<ITestServiceD>();
            //        ITestServiceD TestServiceD4 = inner1.Resolve<ITestServiceD>();

            //        Console.WriteLine($"TestServiceD3��TestServiceD4�Ƿ���ͬ��{object.ReferenceEquals(TestServiceD3, TestServiceD4)}");

            //        Console.WriteLine($"TestServiceD1��TestServiceD3�Ƿ���ͬ��{object.ReferenceEquals(TestServiceD1, TestServiceD3)}");

            //    }
            //}
            #endregion

            #region Module  ���԰ѷ���ע�Ჿ�ָ��뿪��

            // 1�� ���һ�� CustomAutofacModule ����Autofac.Module
            // 2�� ��дLoad ����
            // 3�� ��ConfigureContainer��Ĵ���ᵽ CustomAutofacModule ��Load������
            // 4�� ��ConfigureContainer   containerBuilder.RegisterModule<CustomAutofacModule>();
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
