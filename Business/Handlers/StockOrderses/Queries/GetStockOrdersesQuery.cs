
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.StockOrderses.Queries
{

    public class GetStockOrdersesQuery : IRequest<IDataResult<IEnumerable<StockOrders>>>
    {
        public class GetStockOrdersesQueryHandler : IRequestHandler<GetStockOrdersesQuery, IDataResult<IEnumerable<StockOrders>>>
        {
            private readonly IStockOrdersRepository _stockOrdersRepository;
            private readonly IMediator _mediator;

            public GetStockOrdersesQueryHandler(IStockOrdersRepository stockOrdersRepository, IMediator mediator)
            {
                _stockOrdersRepository = stockOrdersRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<StockOrders>>> Handle(GetStockOrdersesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<StockOrders>>(await _stockOrdersRepository.GetListAsync());
            }
        }
    }
}