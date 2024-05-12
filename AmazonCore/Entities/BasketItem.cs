using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonCore.Entities
{
    public class BasketItem:BaseEntity<int>
    {
        public string productName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
