using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Beauty.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public Service Service { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
