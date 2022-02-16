using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Best_Beauty.Models
{
    public class Service
    {
        public int ID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]
        public string Denumire { get; set; }
        public string Durata { get; set; }
        
        [Range(1, 300)]
        public decimal Pret { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
