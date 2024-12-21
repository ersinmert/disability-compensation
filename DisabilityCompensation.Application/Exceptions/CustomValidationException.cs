using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Shared.Exceptions
{
    public class CustomValidationException : Exception
    {
        public IEnumerable<BaseValidationError> Errors { get; }

        public CustomValidationException()
            : base("One or more validation failures have occured.")
        {
            Errors = new List<BaseValidationError>();
        }

        public CustomValidationException(IEnumerable<BaseValidationError> errors)
            : this()
        {
            Errors = errors;
        }
    }
}
