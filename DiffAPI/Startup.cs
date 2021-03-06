﻿using DiffAPI.Repository;
using DiffAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DiffAPI
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
            //Adds Context to API using default connection string
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DatabaseContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
            
            //Adds Swagger service to API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "JSON Diff", Version = "v1" });
            });
            
            //Dependency Injection Registries
            services.AddTransient<IJsonRepository, JsonRepository>();
            services.AddTransient<IDiffService, DiffService>();
            services.AddTransient<IEncodeService, EncodeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //Adds Swagger service to API
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JSON Diff");
            });
            
            app.UseMvc();
        }
    }
}
