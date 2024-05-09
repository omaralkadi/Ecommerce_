using AmazonApis.Dtos;
using AutoMapper;
using Talabat_Core.Entities;

namespace AmazonApis.Helper
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configration;

        public PictureUrlResolver(IConfiguration Configration)
        {
            _configration = Configration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
