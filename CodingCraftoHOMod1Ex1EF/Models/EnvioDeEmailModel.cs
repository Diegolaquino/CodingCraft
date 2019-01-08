using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class EnvioDeEmailModel
    {
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }
    }
}