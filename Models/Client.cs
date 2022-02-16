using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Beauty.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string NumeClient { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
