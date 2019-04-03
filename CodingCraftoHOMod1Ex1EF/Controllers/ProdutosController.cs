﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftoHOMod1Ex1EF.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Transactions;
using System.IO;

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
        public async Task<ActionResult> Create(Produto produto)
        {
            produto.Quantidade = 0;

            if(!string.IsNullOrEmpty(produto.ImageFile.FileName))
            {
                try
                {
                    string fileName = Path.GetFileName(produto.ImageFile.FileName);
                    produto.ImagePath = "/Img/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Img/"), fileName);
                    produto.ImageFile.SaveAs(fileName);
                    //produto.ImagePath = fileName;
                }
                catch (IOException)
                {
                    throw new IOException("Erro ao tentar salvar a imagem do produto.");
                }
                
            }
            else
            {
                produto.ImagePath = "nothing";
            }

            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Produtos.Add(produto);
                    await db.SaveChangesAsync();
                    scope.Complete();
                }

                ModelState.Clear();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoId,CategoriaId,Nome,Preco,Cardinalidade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(produto).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
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
