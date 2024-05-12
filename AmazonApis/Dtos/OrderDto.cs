namespace AmazonApis.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DelivetMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
