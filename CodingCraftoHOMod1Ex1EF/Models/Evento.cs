using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Eventos")]
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        public DateTime DataDeCadastro { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataDeAviso { get; set; }

        [DataType(DataType.MultilineText)]
        public string Aviso { get; set; }

        public bool EventoCompletado { get; set; }
        
        public string NomeProduto { get; set; }
    }
}