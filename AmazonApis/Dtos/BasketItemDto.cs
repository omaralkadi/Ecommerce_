using System.ComponentModel.DataAnnotations;
using Talabat_Core.Entities;

namespace AmazonApis.Dtos
{
    public class BasketItemDto:BaseEntity<int>
    {
        [Required]
        public string productName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be at least one")]
        public int Quantity { get; set; }   
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Can not be Zero")]
        public decimal Price { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }
    }
}