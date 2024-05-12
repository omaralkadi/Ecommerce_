using AmazonCore.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCore.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string basketId, int DeliveryMethod, Address address);
        Task<Order> CreateOrderByIdForSpecificUserAsync(string BuyerEmail, int orderId);
        Task<IEnumerable<Order>> GetOrderForSpecificUserAsync(string BuyerEmail);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethod();
    }
}
