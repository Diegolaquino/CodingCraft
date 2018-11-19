using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        public DateTime DataDaVenda { get; set; }

        public decimal ValorDaVenda { get; set; }

        public virtual ICollection<Item> itens { get; set; }
    }
}