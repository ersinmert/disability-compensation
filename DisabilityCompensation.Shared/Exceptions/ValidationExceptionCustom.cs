using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Shared.Exceptions
{
    public class ValidationExceptionCustom : Exception
    {
        public IEnumerable<BaseValidationError> Errors { get; }

        public ValidationExceptionCustom()
            : base("One or more validation failures have occured.")
        {
            Errors = new List<BaseValidationError>();
        }

        public ValidationExceptionCustom(IEnumerable<BaseValidationError> errors)
            : this()
        {
            Errors = errors;
        }
    }
}
