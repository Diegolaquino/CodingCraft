using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class ItemPedido
    {

        [Key]
        public int ItemPedidoId { get; set; }

        [Display(Name = "Nome do Produto")]
        public string NomeItem { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        [Display(Name = "Código do Produto")]
        public int CodigoProduto { get; set; }

        public virtual Pedido Pedido { get; set; }

        public int PedidoId { get; set; }
    }
}