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

        [Display(Name = "Tipo de Evento")]
        public int TipoDeEvento { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataDeCadastro { get; set; }

        [Display(Name = "Data de Lembrete do Evento")]
        [DataType(DataType.Date)]
        public DateTime? DataDeAviso { get; set; }

        [DataType(DataType.MultilineText)]
        public string Aviso { get; set; }

        [Display(Name = "Evento Completado")]
        public bool EventoCompletado { get; set; }
    }
}