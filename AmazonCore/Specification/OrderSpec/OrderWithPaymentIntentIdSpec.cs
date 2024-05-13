using AmazonCore.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCore.Specification.OrderSpec
{
    public class OrderWithPaymentIntentIdSpec:BaseSpecification<Order,int>
    {
        public OrderWithPaymentIntentIdSpec(string PaymentIntentId):base(o=>o.PaymentIntentId==PaymentIntentId)
        {
            
        }

    }
}
