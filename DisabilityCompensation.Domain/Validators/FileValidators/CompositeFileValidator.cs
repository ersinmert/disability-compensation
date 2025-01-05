using DisabilityCompensation.Domain.Exceptions;
using DisabilityCompensation.Domain.Interfaces.IValidators;
using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Validators.FileValidators
{
    public class CompositeFileValidator : ICompositeFileValidator
    {
        private readonly List<IFileValidator> _validators;

        public CompositeFileValidator(IEnumerable<IFileValidator> validators)
        {
            _validators = validators.ToList();
        }

        public async Task ValidateAsync(IFormFile file)
        {
            foreach (var validator in _validators)
            {
                var validation = await validator.ValidateAsync(file);
                if (!validation.Success)
                {
                    throw new NotValidException(validation.Message!);
                }
            }
        }
    }
}
