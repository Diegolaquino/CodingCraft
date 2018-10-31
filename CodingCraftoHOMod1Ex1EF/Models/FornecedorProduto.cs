using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("FornecedoresProdutos")]
    public class FornecedorProduto
    {
        // [Key]
        // public int FornecedorProdutoId { get; set; }
        // [Index("IUQ_FornecedoresProdutos_FornecedorId_ProdutoId", IsUnique = true, Order = 1)]
        [Key, Column(Order = 1)]
        public int ProdutoId { get; set; }
        // [Index("IUQ_FornecedoresProdutos_FornecedorId_ProdutoId", IsUnique = true, Order = 2)]
        [Key, Column(Order = 2)]
        public int FornecedorId { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}