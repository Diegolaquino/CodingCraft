﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Compras")]
    public class Compra
    {
        [Key]
        public int CompraId { get; set; }

        public DateTime DataDaCompra { get; set; }
        [Required]
        public decimal Valor { get; set; }

        public decimal PrecoPorProduto { get; set; }

        public int FornecedorId { get; set; }

        public int CartegoriaId { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public virtual Fornecedor Fornecedor { get; set; } 
        
        public virtual Produto Produto { get; set; }

        public virtual Categoria Categoria { get; set; }

    }
}