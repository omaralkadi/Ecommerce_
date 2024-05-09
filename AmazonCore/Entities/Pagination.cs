using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCore.Entities
{
    public class Pagination<T>
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int index,int pagesize,int count,IReadOnlyList<T> data)
        {
            Index = index;
            PageSize = pagesize;
            Count = count;
            Data = data;
        }
    }
}
