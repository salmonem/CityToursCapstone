using System;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using FinalCapstone.Dal.IDAL;

namespace FinalCapstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Sets session expiration to 20 minuates
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.Name = ".FinalCapstone.Session";
                options.Cookie.HttpOnly = true;
            });

            services.AddScoped<ILandmarkDAL>(x => new LandmarkDAL(Configuration.GetConnectionString("CityToursDb")));
            services.AddScoped<INeighborhoodDAL>(x => new NeighborhoodDAL(Configuration.GetConnectionString("CityToursDb")));
            services.AddScoped<IItineraryDAL>(x => new ItineraryDAL(Configuration.GetConnectionString("CityToursDb")));
            services.AddScoped<ILocationDAL>(x => new LocationDAL(Configuration.GetConnectionString("CityToursDb")));
            services.AddScoped<IReviewDAL>(x => new ReviewDAL(Configuration.GetConnectionString("CityToursDb")));
            services.AddScoped<IVoteDAL>(x => new VoteDAL(Configuration.GetConnectionString("CityToursDb")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //CreateRoles(serviceProvider).Wait()/*;*/
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User", "HR" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("jignesh@gmail.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "adminKilo@gmail.com",
                    Email = "adminKilo@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");

            IdentityUser user1 = await UserManager.FindByEmailAsync("tejas@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "teamKilo@gmail.com",
                    Email = "teamKilo@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
            await UserManager.AddToRoleAsync(user1, "User");


        }

    }
}

