using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingCraftoHOMod1Ex1EF.Models
{
    public class Item
    {
        public Produto Produto
        {
            get;
            set;
        }

        public int Quantidade
        {
            get;
            set;
        }
    }
}