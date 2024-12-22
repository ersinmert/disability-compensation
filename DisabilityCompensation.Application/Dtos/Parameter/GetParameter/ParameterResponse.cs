namespace DisabilityCompensation.Application.Dtos.Parameter.GetParameter
{
    public class ParameterResponse
    {
        public string? Code { get; set; }
        public List<ParameterValueResponse>? Values { get; set; }
    }
}
