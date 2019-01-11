using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftoHOMod1Ex1EF.Models;
using System.Transactions;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class FornecedoresProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FornecedoresProdutos
        public async Task<ActionResult> Index()
        {
            var fornecedorProdutoes = db.FornecedoresProdutos.Include(f => f.Fornecedor).Include(f => f.Produto);
            return View(await fornecedorProdutoes.ToListAsync());
        }

        // GET: FornecedoresProdutos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FornecedorProduto fornecedorProduto = await db.FornecedoresProdutos.FindAsync(id);
            if (fornecedorProduto == null)
            {
                return HttpNotFound();
            }
            return View(fornecedorProduto);
        }

        // GET: FornecedoresProdutos/Create
        public ActionResult Create()
        {
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: FornecedoresProdutos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProdutoId,FornecedorId")] FornecedorProduto fornecedorProduto)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.FornecedoresProdutos.Add(fornecedorProduto);
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
                return RedirectToAction("Index");
            }

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome", fornecedorProduto.FornecedorId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", fornecedorProduto.ProdutoId);
            return View(fornecedorProduto);
        }

        // GET: FornecedoresProdutos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FornecedorProduto fornecedorProduto = await db.FornecedoresProdutos.FindAsync(id);
            if (fornecedorProduto == null)
            {
                return HttpNotFound();
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome", fornecedorProduto.FornecedorId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", fornecedorProduto.ProdutoId);
            return View(fornecedorProduto);
        }

        // POST: FornecedoresProdutos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoId,FornecedorId")] FornecedorProduto fornecedorProduto)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(fornecedorProduto).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "Nome", fornecedorProduto.FornecedorId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", fornecedorProduto.ProdutoId);
            return View(fornecedorProduto);
        }

        // GET: FornecedoresProdutos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FornecedorProduto fornecedorProduto = await db.FornecedoresProdutos.FindAsync(id);
            if (fornecedorProduto == null)
            {
                return HttpNotFound();
            }
            return View(fornecedorProduto);
        }

        // POST: FornecedoresProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FornecedorProduto fornecedorProduto = await db.FornecedoresProdutos.FindAsync(id);
            db.FornecedoresProdutos.Remove(fornecedorProduto);
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
