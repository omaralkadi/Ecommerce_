namespace AmazonApis.Helper
{
    public class Pagination<T>
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
        
    }
}
