using CodingCraftoHOMod1Ex1EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class CarrinhoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //ViewModelProduto
            var produtosViewModel = from p in db.Produtos
                                    select new ProdutoViewModel
                                    {
                                        ProdutoId = p.ProdutoId,
                                        Nome = p.Nome,
                                        Quantidade = p.Quantidade,
                                        Cardinalidade = p.Cardinalidade,
                                        Preco = p.Preco
                                    };
            return View(produtosViewModel.ToList());
        }

        public async Task<ActionResult> Comprar(int id)
        {
            if (Session["carrinho"] == null)
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

                if (retorno != null)
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

            return RedirectToAction("Carrinho");
        }

        public ActionResult RemoverItem(int? CodigoProduto)
        {
            if (CodigoProduto == null)
            {
                return HttpNotFound("Codigo do Produto Inválido! -> " + CodigoProduto.ToString());
            }

            List<Item> carrinho = (List<Item>)Session["carrinho"];
            var itemQueSeraExcluido = carrinho.Find(c => c.CodigoProduto == CodigoProduto);

            carrinho.Remove(itemQueSeraExcluido);

            Session["carrinho"] = carrinho;

            return RedirectToAction("Carrinho");
        }

        [HttpGet]
        public ActionResult CancelarPedido()
        {
            Session["carrinho"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Carrinho() => View();
    }
}
