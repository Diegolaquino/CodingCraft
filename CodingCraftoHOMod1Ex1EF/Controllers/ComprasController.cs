using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodingCraftoHOMod1Ex1EF.Models;
using System.Transactions;
using System.Web.Helpers;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compras
        public async Task<ActionResult> Index()
        {
            var compras = db.Compras.Include(c => c.Fornecedor);
            return View(await compras.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = await db.Compras.FindAsync(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetProdutos(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if(id == null)
            {
                return Json(db.Produtos.ToList(), JsonRequestBehavior.AllowGet);
            }
           
            var lista = await GetProdutosPorIdCategoriaAsync((int)id);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public async Task<List<Produto>> GetProdutosPorIdCategoriaAsync(int idCategoria)
        {
            return (await db.Produtos.Where(p => p.CategoriaId == idCategoria).ToListAsync());
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompraId,Valor,FornecedorId, CategoriaId, ProdutoId, Quantidade")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    compra.DataDaCompra = DateTime.Now;
                    db.Compras.Add(compra);

                    db.FornecedoresProdutos.Add(new FornecedorProduto() { FornecedorId = compra.FornecedorId, ProdutoId = compra.ProdutoId });

                    var produto = await db.Produtos.FindAsync(compra.ProdutoId);
                    produto.Quantidade += compra.Quantidade;
                    //sempre atualiza o preço de acordo com a última compra
                    produto.Preco = (compra.Valor / compra.Quantidade) * (decimal)1.1;
                    db.Entry(produto).State = EntityState.Modified;
             
                    await db.SaveChangesAsync();

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome", compra.FornecedorId);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = await db.Compras.FindAsync(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome", compra.FornecedorId);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompraId,Valor,FornecedorId")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(compra).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome", compra.FornecedorId);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = await db.Compras.FindAsync(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Compra compra = await db.Compras.FindAsync(id);
            db.Compras.Remove(compra);
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
