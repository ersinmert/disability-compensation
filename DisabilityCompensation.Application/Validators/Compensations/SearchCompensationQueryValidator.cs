using DisabilityCompensation.Application.Queries.Compensations;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Compensations
{
    public class SearchCompensationQueryValidator : AbstractValidator<SearchCompensationQuery>
    {
        public SearchCompensationQueryValidator()
        {
            RuleFor(x => x.Page).Must(page => page > 0).WithMessage("Page 0'dan büyük olmalıdır!");
            RuleFor(x => x.PageSize).Must(pageSize => pageSize > 0).WithMessage("PageSize 0'dan büyük olmalıdır!");
        }
    }
}
