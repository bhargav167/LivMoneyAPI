using System;
using System.Text;
using AutoMapper;
using LivMoneyAPI.Data;
using LivMoneyAPI.EmailServices;
using LivMoneyAPI.Extension.AuthReponces;
using LivMoneyAPI.Helper;
using LivMoneyAPI.Repository.AuthenticationRepo;
using LivMoneyAPI.Repository.Crud;
using LivMoneyAPI.Repository.ProfileRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace LivMoneyAPI {
    public class Startup {
        private readonly IConfiguration _config;
        public Startup (IConfiguration config) {
            _config = config;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<DataContext> (s => s.UseSqlServer (_config.GetConnectionString ("DefaultConnection")));
            services.AddScoped<ICrudRepo, CrudRepo> ();
            services.AddScoped<IAuthRepo, AuthRepo> ();
            services.AddScoped<IProfileRepo, ProfileRepo> ();
            services.AddControllers ();
            services.AddCors ();
            services.Configure<CloudinarySettings> (_config.GetSection ("CloudinarySettings"));
            services.AddSingleton<IEmailSender, EmailSender> ();
            services.Configure<EmailOptions> (_config);
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration (mc => {
                mc.AddProfile (new Automapperprofiling ());
            });
            IMapper mapper = mappingConfig.CreateMapper ();
            AuthResponces responces= new AuthResponces();
            services.AddSingleton (mapper);
            services.AddSingleton (responces);
            services.AddSignalR ();
            services.AddControllers (option => { option.EnableEndpointRouting = false; })
                .SetCompatibilityVersion (CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson (options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver ();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            var key = Encoding.UTF8.GetBytes (_config["AppSettings:Token"].ToString ());

            services.AddAuthentication (x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (c => {
                c.RequireHttpsMetadata = false;
                c.SaveToken = false;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddControllersWithViews ()
                .AddNewtonsoftJson (options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();

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