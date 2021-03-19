using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendmailWithMemoryStreamApplication.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string Payment { get; set; }
        public List<Product> Products;
    }
}
