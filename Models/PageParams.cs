namespace crud.Models
{
    /// <summary>
    /// Paging parameters
    /// </summary>
    public class PageParams
    {
        /// <summary>
        /// Page size. Must be greater than '0' and less than allowed maximum page size.
        /// Default page size will be loaded from configuration.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page number. Must be greater than '0' and less than total pages count.
        /// Default page size will be loaded from configuration.
        /// </summary>
        public int PageIndex { get; set; }
    }
}
