
using Business.Handlers.Storages.Commands;
using FluentValidation;

namespace Business.Handlers.Storages.ValidationRules
{

    public class CreateStorageValidator : AbstractValidator<CreateStorageCommand>
    {
        public CreateStorageValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();

        }
    }
    public class UpdateStorageValidator : AbstractValidator<UpdateStorageCommand>
    {
        public UpdateStorageValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();

        }
    }
}