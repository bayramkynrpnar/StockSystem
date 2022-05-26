
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.StockOrderses.ValidationRules;

namespace Business.Handlers.StockOrderses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateStockOrdersCommand : IRequest<IResult>
    {

        public int CardId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderBy { get; set; }


        public class CreateStockOrdersCommandHandler : IRequestHandler<CreateStockOrdersCommand, IResult>
        {
            private readonly IStockOrdersRepository _stockOrdersRepository;
            private readonly IMediator _mediator;
            public CreateStockOrdersCommandHandler(IStockOrdersRepository stockOrdersRepository, IMediator mediator)
            {
                _stockOrdersRepository = stockOrdersRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateStockOrdersValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateStockOrdersCommand request, CancellationToken cancellationToken)
            {
                var isThereStockOrdersRecord = _stockOrdersRepository.Query().Any(u => u.CardId == request.CardId);

                if (isThereStockOrdersRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedStockOrders = new StockOrders
                {
                    CardId = request.CardId,
                    OrderDate = request.OrderDate,
                    OrderBy = request.OrderBy,

                };

                _stockOrdersRepository.Add(addedStockOrders);
                await _stockOrdersRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}