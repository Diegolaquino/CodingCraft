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
using System.Collections;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class EventosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Eventos
        public async Task<ActionResult> Index()
        {
            return View(await db.Eventos.ToListAsync());
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            db.Configuration.ProxyCreationEnabled = false;

            IEnumerable produtos = from p in db.Produtos select p.Nome;

            ViewBag.Produtos = new SelectList(produtos,"Nome");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EvenoId,DataDeCadastro,DataDeAviso,Aviso,EventoCompletado, NomeProduto")] Evento evento)
        {
            evento.DataDeCadastro = DateTime.Now;
            evento.EventoCompletado = false;

            if (ModelState.IsValid)
            {
                db.Eventos.Add(evento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = await db.Eventos.FindAsync(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EvenoId,DataDeCadastro,DataDeAviso,Aviso,EventoCompletado,NomeProduto")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = await db.Eventos.FindAsync(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Evento evento = await db.Eventos.FindAsync(id);
            db.Eventos.Remove(evento);
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
