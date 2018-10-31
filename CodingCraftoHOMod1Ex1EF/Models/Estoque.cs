using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        public int EstoqueId { get; set; }
        public DateTime DataDaEntrada { get; set; }
        public DateTime DataDaSaida { get; set; }

        public int FornecedorProdutoId { get; set; }

        public virtual FornecedorProduto fornecedorProduto {get; set;}
        public virtual ICollection<Produto> produtos { get; set; }
    }
}