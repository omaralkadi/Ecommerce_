﻿using Talabat_Core.Entities;

namespace AmazonApis.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string ProductBrand { get; set; }
        public string ProductType { get; set; }
        public int? ProductBrandId { get; set; }
        public int? ProductTypeId { get; set; }


    }
}
