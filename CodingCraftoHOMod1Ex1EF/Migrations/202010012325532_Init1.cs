namespace CodingCraftoHOMod1Ex1EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoriaId)
                .Index(t => t.Nome, unique: true, name: "IUQ_Categorias_Nome");
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cardinalidade = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        URLFoto = c.String(),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.Nome, unique: true, name: "IUQ_Produtos_Nome")
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.FornecedoresProdutos",
                c => new
                    {
                        FornecedorProdutoId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        FornecedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FornecedorProdutoId, t.ProdutoId, t.FornecedorId })
                .ForeignKey("dbo.Fornecedores", t => t.FornecedorId, cascadeDelete: true)
                .ForeignKey("dbo.Produtos", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => new { t.ProdutoId, t.FornecedorId }, unique: true, name: "IUQ_FornecedoresProdutos_FornecedorId_ProdutoId");
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        FornecedorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.FornecedorId)
                .Index(t => t.Nome, unique: true, name: "IUQ_Fornecedores_Nome");
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraId = c.Int(nullable: false, identity: true),
                        DataDaCompra = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoPorProduto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FornecedorId = c.Int(nullable: false),
                        CartegoriaId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Int(nullable: false),
                        Categoria_CategoriaId = c.Int(),
                    })
                .PrimaryKey(t => t.CompraId)
                .ForeignKey("dbo.Categorias", t => t.Categoria_CategoriaId)
                .ForeignKey("dbo.Fornecedores", t => t.FornecedorId, cascadeDelete: true)
                .ForeignKey("dbo.Produtos", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.FornecedorId)
                .Index(t => t.ProdutoId)
                .Index(t => t.Categoria_CategoriaId);
            
            CreateTable(
                "dbo.Eventos",
                c => new
                    {
                        EventoId = c.Int(nullable: false, identity: true),
                        TipoDeEvento = c.Int(nullable: false),
                        DataDeCadastro = c.DateTime(nullable: false),
                        DataDeAviso = c.DateTime(),
                        Aviso = c.String(),
                        EventoCompletado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventoId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        NomeItem = c.String(),
                        Quantidade = c.Int(nullable: false),
                        VendaId = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CodigoProduto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Vendas", t => t.VendaId, cascadeDelete: true)
                .Index(t => t.VendaId);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        VendaId = c.Int(nullable: false, identity: true),
                        DataDaVenda = c.DateTime(nullable: false),
                        ValorDaVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VendaId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PedidoId);
            
            CreateTable(
                "dbo.ItemPedidoes",
                c => new
                    {
                        ItemPedidoId = c.Int(nullable: false, identity: true),
                        NomeItem = c.String(),
                        Quantidade = c.Int(nullable: false),
                        PrecoUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CodigoProduto = c.Int(nullable: false),
                        PedidoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemPedidoId)
                .ForeignKey("dbo.Pedidos", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ItemPedidoes", "PedidoId", "dbo.Pedidos");
            DropForeignKey("dbo.Vendas", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "VendaId", "dbo.Vendas");
            DropForeignKey("dbo.Compras", "ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.Compras", "FornecedorId", "dbo.Fornecedores");
            DropForeignKey("dbo.Compras", "Categoria_CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.FornecedoresProdutos", "ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.FornecedoresProdutos", "FornecedorId", "dbo.Fornecedores");
            DropForeignKey("dbo.Produtos", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ItemPedidoes", new[] { "PedidoId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Vendas", new[] { "UserId" });
            DropIndex("dbo.Items", new[] { "VendaId" });
            DropIndex("dbo.Compras", new[] { "Categoria_CategoriaId" });
            DropIndex("dbo.Compras", new[] { "ProdutoId" });
            DropIndex("dbo.Compras", new[] { "FornecedorId" });
            DropIndex("dbo.Fornecedores", "IUQ_Fornecedores_Nome");
            DropIndex("dbo.FornecedoresProdutos", "IUQ_FornecedoresProdutos_FornecedorId_ProdutoId");
            DropIndex("dbo.Produtos", new[] { "CategoriaId" });
            DropIndex("dbo.Produtos", "IUQ_Produtos_Nome");
            DropIndex("dbo.Categorias", "IUQ_Categorias_Nome");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ItemPedidoes");
            DropTable("dbo.Pedidos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Vendas");
            DropTable("dbo.Items");
            DropTable("dbo.Eventos");
            DropTable("dbo.Compras");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.FornecedoresProdutos");
            DropTable("dbo.Produtos");
            DropTable("dbo.Categorias");
        }
    }
}
