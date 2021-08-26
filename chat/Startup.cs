using chat.DAL;
using chat.Exceptions;
using chat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace chat
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
            services.AddDbContext<ChatContext>(opt =>
                opt.UseNpgsql(Configuration.GetSection("ChatApp")["DbConnection"]));

            services.AddTransient<UserService>();
            services.AddTransient<AuthorizationService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "chat", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "chat v1"));
            }


            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var recievedException = context.Features.Get<IExceptionHandlerPathFeature>().Error;

                BasicException exception = new InternalErrorException(recievedException.Message);

                if (recievedException is BasicException)
                {
                    exception = (BasicException)recievedException;
                }

                string result = JsonConvert.SerializeObject(new
                {
                    Status = exception.Status,
                    Code = exception.Code,
                    Message = exception.Message
                });

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)exception.Status;

                await context.Response.WriteAsync(result);
            }));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
