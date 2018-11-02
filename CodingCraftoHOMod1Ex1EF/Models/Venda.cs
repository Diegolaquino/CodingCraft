using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Vendas")]
    public class Venda
    {
        public int VendaId { get; set; }

        [Display(Name = "Cliente")]
        public int QuantidadeProdutos { get; set; }

        public DateTime DataDaVenda { get; set; }

        public float ValorVenda { get; set; }

        [Display(Name = "Cliente")]
        public int ClientId { get; set; }

        public virtual Cliente cliente { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}