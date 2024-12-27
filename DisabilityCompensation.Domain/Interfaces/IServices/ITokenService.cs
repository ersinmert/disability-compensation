namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId);
    }
}
