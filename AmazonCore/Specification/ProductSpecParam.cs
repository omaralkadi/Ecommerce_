using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AmazonCore.Specification
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortingTypes
    {
        PriceAsc,
        PriceDesc,
        NameDesc,
    };
    public class ProductSpecParam
    {
        public SortingTypes? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private int pageSize=10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value>10?10:value; }
        }

    }
}
