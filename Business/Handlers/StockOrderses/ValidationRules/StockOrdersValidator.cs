
using Business.Handlers.StockOrderses.Commands;
using FluentValidation;

namespace Business.Handlers.StockOrderses.ValidationRules
{

    public class CreateStockOrdersValidator : AbstractValidator<CreateStockOrdersCommand>
    {
        public CreateStockOrdersValidator()
        {
            RuleFor(x => x.CardId).NotEmpty();
            RuleFor(x => x.OrderDate).NotEmpty();
            RuleFor(x => x.OrderBy).NotEmpty();

        }
    }
    public class UpdateStockOrdersValidator : AbstractValidator<UpdateStockOrdersCommand>
    {
        public UpdateStockOrdersValidator()
        {
            RuleFor(x => x.CardId).NotEmpty();
            RuleFor(x => x.OrderDate).NotEmpty();
            RuleFor(x => x.OrderBy).NotEmpty();

        }
    }
}