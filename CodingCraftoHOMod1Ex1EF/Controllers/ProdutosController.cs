using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftoHOMod1Ex1EF.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class ProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produtos
        public async Task<ActionResult> Index()
        {
            var produtoes = db.Produtos.Include(p => p.Categoria);
            return View(await produtoes.ToListAsync());
        }

        public ActionResult Carrinho() => View();

        public async Task<ActionResult> Comprar(int id)
        {
            if(Session["carrinho"] == null)
            {
                List<Item> carrinho = new List<Item>();
                var produtoQueSeraComprado = await db.Produtos.FindAsync(id);
                Item produto = new Item { NomeItem = produtoQueSeraComprado.Nome, Quantidade = 1, CodigoProduto = produtoQueSeraComprado.ProdutoId, PrecoUnitario = produtoQueSeraComprado.Preco };
                carrinho.Add(produto);
                Session["carrinho"] = carrinho;
            }
            else
            {
                List<Item> carrinho = (List<Item>)Session["carrinho"];

                var retorno = carrinho.Find(c => c.CodigoProduto == id);

                if(retorno != null)
                {
                    carrinho.Remove(retorno);
                    Item produto = new Item { NomeItem = retorno.NomeItem, Quantidade = ++retorno.Quantidade, CodigoProduto = retorno.CodigoProduto, PrecoUnitario = retorno.PrecoUnitario };
                    carrinho.Add(produto);
                }
                else
                {
                    var produtoQueSeraComprado = await db.Produtos.FindAsync(id);
                    Item produto = new Item { NomeItem = produtoQueSeraComprado.Nome, Quantidade = 1, CodigoProduto = produtoQueSeraComprado.ProdutoId, PrecoUnitario = produtoQueSeraComprado.Preco };
                    carrinho.Add(produto);
                    Session["carrinho"] = carrinho;
                }
                
                Session["carrinho"] = carrinho;
            }

            return View("Carrinho");
        }

        public ActionResult RemoverItem(int? CodigoProduto)
        {
            if(CodigoProduto == null)
            {
                return HttpNotFound("Codigo do Produto Inválido! -> " + CodigoProduto.ToString());
            }

            List<Item> carrinho = (List<Item>)Session["carrinho"];
            var itemQueSeraExcluido = carrinho.Find(c => c.CodigoProduto == CodigoProduto);

            carrinho.Remove(itemQueSeraExcluido);

            Session["carrinho"] = carrinho;

            return RedirectToAction("Carrinho");
        }

        public ActionResult ListarVendas() => View();

        [HttpGet]
        public ActionResult CancelarPedido()
        {
            Session["carrinho"] = null;
            return RedirectToAction("Index");
        }
   
        //public async Task<ActionResult> ComprarParaEstoque()
        //{
        //    List<Estoque> produtos = new List<Estoque>();
        //    Estoque p = new Estoque();
        //    produtos = db.Estoques.Add(p);
        //}

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProdutoId,CategoriaId,Nome,Preco,Cardinalidade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoId,CategoriaId,Nome,Preco,Cardinalidade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
