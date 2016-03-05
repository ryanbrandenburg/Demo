using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;

namespace Demo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddViewLocalization();

            services.AddTransient<IStringLocalizerFactory, Localization.DBStringLocalizerFactory>();
            services.AddSingleton<Localization.LocalizationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            // Configure the Localization middleware after ASP.NET Identity has been configured
            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                    new CultureInfo("pl"),
                },
                SupportedUICultures = new List<CultureInfo>()
                {
                    new CultureInfo("en"),
                    new CultureInfo("fr"),
                    new CultureInfo("pl"),
                },
                DefaultRequestCulture = new RequestCulture("en"),
                RequestCultureProviders = new List<IRequestCultureProvider>()
            };
            requestLocalizationOptions.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider() { Options = requestLocalizationOptions });
            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseDefaultConfiguration(args)
                .UseServer("Microsoft.AspNetCore.Server.Kestrel")
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
