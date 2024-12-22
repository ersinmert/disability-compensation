using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class ParameterRepository : GenericRepository<Parameter, AppDbContext>, IParameterRepository
    {
        public ParameterRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<List<Parameter>> GetParametersAsync(List<string>? codes)
        {
            return await _context.Parameters
                .WhereIf(codes?.Any() == true, x => codes!.Contains(x.Code) && x.IsActive)
                .ToListAsync();
        }
    }
}
