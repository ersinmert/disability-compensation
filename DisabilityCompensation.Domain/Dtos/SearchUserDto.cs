namespace DisabilityCompensation.Domain.Dtos
{
    public class SearchUserDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
