using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Compras")]
    public class Compra
    {
        public int CompraId { get; set; }
        public float Valor { get; set; }

        public int FornecedorId { get; set; }

        public virtual Fornecedor fornecedor { get; set; } 
        
        public virtual ICollection<Produto> produtos { get; set; }
        public virtual ICollection<Categoria> categorias { get; set; }

    }
}