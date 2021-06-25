using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Ruanmou.Core30
{
    public class Program
    {
        /// <summary>
        /// 欢迎大家来到  .Net 进阶学习，在本次课堂的学习中，如果有任何的疑问或者有更多的学习需求，请联系
        /// QQ:57265177
        ///  
        /// .Net Core 3.0 整合Autofac
        ///  
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
