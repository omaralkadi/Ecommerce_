using AmazonCore.Entities.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat_Core.Entities;

namespace AmazonRepository.Data
{
    public static class DataContextSeed
    {
        public static async Task ApplySeeding(DataContext _DbContext)
        {
            if (!_DbContext.ProductBrands.Any())
            {
                var Brands = File.ReadAllText("../AmazonRepository/Data/DataSeed/brands.json");
                var BrandsData = JsonSerializer.Deserialize<List<ProductBrand>>(Brands);
                if (BrandsData?.Count > 0)
                {
                    await _DbContext.ProductBrands.AddRangeAsync(BrandsData);
                }

            }
            if (!_DbContext.ProductTypes.Any())
            {
                var Types = File.ReadAllText("../AmazonRepository/Data/DataSeed/types.json");
                var TypesData = JsonSerializer.Deserialize<List<ProductType>>(Types);
                if (TypesData?.Count > 0)
                {
                    await _DbContext.ProductTypes.AddRangeAsync(TypesData);
                }

            }

            if (!_DbContext.Products.Any())
            {
                var products = File.ReadAllText("../AmazonRepository/Data/DataSeed/products.json");
                var ProductData = JsonSerializer.Deserialize<List<Product>>(products);
                if (ProductData?.Count > 0)
                {
                    await _DbContext.Products.AddRangeAsync(ProductData);
                }

            }

            if (!_DbContext.DeliveyMethods.Any())
            {
                var Delivery = File.ReadAllText("../AmazonRepository/Data/DataSeed/delivery.json");
                var DeliveryData = JsonSerializer.Deserialize<List<DeliveryMethod>>(Delivery);
                if (DeliveryData?.Count > 0)
                {
                    await _DbContext.DeliveyMethods.AddRangeAsync(DeliveryData);
                }

            }

            await _DbContext.SaveChangesAsync();

        }

    }
}
