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
                new Models.Fornecedor { FornecedorId = 1, Nome = "Candy" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Didoci" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Doly" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Doces Marsil" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Manos Doces" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Doces Malu" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Bom Baiano" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Garoto Atacado" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Atacado de guloseimas" },
                new Models.Fornecedor { FornecedorId = 1, Nome = "Nestlé" }
                );

            context.Categorias.AddOrUpdate(c => c.Nome,
                new Models.Categoria { CategoriaId = 1, Nome = "Chocolates" },
                new Models.Categoria { CategoriaId = 2, Nome = "Sucos" },
                new Models.Categoria { CategoriaId = 3, Nome = "Biscoitos" },
                new Models.Categoria { CategoriaId = 4, Nome = "Açucarados" },
                new Models.Categoria { CategoriaId = 5, Nome = "Bolos" },
                new Models.Categoria { CategoriaId = 6, Nome = "Caseiros" }
                );

            context.Produtos.AddOrUpdate(p => p.Nome,
                new Models.Produto { Nome = "KitKat", CategoriaId = 1, Preco = 5, Cardinalidade = 3, ProdutoId = 1, Quantidade = 5 },
                new Models.Produto { Nome = "Guaracamp", CategoriaId = 2, Preco = 5, Cardinalidade = 3, ProdutoId = 2, Quantidade = 5 },
                new Models.Produto { Nome = "Gulosos", CategoriaId = 3, Preco = 5, Cardinalidade = 4, ProdutoId = 3, Quantidade = 5 },
                new Models.Produto { Nome = "Jujubas", CategoriaId = 4, Preco = 5, Cardinalidade = 4, ProdutoId = 4, Quantidade = 5 },
                new Models.Produto { Nome = "Bolo de chocolate", CategoriaId = 5, Preco = 5, Cardinalidade = 4, ProdutoId = 5, Quantidade = 5 },
                new Models.Produto { Nome = "Maça do Amor", CategoriaId = 6, Preco = 5, Cardinalidade = 4, ProdutoId = 6, Quantidade = 5 }
                );

            context.FornecedoresProdutos.AddOrUpdate(
                new Models.FornecedorProduto { FornecedorProdutoId = 1, FornecedorId = 1,ProdutoId = 1 },
                new Models.FornecedorProduto { FornecedorProdutoId = 2, FornecedorId = 2, ProdutoId = 2 },
                new Models.FornecedorProduto { FornecedorProdutoId = 3, FornecedorId = 3, ProdutoId = 3 },
                new Models.FornecedorProduto { FornecedorProdutoId = 4, FornecedorId = 4, ProdutoId = 4 },
                new Models.FornecedorProduto { FornecedorProdutoId = 5, FornecedorId = 5, ProdutoId = 5 }
                );

            context.SaveChanges();
        }
    }
}
