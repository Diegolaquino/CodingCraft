using CodingCraftoHOMod1Ex1EF.Helper;
using CodingCraftoHOMod1Ex1EF.Models;
using CodingCraftoHOMod1Ex1EF.Models.Enums;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CodingCraftoHOMod1Ex1EF.Controllers
{
    public class CarrinhoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index
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

            //Verifica se há algum pagamento agendado para o dia
            BackgroundJob.Enqueue(() => LembretePagamentoFornecedor());

            return View(produtosViewModel.ToList());
        }
        #endregion

        public ActionResult Carrinho() => View();

        #region Método Comprar
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
        #endregion

        #region Método que remove produto do carrinho
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
        #endregion

        #region Método de Cancelamento do pedido
        [HttpGet]
        public ActionResult CancelarPedido()
        {
            Session["carrinho"] = null;
            return RedirectToAction("Index");
        }
        #endregion

        #region Método LembretePagamentoFornecedor
        public async Task LembretePagamentoFornecedor()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateTime currentDate = DateTime.Now;
            var eventos = await db.Eventos.Where(e => e.TipoDeEvento == (int)TipoDeEvento.PagamentoFornecedor && DbFunctions.TruncateTime(e.DataDeAviso) == currentDate.Date).ToListAsync();

            if (eventos.Any())
            {
                string mensagemEmail = "";
                int cont = 1;
                string m = "\r\n";

                foreach (Evento evento in eventos)
                {
                    m += cont + " " + evento.Aviso + "\r\n";
                    cont++;
                }

                mensagemEmail += m;

                await EmailHelper.EnviarEmailAsync("diegol.aquino@gmail.com", "Lembrete de Pagamento", mensagemEmail);
            }
        }
        #endregion
    }
}
