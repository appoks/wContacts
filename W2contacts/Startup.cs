using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpsPolicy;

namespace W2contacts
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

            services.AddDbContext<ContactsContext>(x => x.UseNpgsql("Server=tuffi.db.elephantsql.com;Port=5432;Database=nloyjybp;User Id=nloyjybp;Password=SzXsBxy32mb-LiZNXpOI_nyrcZ5ebmR9;"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                services.AddOptions();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // enables deploying SPA files inside wwwroot folder   
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // ###TODO : Is cors still needed now I moved SPA files to wwwroot?
            app.UseCors("CorsPolicy");

            // ??? 
            app.UseMvc();
        }
    }
}
