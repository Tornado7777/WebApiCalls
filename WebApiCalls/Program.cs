using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiCalls.Data;
using WebApiCalls.Services;
using WebApiCalls.Services.Impl;
using System;
using WebApiCalls.Models.Requests;
using WebApiCalls.Models;
using Microsoft.Extensions.Hosting;
using WebApiCalls.Models.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WebApiCalls
{
    public class Program
    {



        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5000);
            });



            #region Configure EF DBContext Service (EmployeeServiceDB Database)

            builder.Services.AddDbContext<WebApiCallsDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefualtConnection"));
            });

            //Not use Datetime UTC format
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            #endregion

            #region Configure Logging Services

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });



            #endregion

            #region Configure Services

            builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();
            
            #endregion

            #region Configure repository

            builder.Services.AddScoped<ICallsRepository, CallRepository>();
            builder.Services.AddScoped<IContactsRepository, ContactRepository>();

            #endregion

            #region Configure Authenticate 

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new
                TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticateService.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion

            #region Configure FluentValidator

            builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
            builder.Services.AddScoped<IValidator<ContactDto>, ContactDtoValidator>();
            builder.Services.AddScoped<IValidator<CreateCallRequest>, CreateCallRequestValidator>();
            builder.Services.AddScoped<IValidator<CallDto>, CallDtoValidator>();

            #endregion

            #region  CORS
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "MyPolicy",
            //                policy =>
            //                {
            //                    policy.WithOrigins(
            //                        "http://localhost:3000",
            //                        "http://localhost:5000")
            //                            .WithMethods("PUT", "DELETE", "GET", "POST")
            //                            .SetIsOriginAllowedToAllowWildcardSubdomains()
            //                            .AllowAnyHeader();
            //                });
            //});
            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API сервиса учета контактов и звонков",
                    Description = "API сервиса учета контактов и звонков",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ivan",
                        Email = string.Empty,
                        Url = new Uri("https://nn.hh.ru/resume/83aa3383ff09d42d850039ed1f7247336c6664?hhtmFrom=resume_list"),
                    },
                    
                });

                //Set the comments path for the swagger json and ui.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });



                 
                
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            //app.UseCors();
            app.UseAuthentication();



            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}