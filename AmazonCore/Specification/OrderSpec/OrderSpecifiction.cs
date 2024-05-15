using AmazonCore.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCore.Specification.OrderSpec
{
    public class OrderSpecifiction : BaseSpecification<Order, int>
    {
        public OrderSpecifiction(string BuyerEmail) : base(O => O.BuyerEmail == BuyerEmail)
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.items);
            OrderDesc(o => o.DateTime);
        }
        public OrderSpecifiction(string BuyerEmail, int orderId) : base(o => o.BuyerEmail == BuyerEmail && o.id == orderId)
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.items);
        }
    }
}
