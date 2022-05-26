
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Stocks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteStockCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, IResult>
        {
            private readonly IStockRepository _stockRepository;
            private readonly IStockOrdersRepository _stockOrderRepository;
            private readonly IStorageRepository _storage;
            private readonly IMediator _mediator;

            public DeleteStockCommandHandler(IStockRepository stockRepository, IMediator mediator, IStockOrdersRepository stockOrdersRepository, IStorageRepository storageRepository)
            {
                _stockRepository = stockRepository;
                _mediator = mediator;
                _stockOrderRepository = stockOrdersRepository;
                _storage = storageRepository;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
            {
                var stockToDelete = _stockRepository.Get(p => p.Id == request.Id);
                var storoge = _storage.Get(x => x.Id == stockToDelete.Id);

                _stockOrderRepository.Add(new Entities.Concrete.StockOrders()
                {
                    CardId = stockToDelete.CardId,
                    OrderDate = System.DateTime.Now,
                    OrderType = Entities.Enum.OrderType.Çıkış,
                    OrderBy = storoge.Name
                });
                _stockRepository.Delete(stockToDelete);
                await _stockRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}