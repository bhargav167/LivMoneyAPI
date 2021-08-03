using AutoMapper;
using LivMoneyAPI.Data;
using LivMoneyAPI.Helper;
using LivMoneyAPI.Repository.AuthenticationRepo;
using LivMoneyAPI.Repository.Crud;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace LivMoneyAPI
{
    public class Startup
    {
       private readonly IConfiguration _config;
        public Startup (IConfiguration config) {
            _config = config;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
              services.AddDbContext<DataContext> (s => s.UseSqlServer (_config.GetConnectionString ("DefaultConnection")));
            services.AddScoped<ICrudRepo, CrudRepo> (); 
            services.AddScoped<IAuthRepo, AuthRepo> (); 
            services.AddControllers ();
            services.AddCors ();
            services.Configure<CloudinarySettings> (_config.GetSection ("CloudinarySettings"));
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration (mc => {
                mc.AddProfile (new Automapperprofiling ());
            });
            IMapper mapper = mappingConfig.CreateMapper ();
            services.AddSingleton (mapper);
            services.AddSignalR ();
            services.AddControllers (option => { option.EnableEndpointRouting = false; })
                .SetCompatibilityVersion (CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson (options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver ();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddControllersWithViews ()
                .AddNewtonsoftJson (options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddAuthentication ().AddGoogle (options => {
                options.ClientId = "523248943342-e3j50o2fojm6fujhck86a3h8bd9ehuho.apps.googleusercontent.com";
                options.ClientSecret = "UqqppOAaSX1okn-xxPaaRuH-";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
              app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ()
                .AllowAnyHeader ()
            );

            app.UseAuthentication ();
            app.UseAuthorization ();

            //    app.UseDefaultFiles ();
            app.UseStaticFiles ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
                endpoints.MapFallbackToController ("Index", "Fallback");
            });
        }
    }
}
