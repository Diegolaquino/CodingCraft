using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{

    public class Item
    {
        public int ItemId { get; set; }

        [Display(Name = "Nome do Produto")]
        public string NomeItem { get; set; }

        public int Quantidade { get; set; }
        
        public int VendaId { get; set; }

        public decimal PrecoUnitario { get; set; }

        [ForeignKey("VendaId")]
        public Venda Venda { get; set; }

        [Display(Name = "Código do Produto")]
        public int CodigoProduto { get; set; }

    }
}