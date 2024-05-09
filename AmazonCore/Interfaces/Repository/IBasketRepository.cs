using AmazonCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCore.Interfaces.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string BasketId);
        Task<CustomerBasket?> UpdateOrCreateBasket(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(string BasketId);
        
    }
}
