using AmazonCore;
using AmazonCore.Entities.Order;
using AmazonCore.Interfaces.Repository;
using AmazonCore.Services;
using AmazonCore.Specification.OrderSpec;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonService
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        public IBasketRepository _basketRepository { get; }
        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string basketId, int DeliveryMethod, Address Shippingaddress)
        {
            //1.Get Basket From Basket Repo
            var Basket = await _basketRepository.GetBasketAsync(basketId);
            //2.Get Selected Items at Basket From Product Repo
            var orderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                    var ProductItemOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            //3.Calculate SubTotal
            var SubTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //4.Get Delivery Method From DeliveryMethod Repo
            var DeliveyMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(DeliveryMethod);

            //5.Create Order
            var order = new Order(BuyerEmail, Shippingaddress, DeliveyMethod, orderItems, SubTotal);
            //6.Add Order Locally
            await _unitOfWork.Repository<Order>().AddAsync(order);

            //7.Save Order To Database[ToDo]
            var Result = await _unitOfWork.Complete();

            if (Result <= 0) return null;

            return order;

        }

        public async Task<Order> CreateOrderByIdForSpecificUserAsync(string BuyerEmail, int orderId)
        {
            var spec =new OrderSpecifiction(BuyerEmail,orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpec(spec);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrderForSpecificUserAsync(string BuyerEmail)
        {
            var Spec = new OrderSpecifiction(BuyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetAllWithSpec(Spec);
            return order;

        }
        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethod()
        {
            var methods = await _unitOfWork.Repository<DeliveryMethod>().GetAll();
            return methods;

        }

    }
}
