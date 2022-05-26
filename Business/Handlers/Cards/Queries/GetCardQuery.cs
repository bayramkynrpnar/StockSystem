
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Cards.Queries
{
    public class GetCardQuery : IRequest<IDataResult<Card>>
    {
        public int Id { get; set; }

        public class GetCardQueryHandler : IRequestHandler<GetCardQuery, IDataResult<Card>>
        {
            private readonly ICardRepository _cardRepository;
            private readonly IMediator _mediator;

            public GetCardQueryHandler(ICardRepository cardRepository, IMediator mediator)
            {
                _cardRepository = cardRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Card>> Handle(GetCardQuery request, CancellationToken cancellationToken)
            {
                var card = await _cardRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Card>(card);
            }
        }
    }
}
