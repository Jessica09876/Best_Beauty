using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Beauty.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string NumeCategorie { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
