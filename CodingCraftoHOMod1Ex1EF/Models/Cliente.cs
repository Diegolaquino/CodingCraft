using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        public int ClientId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}