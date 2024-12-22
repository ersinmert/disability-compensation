namespace DisabilityCompensation.Application.Dtos.Parameter.AddParameter
{
    public class ParameterRequest
    {
        public string? Code { get; set; }
        public List<ParameterValueRequest>? Values { get; set; }
    }
}
