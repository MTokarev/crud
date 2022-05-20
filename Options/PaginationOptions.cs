namespace crud.Options
{
    public class PaginationOptions
    {
        public int DefaultPageSize { get; set; } = 10;
        public int MaxPageSize { get; set; } = 50;
        public int DefaultPageIndex { get; set; } = 1;
    }
}
