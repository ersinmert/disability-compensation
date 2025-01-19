using DisabilityCompensation.Application.Queries.Users;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Users
{
    public class SearchUserQueryValidator : AbstractValidator<SearchUserQuery>
    {
        public SearchUserQueryValidator()
        {
            RuleFor(x => x.Page)
                .Must(page => page > 0)
                    .WithMessage("Page 0'dan büyük olmalıdır!");
            RuleFor(x => x.PageSize)
                .Must(pageSize => pageSize > 0)
                    .WithMessage("PageSize 0'dan büyük olmalıdır!");
        }
    }
}
