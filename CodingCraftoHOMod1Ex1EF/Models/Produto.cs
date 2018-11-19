using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        [Index("IUQ_Produtos_Nome", IsUnique = true)]
        public String Nome { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        [Required]
        public Decimal Preco { get; set; }

        [Required]
        public int Cardinalidade { get; set; }

        public virtual Categoria Categoria { get; set; }

        //public virtual ICollection<FornecedorProduto> ProdutoFornecedores { get; set; }
    }
}