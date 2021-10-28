using Loja.Application.Context;
using Loja.Application.Contracts.Ioc;
using Loja.Application.Interfaces;
using Loja.Application.Interfaces.Repositories;
using Loja.Application.Interfaces.Services;
using Loja.Application.Repositories;
using Loja.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Loja.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loja.Api", Version = "v1" });
            });

            services.AddDbContextFactory<LojaContext>(
                options =>
                options.UseSqlServer(@"INPUT CONNECTION STRING"));

            RegisterIocValidator(services);
            RegisterIoc(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loja.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterIoc(IServiceCollection services)
        {
            services.AddSingleton<IClienteRepository, ClienteRepository>();
            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
            services.AddSingleton<IPedidoRepository, PedidoRepository>();


            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IProdutoService, ProdutoService>();
            services.AddSingleton<IPedidoService, PedidoService>();

        }

        private void RegisterIocValidator(IServiceCollection services)
        {
            foreach (var item in IocValidators.ObterSingleTypes())
            {
                services.AddSingleton(item, item);
            }
        }
    }
}
