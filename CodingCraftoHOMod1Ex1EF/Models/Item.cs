using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{

    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
    }
}