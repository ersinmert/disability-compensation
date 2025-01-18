namespace DisabilityCompensation.Domain.Dtos
{
    public class PagedResultDto<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public IList<T>? Items { get; set; }
    }
}
