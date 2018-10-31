using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Required]
        [StringLength(100)]
        [Index("IUQ_Fornecedores_Nome", IsUnique = true)]
        public String Nome { get; set; }

        public virtual ICollection<FornecedorProduto> FornecedorProdutos { get; set; }
    }
}