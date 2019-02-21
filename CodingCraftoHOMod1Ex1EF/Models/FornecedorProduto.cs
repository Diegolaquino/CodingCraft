using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("FornecedoresProdutos")]
    public class FornecedorProduto
    {
        [Display(Name = "Fornecedor do Produto")]
        [Key, Column(Order = 0)]
        public int FornecedorProdutoId { get; set; }

        [Index("IUQ_FornecedoresProdutos_FornecedorId_ProdutoId", Order = 1)]
        [Key, Column(Order = 1)]
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        [Index("IUQ_FornecedoresProdutos_FornecedorId_ProdutoId", Order = 2)]
        [Key, Column(Order = 2)]
        [Display(Name = "Fornecedor")]
        public int FornecedorId { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}