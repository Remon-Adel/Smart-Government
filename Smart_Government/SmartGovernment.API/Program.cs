using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartGovernment.Core.Entities;
using SmartGovernment.Core.Repositories;
using SmartGovernment.Core.Services;
using SmartGovernment.Repository;
using SmartGovernment.Repository.Data;
using SmartGovernment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGovernment.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            #region Configure Service


            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartGovernment.API", Version = "v1" });
            });
            builder.Services.AddDbContext<SmartContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<appuser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<SmartContext>();
            //.AddTokenProvider<DataProtectorTokenProvider<appuser>>(TokenOptions.DefaultProvider);

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
            builder.Services.AddScoped<IUserMinistryRepository, UserMinistryRepository>();



            builder.Services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/ options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["jwt:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["jwt:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
                    };
                }
                );

            //services.AddScoped<UserManager<appuser>, UserManager<appuser>>();



            #endregion

            var app = builder.Build();

            #region Apply Migration And Data Seeding
           

            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                var Context = Services.GetRequiredService<SmartContext>();
                await Context.Database.MigrateAsync();

                await SmartContextSeed.SeedAsync(Context, LoggerFactory);

                var Usermanager = Services.GetRequiredService<UserManager<appuser>>();


            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Ocurred Durring Apply Migration");

            }
            #endregion

            #region Configure Http Request Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartGovernment.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #endregion


            app.Run();







        }


    }
}
