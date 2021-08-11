﻿using Microsoft.EntityFrameworkCore;
using SingleExperience.Entities;

namespace SingleExperience.Context
{
    public class Context : DbContext
    {
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = rafael.messias; Data Source = SERVER");
        }
    }
}
