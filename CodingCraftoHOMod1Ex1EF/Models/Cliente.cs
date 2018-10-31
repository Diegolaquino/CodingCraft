using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome do Cliente")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
    }
}