namespace DisabilityCompensation.Domain.Entities
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public IList<T>? Items { get; set; }
    }
}
