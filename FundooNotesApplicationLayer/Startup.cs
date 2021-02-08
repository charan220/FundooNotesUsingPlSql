using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooNotesManagerLayer.IManager;
using FundooNotesManagerLayer.Manager;
using FundooNotesRepositoryLayer.IRepository;
using FundooNotesRepositoryLayer.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;


namespace FundooNotesApplicationLayer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<INotesManger, NotesManager>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<ILabelManager, LabelManager>();
            services.AddTransient<IlabelRepository, LabelRepository>();
            services.AddSwaggerGen(c =>
            {
               c.SwaggerDoc("v1", new Info { Title = "Fundoo API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
