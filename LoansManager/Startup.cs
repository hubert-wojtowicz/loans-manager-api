using Autofac;
using Autofac.Extensions.DependencyInjection;
using LoansManager.DAL;
using LoansManager.Services.Infrastructure;
using LoansManager.Services.Infrastructure.IoC;
using LoansManager.Services.Infrastructure.IoC.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace LoansManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static IContainer AutofacContainer { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the Description.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc(
                        Configuration["Api:Name"], 
                        new Info
                        {
                            Title = Configuration["Api:Title"],
                            Version = Configuration["Api:Version"],
                            Description = Configuration["Api:Description"]
                        });
                })
                .AddMvc(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddSingleton(AutomapperConfig.Initialize())
                .AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    };
                });

            var builder = new ContainerBuilder();
            AutofacConfig.Register(builder);
            builder.Populate(services);
            builder.RegisterModule(new SettingsModule(Configuration));
            builder.RegisterModule<CommandModule>();
            AutofacContainer = builder.Build();

            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication()
               .UseHttpsRedirection()
               .UseMvc()
               .UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"/swagger/{Configuration["Api:Name"]}/swagger.json", Configuration["Api:Name"]);
               });

            applicationLifetime.ApplicationStopped.Register(() => AutofacContainer.Dispose());
        }
    }
}
