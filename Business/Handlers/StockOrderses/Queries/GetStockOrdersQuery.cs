
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.StockOrderses.Queries
{
    public class GetStockOrdersQuery : IRequest<IDataResult<StockOrders>>
    {
        public int Id { get; set; }

        public class GetStockOrdersQueryHandler : IRequestHandler<GetStockOrdersQuery, IDataResult<StockOrders>>
        {
            private readonly IStockOrdersRepository _stockOrdersRepository;
            private readonly IMediator _mediator;

            public GetStockOrdersQueryHandler(IStockOrdersRepository stockOrdersRepository, IMediator mediator)
            {
                _stockOrdersRepository = stockOrdersRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<StockOrders>> Handle(GetStockOrdersQuery request, CancellationToken cancellationToken)
            {
                var stockOrders = await _stockOrdersRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<StockOrders>(stockOrders);
            }
        }
    }
}
