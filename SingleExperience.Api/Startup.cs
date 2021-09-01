using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SingleExperience.Context;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.CartaoCredito;
using SingleExperience.Services.Categoria;
using SingleExperience.Services.Cliente;
using SingleExperience.Services.Compra;
using SingleExperience.Services.Endereco;
using SingleExperience.Services.ListaProdutoCompra;
using SingleExperience.Services.Produto;

namespace SingleExperience.Api
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
            services.AddDbContext<Context.Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<CarrinhoService>();
            services.AddScoped<CartaoCreditoService>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<ClienteService>();
            services.AddScoped<CompraService>();
            services.AddScoped<EnderecoService>();
            services.AddScoped<ListaProdutoCompraService>();
            services.AddScoped<ProdutoService>();

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

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
