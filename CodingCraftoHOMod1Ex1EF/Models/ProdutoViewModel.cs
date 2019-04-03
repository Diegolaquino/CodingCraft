using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        public int Cardinalidade { get; set; }

        public string ImagePath { get; set; }
    }
}