using AmazonCore.Entities.Order;

namespace AmazonApis.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset DateTime { get; set; } 
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        public string deliveryMethod { get; set; }
        public decimal deliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }

    }
}
