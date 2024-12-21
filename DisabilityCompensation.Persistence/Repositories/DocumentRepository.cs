using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class DocumentRepository : GenericRepository<Document, AppDbContext>, IDocumentRepository
    {
        public DocumentRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
