using AmazonCore.Entities;
using AmazonCore.Interfaces.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmazonRepository.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {

            return await _database.KeyDeleteAsync(BasketId);
        }
        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await _database.StringGetAsync(BasketId);

            return Basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket?>(Basket);

        }

        public async Task<CustomerBasket?> UpdateOrCreateBasket(CustomerBasket Basket)
        {
            var SerializedBasket = JsonSerializer.Serialize(Basket);
            var Result = await _database.StringSetAsync(Basket.Id, SerializedBasket, TimeSpan.FromDays(1));
            return Result is false ? null : await GetBasketAsync(Basket.Id);
        }
    }
}
