using DisabilityCompensation.Application.Commands.Compensations;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Compensations
{
    public class AddCompensationCommandValidator : AbstractValidator<AddCompensationCommand>
    {
        public AddCompensationCommandValidator()
        {
            #region Claimant
            RuleFor(x => x.Claimant).NotNull().WithMessage("Hak sahibi bilgileri null olamaz.");

            RuleFor(x => x.Claimant!.Name)
                .NotNull().NotEmpty().WithMessage("Hak sahibi Adı boş geçilemez")
                .When(x => x.Claimant != null);

            RuleFor(x => x.Claimant!.Surname)
                .NotNull().NotEmpty().WithMessage("Hak sahibi Soyadı boş geçilemez")
                .When(x => x.Claimant != null);

            RuleFor(x => x.Claimant!.BirthDate)
                .NotNull().NotEmpty().WithMessage("Hak sahibi Doğum günü boş geçilemez")
                .When(x => x.Claimant != null);

            RuleFor(x => x.Claimant!.TCKN)
                .NotNull().NotEmpty().WithMessage("Hak Sahibi TCKN boş geçilemez")
                .When(x => x.Claimant != null);

            RuleFor(x => x.Claimant!.Gender)
                .NotNull().NotEmpty().WithMessage("Hak Sahibi Cinsiyeti boş geçilemez")
                .When(x => x.Claimant != null);

            RuleFor(x => x.Claimant!.MaritalStatus)
                .NotNull().NotEmpty().WithMessage("Hak Sahibi Medeni Hali boş geçilemez")
                .When(x => x.Claimant != null);

            RuleFor(x => x.Claimant!.EmploymentStatus)
                .NotNull().NotEmpty().WithMessage("Hak Sahibi Çalışma Durumu boş geçilemez")
                .When(x => x.Claimant != null);
            #endregion

            #region Event
            RuleFor(x => x.Event).NotNull().WithMessage("Olay bilgileri null olamaz.");

            RuleFor(x => x.Event!.EventType)
                .NotNull().NotEmpty().WithMessage("Olay Türü boş geçilemez")
                .When(x => x.Event != null);

            RuleFor(x => x.Event!.EventDate)
                .NotNull().NotEmpty().WithMessage("Olay Tarihi boş geçilemez")
                .When(x => x.Event != null);

            RuleFor(x => x.Event!.ExaminationDate)
                .NotNull().NotEmpty().WithMessage("Muayene Tarihi boş geçilemez")
                .When(x => x.Event != null);

            RuleFor(x => x.Event!.LifeStatus)
                .NotNull().NotEmpty().WithMessage("Durum boş geçilemez")
                .When(x => x.Event != null);

            RuleFor(x => x.Event!.LifeTable)
                .NotNull().NotEmpty().WithMessage("Yaşam Tablosu boş geçilemez")
                .When(x => x.Event != null);
            #endregion

            #region Expenses

            RuleForEach(x => x.Expenses)
                .ChildRules(expense =>
                {
                    expense.RuleFor(x => x.ExpenseType).NotNull().NotEmpty().WithMessage("Masraf Tipi boş geçilemez");
                    expense.RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Masraf Tarihi boş geçilemez");
                    expense.RuleFor(x => x.Amount).NotNull().NotEmpty().WithMessage("Masraf Tutarı boş geçilemez");
                })
                .When(x => x.Expenses != null && x.Expenses.Any());

            #endregion

            #region Documents

            RuleForEach(x => x.Documents)
                .ChildRules(document =>
                {
                    document.RuleFor(x => x.DocumentType).NotNull().NotEmpty().WithMessage("Evrak Tipi boş geçilemez");
                    document.RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Evrak Tarihi boş geçilemez");
                    document.RuleFor(x => x.File).NotNull().NotEmpty().WithMessage("Evrak Dosyası boş geçilemez");
                })
                .When(x => x.Documents != null && x.Documents.Any());

            #endregion
        }
    }
}
