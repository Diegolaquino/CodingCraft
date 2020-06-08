using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftoHOMod1Ex1EF.Models;
using System.Transactions;
using Hangfire;
using System.Net.Mail;
using CodingCraftoHOMod1Ex1EF.Helper;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vendas
        public async Task<ActionResult> Index()
        {
            var vendas = db.Vendas.Include(v => v.User).Include(v => v.Itens);
            return View(await db.Vendas.ToListAsync());
        }

        public ActionResult EmailCliente() => View();

        public async Task<ActionResult> FinalizarPedidoAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return HttpNotFound("Você deve preencher o email do cliente");
            }

            var user = await db.Users.SingleOrDefaultAsync(c => c.Email.Contains(email.Trim()));

            if (user == null)
            {
                return HttpNotFound("Cliente Não cadastrado ou email incorreto!");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Venda venda = new Venda
                {
                    User = user,
                    UserId = user.Id,
                    DataDaVenda = DateTime.Now,
                    Itens = (List<Item>)Session["carrinho"]
                };
                venda.ValorDaVenda = venda.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
                db.Vendas.Add(venda);

                await db.SaveChangesAsync();

                foreach (var p in venda.Itens)
                {
                    var produtoQueSeraAtualizado = await db.Produtos.FindAsync(p.CodigoProduto);
                    if (produtoQueSeraAtualizado.Quantidade < p.Quantidade)
                    {
                        return HttpNotFound("Não há quantidade suficiente - " + produtoQueSeraAtualizado.Nome.ToString() + "- quantidade atual " + produtoQueSeraAtualizado.Quantidade.ToString());
                    }
                    produtoQueSeraAtualizado.Quantidade -= p.Quantidade;
                    db.Entry(produtoQueSeraAtualizado).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();

                scope.Complete();
            }

            BackgroundJob.Enqueue(() => ConfereQuantidadeProdutosAsync());

            return RedirectToAction("Index", "Vendas");
        }

        public async Task ConfereQuantidadeProdutosAsync()
        {
            var confereQuantidade = await db.Produtos.Where(p => p.Quantidade < 10).ToListAsync();
            string body = "Os seguintes produtos estão com estoque baixo: ";

            if (confereQuantidade.Any())
            {
                foreach (Produto p in confereQuantidade)
                {
                    body += p.Nome.ToString() + ", ";
                }

                await EmailHelper.EnviarEmailAsync("diegol.aquino@gmail.com", "Lembrete de Compra de Produtos", body);
            }
        }

        // GET: Vendas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VendaId,DataDaVenda,ValorDaVenda")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(venda).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venda venda = await db.Vendas.FindAsync(id);
            db.Vendas.Remove(venda);
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
