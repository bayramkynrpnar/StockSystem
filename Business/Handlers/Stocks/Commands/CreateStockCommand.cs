
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
using Business.Handlers.Stocks.ValidationRules;

namespace Business.Handlers.Stocks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateStockCommand : IRequest<IResult>
    {

        public int CardId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public int StorageId { get; set; }


        public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, IResult>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IStorageRepository _storageRepository;
            private readonly IStockOrdersRepository _stockOrdersRepository;
            private readonly IMediator _mediator;
            public CreateStockCommandHandler(IStockRepository stockRepository, IMediator mediator, IStorageRepository storageRepository, IStockOrdersRepository stockOrdersRepository)
            {
                _stockRepository = stockRepository;

                _mediator = mediator;
                _storageRepository = storageRepository;
                _stockOrdersRepository = stockOrdersRepository;
                
            }

            [ValidationAspect(typeof(CreateStockValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateStockCommand request, CancellationToken cancellationToken)
            {
                var isThereStockRecord = _stockRepository.Query().Any(u => u.CardId == request.CardId);

                if (isThereStockRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var storage = _storageRepository.Get(x => x.Id == request.StorageId);
                var addedStock = new Stock
                {
                    CardId = request.CardId,
                    Quantity = request.Quantity,
                    Cost = request.Cost,
                    StorageId = request.StorageId,
                };
                _stockOrdersRepository.Add(new StockOrders()
                {
                    OrderBy = storage.Name,
                    CardId = request.CardId,
                    OrderDate = System.DateTime.Now,
                    OrderType = Entities.Enum.OrderType.Giriş
                });

                _stockRepository.Add(addedStock);
                await _stockRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}