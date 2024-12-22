using DisabilityCompensation.Application.Commands.Parameters;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Parameters
{
    public class AddParameterCommandValidator : AbstractValidator<AddParametersCommand>
    {
        public AddParameterCommandValidator()
        {
            RuleFor(x => x.Parameters)
                .NotNull().Must(x => x?.Any() == true).WithMessage("Parametre listesi boş olamaz");

            RuleForEach(x => x.Parameters)
                .ChildRules(parameter =>
                {
                    parameter.RuleFor(x => x.Code)
                        .NotNull().NotEmpty().WithMessage("Parametreler içerisindeki code alanı boş bırakılamaz.");

                    parameter.RuleFor(x => x.Values)
                        .Must(x => x?.Any() == true).WithMessage("Parametre değerleri listesi boş olamaz");

                    parameter.RuleForEach(x => x.Values)
                        .ChildRules(value =>
                        {
                            value.RuleFor(x => x.Value).NotNull().NotEmpty().WithMessage("Parametre değerlerinde value alanı boş olamaz");
                            value.RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Parametre değerlerinde isim alanı boş olamaz");
                        }).When(x => x.Values?.Any() == true);
                })
                .When(x => x.Parameters?.Any() == true);
        }
    }
}
