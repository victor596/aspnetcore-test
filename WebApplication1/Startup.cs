using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Culture;

namespace WebApplication1
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
            services.AddMvc();
            
        }
        private static void HandleMapTest1(IApplicationBuilder app)
        {
            //这样会挡掉原来的请求
            
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
            

        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }
        /*
         配置 HTTP 管道使用Use， Run，和Map。 Use方法则可以绕过管道 (即，如果它未调用next请求委托)。 Run是一种约定，并且可能会使某些中间件组件Run[Middleware]管道结束时运行的方法。
             */
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRequestCulture();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                System.IO.FileInfo fi = new System.IO.FileInfo("a.txt");
                using (var ow = fi.AppendText())
                {
                    ow.WriteLine(context.Request.Path.Value);
                }
                    await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });
            //app.Map("/api/values", HandleMapTest1); //要使MIDDLEWARE起作用要放到app.UseMvc前而
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            //app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        

            app.Map("/map2", HandleMapTest2);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
            });
        }
    }
}
