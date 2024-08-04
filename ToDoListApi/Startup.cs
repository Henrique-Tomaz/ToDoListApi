using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DataAccess.Entities;
using Domain.Interfaces;
using Services;

namespace ToDoListApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ToDoItemAPI",
                        Description = "O objetivo desta documentação é servir de guia para o desenvolvedor sobre como integrar com a API, descrevendo os métodos disponíveis com exemplos de objetos como requisição, resposta e dos verbos http utilizados.",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Desenvolvimento Henrique",
                            Email = "Henricobello@hotmail.com",
                            Url = new Uri("https://www.microsoft.com/pt-br")
                        }
                    });
            });

            services.AddControllers();
            #region DataBase
            var optionsBuilder = new DbContextOptionsBuilder<ToDoItemContext>();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.EnableSensitiveDataLogging(true);
            services.AddDbContext<ToDoItemContext>(ctx => ctx.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            #endregion
            #region DependencyInjection
            services.AddScoped<IToDoItemService, ToDoItemService>();
            services.AddScoped<ToDoItemContext>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
