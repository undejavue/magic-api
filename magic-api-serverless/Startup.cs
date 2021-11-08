using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Amazon.DynamoDBv2;
using magic_api_serverless.api;
using magic_api_serverless.api.Handlers;
using magic_api_serverless.api.Repositories;
using magic_api_serverless.core.Common;
using magic_api_serverless.core.Models;
using Microsoft.OpenApi.Models;

namespace magic_api_serverless
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var awsOptions = Configuration.GetAWSOptions();

            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddTransient<IDynamoRepository<UserModel>, UserRepository>();
            services.Configure<DynamoSettings>(Configuration.GetSection("DynamoDb"));
            services.AddTransient<IHandler<UserModel, string>, CreateUserHandler>();
            services.AddTransient<IHandler<string, UserModel>, GetUserHandler>();
            services.AddTransient<IHandler<IEnumerable<UserModel>>, GetUserListHandler>();
            services.AddTransient<IHandler<string, bool>, DeleteUserHandler>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("ApiKey", new ()
                {
                    Description =
                        "Api-Key is required. Example: \"ApiKey: {token}\"",
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                            Scheme = "oauth2",
                            Name = "ApiKey",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}
