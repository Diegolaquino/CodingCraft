using CodingCraftoHOMod1Ex1EF.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;

namespace CodingCraftoHOMod1Ex1EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Fornecedores.AddOrUpdate(f => f.Nome,
                new Fornecedor { Nome = "Candy" },
                new Fornecedor { Nome = "Didoci" },
                new Fornecedor { Nome = "Doly" },
                new Fornecedor { Nome = "Doces Marsil" },
                new Fornecedor { Nome = "Manos Doces" },
                new Fornecedor { Nome = "Doces Malu" },
                new Fornecedor { Nome = "Bom Baiano" },
                new Fornecedor { Nome = "Garoto Atacado" },
                new Fornecedor { Nome = "Atacado de guloseimas" },
                new Fornecedor { Nome = "Nestlé" }
                );

            context.SaveChanges();

            context.Categorias.AddOrUpdate(c => c.Nome,
                new Categoria { Nome = "Chocolates" },
                new Categoria { Nome = "Sucos" },
                new Categoria { Nome = "Biscoitos" },
                new Categoria { Nome = "Açucarados" },
                new Categoria { Nome = "Bolos" },
                new Categoria { Nome = "Caseiros" }
                );

            context.SaveChanges();

            context.Produtos.AddOrUpdate(p => p.Nome,
                new Produto { Nome = "KitKat", CategoriaId = 1, Preco = 0, Cardinalidade = 3, Quantidade = 0 },
                new Produto { Nome = "Guaracamp", CategoriaId = 2, Preco = 0, Cardinalidade = 3, Quantidade = 0 },
                new Produto { Nome = "Gulosos", CategoriaId = 3, Preco = 0, Cardinalidade = 4, Quantidade = 0 },
                new Produto { Nome = "Jujubas", CategoriaId = 4, Preco = 0, Cardinalidade = 4, Quantidade = 0 },
                new Produto { Nome = "Bolo de chocolate", CategoriaId = 0, Preco = 5, Cardinalidade = 4, Quantidade = 0 },
                new Produto { Nome = "Maça do Amor", CategoriaId = 6, Preco = 0, Cardinalidade = 4, Quantidade = 0 }
                );

            context.SaveChanges();

            context.FornecedoresProdutos.AddOrUpdate(
                new FornecedorProduto { FornecedorProdutoId = 1, FornecedorId = 1,ProdutoId = 1 },
                new FornecedorProduto { FornecedorProdutoId = 2, FornecedorId = 2, ProdutoId = 2 },
                new FornecedorProduto { FornecedorProdutoId = 3, FornecedorId = 3, ProdutoId = 3 },
                new FornecedorProduto { FornecedorProdutoId = 4, FornecedorId = 4, ProdutoId = 4 },
                new FornecedorProduto { FornecedorProdutoId = 5, FornecedorId = 5, ProdutoId = 5 }
                );

            context.SaveChanges();
        }
    }
}
