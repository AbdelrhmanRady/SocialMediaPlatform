using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Social_Media_Platform.Models;
using Social_Media_Platform.Security;
using Social_Media_Platform.Services;
using Social_Media_Platform.Settings;
using System.Security.Claims;

namespace Social_Media_Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "1037727137911-2fhm7uhi9ns4e6nt8da2hvusotdggrgp.apps.googleusercontent.com";
                options.ClientSecret = "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";
            }).AddFacebook(options =>
            {
                options.ClientId = "1612063962702622";
                options.ClientSecret = "0afcd11c5e0b9a66d6afceecc3bbbfbf";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("EmailConfirmed", policy =>
                {
                    policy.AddRequirements(new ConfirmEmailRequirement());
                });
            });

            builder.Services.AddScoped<IAuthorizationHandler,ConfirmEmailHandler>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMediaDBConnection"));
            });
            builder.Services.AddTransient<IMailingService,MailingService>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(24);
            });

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}