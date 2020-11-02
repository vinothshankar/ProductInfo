using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductInfo.API;
using ProductInfo.Business;
using ProductInfo.DAL;
using ProductInfo.Data.Entities;
using ProductInfo.Utils;
using ProductInfo.Utils.Interfaces.Business;
using ProductInfo.Utils.Interfaces.Data;

namespace ProductInfo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Constants.DBConnectionString = Configuration["DBConnectionString:SQLConnectionString"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<DbContext, ProductInfoContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
