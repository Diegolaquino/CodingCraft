using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        public int PedidoId { get; set; }

        public virtual ICollection<ItemPedido> Itens { get; set; }

        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

    }
}