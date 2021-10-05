using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MVCBikeShop.Config;
using MVCBikeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var bikeShopConfiguration = Configuration.GetConnectionString("BikeShopConnection");
            services.AddDbContext<BikeContext>(options => options.UseSqlServer(bikeShopConfiguration));

            var identityConfiguration = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(identityConfiguration));

            //services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = new PathString("/Account/Login");
            //        options.AccessDeniedPath = new PathString("/Account/Login");

            //    });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSURER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero//time token alive
                };
            });
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()/*.AddDefaultTokenProviders()*/;

            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
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
            //app.UseDefaultFiles();//TYT
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCors(builder => builder.AllowAnyOrigin());
            //app.UseCors(builder => builder.AllowAnyMethod());
            //app.UseCors(builder => builder.AllowAnyHeader());
            //app.UseCors(builder => builder.AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}

//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.IdentityModel.Tokens;
//using MVCBikeShop.Config;
//using MVCBikeShop.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//public class Startup
//{
//    public Startup(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    public IConfiguration Configuration { get; }

//    // This method gets called by the runtime. Use this method to add services to the container.
//    public void ConfigureServices(IServiceCollection services)
//    {
//        string connection = Configuration.GetConnectionString("DefaultConnection");
//        services.AddDbContext<PhoneContext>(options => options.UseSqlServer(connection));



//        services.AddAuthentication(options =>
//        {
//            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//        })
//        .AddJwtBearer(options =>
//        {
//            options.RequireHttpsMetadata = false;
//            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidIssuer = AuthOptions.ISSURER,
//                ValidateAudience = true,
//                ValidAudience = AuthOptions.AUDIENCE,
//                ValidateLifetime = true,
//                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//                ValidateIssuerSigningKey = true,
//                ClockSkew = TimeSpan.Zero
//            };
//        });

//        services.AddIdentity<User, IdentityRole>()
//            .AddEntityFrameworkStores<PhoneContext>()
//            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

//        services.AddControllersWithViews();
//        services.AddSession();
//    }

//    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }
//        else
//        {
//            app.UseExceptionHandler("/Home/Error");
//            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//            app.UseHsts();
//        }
//        app.UseStatusCodePages("text/html", "<center><h1>Sorry, but you have error: <b>404</b></h1></center>");
//        app.UseHttpsRedirection();
//        //app.UseDefaultFiles();
//        app.UseStaticFiles();

//        app.UseSession();
//        app.UseRouting();

//        app.UseAuthentication();
//        app.UseAuthorization();

//        app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");
//            endpoints.MapControllers();
//        });
//    }
//}
