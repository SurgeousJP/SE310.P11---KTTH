﻿using BookCatalog.API.Infrastructure;
using BookCatalog.API.Middleware;
using BookCatalog.API.Model;
using BookCatalog.API.Repositories;
using BookCatalog.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BookCatalog.API.Extensions
{
    public static class Extension
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            // Add env 
            builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // Swagger genereator
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT token with the prefix Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
            }); ;

            // Add repositories
            builder.Services.AddScoped<GenreRepository>();
            builder.Services.AddScoped<BookRepository>();
            // Add Db context for catalog
            builder.Services.AddDbContext<BookContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration["ConnectionStrings:BookCatalog"
                    ]);
                options.EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public static void AddJwtAuthentication
    (this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("jwt");
            var options = section.Get<JwtOptions>();
            var key = Encoding.UTF8.GetBytes(options.Secret);
            section.Bind(options);
            services.Configure<JwtOptions>(section);

            services.AddSingleton<IJwtBuilder, JwtBuilder>();
            services.AddTransient<JwtMiddleware>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(x =>
            {
                x.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }
    }
}
