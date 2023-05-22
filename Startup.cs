using appPrevencionRiesgos.Data;
using appPrevencionRiesgos.Data.Repository;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Services;
using appPrevencionRiesgos.Services.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appPrevencionRiesgos
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IUserInformationService, UserInformationService>();
            services.AddTransient<IUserInformationRepository, UserInformationRepository>();
            //services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IMongoDBServices, MongoDBService>();
            services.AddScoped<IMongoDBServices, MongoDBService>();

            //Map model and entitys (equality)
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });
            });

            var aux = Configuration.GetRequiredSection("MongoDB");
            MongoDBSettings dbConf = new MongoDBSettings();
            dbConf.ConnectionURI = aux["ConnectionURI"];
            dbConf.DatabaseName = aux["DatabaseName"];
            dbConf.CollectionName = aux["CollectionName"];
            //Console.WriteLine(dbConf);

            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDB"));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
