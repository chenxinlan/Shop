using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Shop.EFramework;
using Shop.Backend.Shop.EFramework;

namespace Shop
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        //构造函数
        public Startup(IHostingEnvironment env)
        {
            //asp.net core中,使用json格式的配置文件进行系统相关参数的配置,将相关配置文件通过ConfigurationBuilder进行统一管理,得到IConfigurationRoot的配置实例.
            //获取相关配置文件配置节点的信息,想要使用配置文件相关服务,需要添加一下依赖.
            //Microsoft.Extensions.Configuration   Microsoft.Extensions.Configuration  Microsoft.Extensions.Configuration.Json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        //添加mvc服务
        // This method gets called by the runtime. Use this method to add services to the container.
        //译:这个方法被运行时调用,使用此方法将服务添加到容器中 by cxl
        //在ConfigureServices方法中获取数据库连接字符串,并添加数据库连接服务.
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            var sqlConnectionString= Configuration.GetConnectionString("DefaultConnection");

            //添加数据上下文
           
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ShopDbContext>(options => options.UseNpgsql(sqlConnectionString));

            // Add framework services.
            services.AddMvc();

            //对Swagger的支持(api)
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info
                {
                    Version = "v1",
                    Title = "My Web Application",
                    Description = "RESTful API for My Web Application",
                    TermsOfService = "None"
                });
                options.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "Shop.XML")); // 注意：此处替换成所生成的XML documentation的文件名。
                options.DescribeAllEnumsAsStrings();
            });

            //添加options
            services.AddOptions();
            services.Configure<SiteConfig>(Configuration.GetSection("SiteConfig"));
        }

        //配置Web应用程序使用配置和使用MVC路由（http请求管道配置）
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var data = Configuration["Data"];
            //两种方式读取配置文件
            var defaultcon = Configuration.GetConnectionString("DefaultConnection");
            var devcon = Configuration["ConnectionStrings:DevConnection"];

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();
            app.UseSwaggerUi();

            if (env.IsDevelopment())//开发版本 调试的时候
            {
                //开发环境异常
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else //生产版本 运行的时候
            {
                //生产环境异常
                app.UseExceptionHandler("/Home/Error");
            }

            //使用静态文件
            app.UseStaticFiles();

            //使用mvc,设置默认路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });

            SeedData.Initialize(app.ApplicationServices); //初始化数据

            //app.UseMvc();
        }
    }
}
