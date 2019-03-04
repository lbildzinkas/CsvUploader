using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvLoader.Data.Common.Settings;
using CsvLoader.Data.Factories.Implementations;
using CsvLoader.Data.Factories.Interfaces;
using CsvLoader.Services.Implementations;
using CsvLoader.Services.Interfaces;
using CsvUploader.Api.ExceptionHandling;
using CsvUploader.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddLogging();
            services.Configure<DatabaseSettings>(options => Configuration.GetSection("DatabaseSettings").Bind(options));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Product Upload Api", Version = "v1" });
            });

            services.AddTransient<IProductRepositoryFactory, ProductRepositoryFactory>();
            services.AddTransient<IJsonTextWriterFactory, JsonTextWriterFactory>();
            services.AddTransient<IMongoDatabaseFactory, MongoDatabaseFactory>();
            services.AddTransient<IProductUploadService, ProductUploadService>();
            services.AddTransient<IFactory, Factory>();
            services.AddTransient<IMultipartRequestUtilitiesProvider, MultipartRequestUtilitiesProvider>();

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;

            });
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Syndy.ProductsService v1");
            });

            app.UseMvc();
        }
    }
}
