using Microsoft.EntityFrameworkCore;
using SingleExperience.Entities;
using System.Threading.Tasks;

namespace SingleExperience.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<CartaoCredito> CartaoCredito { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<ListaProdutoCompra> ListaProdutoCompra { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<StatusCarrinhoProduto> StatusCarrinhoProduto { get; set; }
        public DbSet<StatusCompra> StatusCompra { get; set; }

        public async Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync()
        {
            return await this.Database.BeginTransactionAsync();
        }
    }
}
