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
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<CodingCraftoHOMod1Ex1EF.Models.Categoria> Categorias { get; set; }
        public System.Data.Entity.DbSet<CodingCraftoHOMod1Ex1EF.Models.FornecedorProduto> FornecedoresProdutos { get; set; }
    }
}