namespace DisabilityCompensation.Domain.Dtos
{
    public class ApproveCompensationDto
    {
        public Guid Id { get; set; }
        public int DisabilityRate { get; set; }
        public bool HasTemporaryDisability { get; set; }
        public bool HasCaregiver { get; set; }
        public int TemporaryDisabilityDay { get; set; }
    }
}
