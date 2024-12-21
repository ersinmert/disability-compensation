using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense, AppDbContext>, IExpenseRepository
    {
        public ExpenseRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
