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
        [Key]
        public int VendaId { get; set; }

        public DateTime DataDaVenda { get; set; }
        [Required]
        public decimal Total { get; set; }

        public int QuantidadeDeItens { get; set; }

        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<Item> Itens { get; set; }

    }
}