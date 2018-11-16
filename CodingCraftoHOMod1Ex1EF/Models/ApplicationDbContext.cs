using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<FornecedorProduto> FornecedoresProdutos { get; set; }
        public DbSet<Compra> Compras { get; set; }
    }
}