
using System.Text;
using Application.ExternalService.Implementation;
using Application.ExternalService.Interface;
using Application.Service.Implementation;
using Application.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IChatbotService, ChatbotService>();
            builder.Services.AddTransient<IGraderService, GraderService>();
            builder.Services.AddTransient<IProductExternalService, ProductExternalService>();
            builder.Services.AddTransient<IProgressExternalService, ProgressExternalService>();


            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "API có Authentication v?i JWT"
                });

                // add Security Definition cho Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Input JWT token  format: {token}"
                });

                // add Security Requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // add auth author

            var jwtConfig = builder.Configuration.GetSection("JWT");
            var jwtKey = Encoding.UTF8.GetBytes(jwtConfig["Key"] ?? throw new Exception("Missing jwt key"));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtConfig["Audience"],

                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig["Issuer"],

                        //ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
                    };


                }
            );

            // add Logging
            builder.Logging.AddConsole();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
