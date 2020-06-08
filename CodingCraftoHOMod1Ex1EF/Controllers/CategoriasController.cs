using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftoHOMod1Ex1EF.Models;
using System.Transactions;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        public async Task<ActionResult> Index()
        {
            return View(await db.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoriaId,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Categorias.Add(categoria);
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
                
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoriaId,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(categoria).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            db.Categorias.Remove(categoria);
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
