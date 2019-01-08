using System.ComponentModel.DataAnnotations;

namespace CodingCraftoHOMod1Ex1EF.Models.Enums
{
    public enum TipoDeEvento
    {
        [Display(Name = "Lembrete Para Pagar o Fornecedor")]
        PagamentoFornecedor = 1,

        [Display(Name = "Lembrete Para Cobrar o Cliente")]
        CobrancaCliente = 2
    }
}