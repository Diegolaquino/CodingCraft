using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Display(Name = "Produto")]
        [Key]
        public int ProdutoId { get; set; }

        [Display(Name = "Foto do Produto")]
        public string ImagePath { get; set; }

        [DataType(DataType.Upload)]
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

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

        public int Quantidade { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        
        public virtual ICollection<FornecedorProduto> ProdutoFornecedores { get; set; }
    }
}