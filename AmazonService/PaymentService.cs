using AmazonCore;
using AmazonCore.Entities;
using AmazonCore.Entities.Order;
using AmazonCore.Interfaces.Repository;
using AmazonCore.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Entities;
using Product = Talabat_Core.Entities.Product;

namespace AmazonService
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IConfiguration _configuration { get; }

        public PaymentService(IBasketRepository basketRepository, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }


        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripKeys:Secretkey"];
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket is null) return null;
            var ShippingCost = 0M;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveyMethod = await _unitOfWork.Repository<DeliveryMethod,int>().GetById(Basket.DeliveryMethodId.Value);
                ShippingCost = DeliveyMethod.Cost;
            }

            if (Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product,int>().GetById(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }
            var SubTotal = Basket.Items.Sum(item => item.Quantity * item.Price);
            //CreatePaymentIntent

            var Service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)((SubTotal * 100) + (ShippingCost * 100)), // Convert to cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() {"card"}
                };

                paymentIntent = await Service.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)((SubTotal * 100) + (ShippingCost * 100))
                };
                paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId, option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            };
            await _basketRepository.UpdateOrCreateBasket(Basket);

            return Basket;

        }

    }
}

