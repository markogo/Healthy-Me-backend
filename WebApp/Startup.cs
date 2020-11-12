#pragma warning disable 1591
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Text;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;

namespace WebApp
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
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MSSqlLocalDbConnection")));
            } 
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
            }

            services.AddScoped<IUserNameProvider, UserNameProvider>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();
            
            //services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            // ===================== JWT SUPPORT =====================
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SigningKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            
            services.AddVersionedApiExplorer( options => options.GroupNameFormat = "'v'VVV" );
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            UpdateDatabase(app, env, Configuration);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsAllowAll");

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach ( var description in provider.ApiVersionDescriptions )
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant() );
                    }
                } );
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "area",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
        private static void UpdateDatabase( IApplicationBuilder app, IWebHostEnvironment env, IConfiguration Configuration)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var ctx = serviceScope.ServiceProvider.GetService<AppDbContext>();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();


            if (Configuration["AppDataInitialization:DropDatabase"] == "True")
            {
                Console.WriteLine("DropDatabase");
                DAL.App.EF.Helpers.DataInitializers.DeleteDatabase(ctx!);
            }

            if (Configuration["AppDataInitialization:MigrateDatabase"] == "True")
            {
                Console.WriteLine("MigrateDatabase");
                DAL.App.EF.Helpers.DataInitializers.MigrateDatabase(ctx!);
            }

            if (Configuration["AppDataInitialization:SeedIdentity"] == "True")
            {
                Console.WriteLine("SeedIdentity");
                DAL.App.EF.Helpers.DataInitializers.SeedIdentity(userManager!, roleManager!);
            }

            if (Configuration.GetValue<bool>("AppDataInitialization:SeedData"))
            {
                Console.WriteLine("SeedData");
                DAL.App.EF.Helpers.DataInitializers.SeedData(ctx!, userManager!);
            }
        }
    }
}