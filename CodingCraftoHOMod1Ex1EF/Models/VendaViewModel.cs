using System.ComponentModel.DataAnnotations;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class VendaViewModel
    {
        //[Key]
        //public int Id { get; set; }

        public string Cliente { get; set; }

        public decimal Valor { get; set; } 
    }
}