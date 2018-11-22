namespace CodingCraftoHOMod1Ex1EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
                new Models.Fornecedor { Nome = "Candy" },
                new Models.Fornecedor { Nome = "Didoci" },
                new Models.Fornecedor { Nome = "Doly" });

            context.Categorias.AddOrUpdate(c => c.Nome,
                new Models.Categoria { CategoriaId = 1, Nome = "Chocolates" },
                new Models.Categoria { CategoriaId = 2, Nome = "Sucos" },
                new Models.Categoria { CategoriaId = 3, Nome = "Biscoitos" });

            context.Produtos.AddOrUpdate(p => p.Nome,
                new Models.Produto { Nome = "KitKat", CategoriaId = 1, Preco = 5, Cardinalidade = 3 },
                new Models.Produto { Nome = "Guaracamp", CategoriaId = 2, Preco = 5, Cardinalidade = 3 },
                new Models.Produto { Nome = "Gulosos", CategoriaId = 3, Preco = 5, Cardinalidade = 4 });

            context.SaveChanges();
        }
    }
}
