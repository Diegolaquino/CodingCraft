using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Display(Name = "Data de Venda")]
        public DateTime DataDaVenda { get; set; }

        [Display(Name = "Valor de Venda")]
        public decimal ValorDaVenda { get; set; }

        [Display(Name = "ID Usuário")]
        public int UserId { get; set; }

        public virtual ICollection<Item> Itens { get; set; }

        public virtual ApplicationUser User { get; set; }

        //public bool Paga { get; set; }
    }
}