
using Business.Handlers.Cards.Commands;
using FluentValidation;

namespace Business.Handlers.Cards.ValidationRules
{

    public class CreateCardValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardValidator()
        {
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.SerialNo).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();

        }
    }
    public class UpdateCardValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardValidator()
        {
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.SerialNo).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();

        }
    }
}