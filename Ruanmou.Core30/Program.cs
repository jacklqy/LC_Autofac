using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Ruanmou.Core30
{
    public class Program
    {
        /// <summary>
        /// ��ӭ�������  .Net ����ѧϰ���ڱ��ο��õ�ѧϰ�У�������κε����ʻ����и����ѧϰ��������ϵ
        /// QQ:57265177
        ///  
        /// .Net Core 3.0 ����Autofac
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
