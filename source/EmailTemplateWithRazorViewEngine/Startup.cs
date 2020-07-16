using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailTemplateWithRazorViewEngine.AppSettings;
using EmailTemplateWithRazorViewEngine.Interfaces;
using EmailTemplateWithRazorViewEngine.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailTemplateWithRazorViewEngine
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
            services.AddRazorPages();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            var emailSettings = Configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();
            services.AddSingleton(emailSettings);
            services.AddTransient<IEmailService, EmailService>();
            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
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
            });
            app.UseHangfireServer();
        }
    }
}
