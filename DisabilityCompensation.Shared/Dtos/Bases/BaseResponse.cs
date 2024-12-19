namespace DisabilityCompensation.Shared.Dtos.Bases
{
    public class BaseResponse<T>
    {
        public bool Succcess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public IEnumerable<BaseValidationError>? ValidationErrors { get; set; }
    }
}
