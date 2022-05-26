
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

namespace Business.Handlers.Cards.Queries
{

    public class GetCardsQuery : IRequest<IDataResult<IEnumerable<Card>>>
    {
        public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, IDataResult<IEnumerable<Card>>>
        {
            private readonly ICardRepository _cardRepository;
            private readonly IMediator _mediator;

            public GetCardsQueryHandler(ICardRepository cardRepository, IMediator mediator)
            {
                _cardRepository = cardRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Card>>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Card>>(await _cardRepository.GetListAsync());
            }
        }
    }
}