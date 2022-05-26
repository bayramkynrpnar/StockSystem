
using Business.Handlers.Stocks.Commands;
using FluentValidation;

namespace Business.Handlers.Stocks.ValidationRules
{

    public class CreateStockValidator : AbstractValidator<CreateStockCommand>
    {
        public CreateStockValidator()
        {
            RuleFor(x => x.CardId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.Cost).NotEmpty();
            RuleFor(x => x.StorageId).NotEmpty();

        }
    }
    public class UpdateStockValidator : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockValidator()
        {
            RuleFor(x => x.CardId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.Cost).NotEmpty();
            RuleFor(x => x.StorageId).NotEmpty();

        }
    }
}