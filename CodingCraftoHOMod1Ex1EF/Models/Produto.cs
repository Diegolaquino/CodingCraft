using CodingCraftoHOMod1Ex1EF.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Display(Name = "Produto")]
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(100)]
        [Index("IUQ_Produtos_Nome", IsUnique = true)]
        public String Nome { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        [Required]
        public decimal Preco { get; set; }

        [Required]
        public int Cardinalidade { get; set; }

        [Range(1, 100)]
        public int Quantidade { get; set; }

        public string URLFoto { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [NotMapped]
        public ETipoFotoProduto TipoFoto { get; set; }
        

        public virtual Categoria Categoria { get; set; }

        
        public virtual ICollection<FornecedorProduto> ProdutoFornecedores { get; set; }
    }
}