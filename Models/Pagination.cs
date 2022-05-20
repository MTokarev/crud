namespace crud.Models
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageSizeMaxAllowed { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / (decimal)PageSize);
        public int ItemsReturned => Data.Count;
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int pageSizeMaxAllowed, int totalItems, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageSizeMaxAllowed = pageSizeMaxAllowed;
            TotalItems = totalItems;
            Data = data;
        }

    }
}
