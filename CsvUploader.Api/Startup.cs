using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvLoader.Data.Common.Settings;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Services.Implementations;
using CsvLoader.Services.Interfaces;
using CsvUploader.Api.ExceptionHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CsvUploader.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLogging();
            services.Configure<DatabaseSettings>(options => Configuration.GetSection("DatabaseSettings").Bind(options));

            services.AddTransient<IProductRepositoryFactory, ProductRepositoryFactory>();
            services.AddTransient<IMongoDatabaseFactory, MongoDatabaseFactory>();
            services.AddTransient<IProductUploadService, ProductUploadService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
                app.UseExceptionHandler();
            }

            app.UseMvc();
        }
    }
}
