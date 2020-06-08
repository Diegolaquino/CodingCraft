using CodingCraftoHOMod1Ex1EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContabilidadesController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contabilidades
        public ActionResult Index()
        {
            ViewData["Despesas"] = (from d in db.Compras select new CompraViewModel { Fornecedor = d.Fornecedor.Nome, Valor = d.Valor }).ToList();
            ViewData["Receitas"] = (from r in db.Vendas select new VendaViewModel { Cliente  = r.User.UserName, Valor = r.ValorDaVenda }).ToList();

            return View();
        }
    }
}